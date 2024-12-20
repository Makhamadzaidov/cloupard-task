using AutoMapper;
using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.Commons.Extensions;
using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DbContexts;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;
using CloupardTask.DataAccess.Interfaces.Categories;
using CloupardTask.DataAccess.Interfaces.Products;
using CloupardTask.DataAccess.Repositories.Categories;
using CloupardTask.DataAccess.Repositories.Products;
using CloupardTask.Service.Interfaces.Commons;
using CloupardTask.Service.Interfaces.Products;
using CloupardTask.Service.ViewModels.Products;
using System.Linq.Expressions;
using System.Net;

namespace CloupardTask.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        private readonly IFileService _fileService;
        public ProductService(IMapper mapper,
            AppDbContext dbContext,
            IFileService fileService)
        {
            _dbContext = dbContext;
            _productRepository = new ProductRepository(_dbContext);
            _categoryRepository = new CategoryRepository(_dbContext);
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<ProductViewModel> CreateAsync(ProductCreateDto dto)
        {
            var productExists = await _productRepository.GetAsync(p => p.Name.Equals(dto.Name));

            if (productExists is not null)
            {
                throw new StatusCodeException(HttpStatusCode.Conflict, "This product already exist");
            }

            var categoryExists = await _productRepository.GetAsync(p => p.Category.Name == dto.CategoryName)
                         ?? throw new StatusCodeException(HttpStatusCode.Conflict, "This category does not exist in Categories Table");

            if (categoryExists is null)
            {
                throw new StatusCodeException(HttpStatusCode.Conflict, "This category does not have in Categories Table");
            }

            var product = _mapper.Map<Product>(dto);
            product.CategoryId = categoryExists.Id;
            product.Category = categoryExists.Category;

            if (dto.Image is not null)
            {
                product.ImageUrl = await _fileService.SaveImageAsync(dto.Image);
            }

            var root = await _productRepository.CreateAsync(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductViewModel>(root);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var entity = await _productRepository.GetAsync(expression);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");

            await _productRepository.DeleteAsync(entity);
            return true;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression, PaginationParams? @params = null)
        {
            var products = _productRepository.GetAllProducts(expression)
                                              .OrderBy(p => p.Name)
                                              .ToPagedAsEnumerable(@params);

            var productViews = new List<ProductViewModel>();
            foreach (var productView in products)
            {
                var product = _mapper.Map<ProductViewModel>(productView);
                productViews.Add(product);
            }
            return productViews;
        }

        public async Task<ProductViewModel?> GetAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _productRepository.GetAsync(expression);

            if (product is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
            }

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> UpdateAsync(string oldProductName, ProductUpdateDto dto)
        {
            var entity = await _productRepository.GetAsync(p => p.Name == oldProductName);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");

            if (dto.Name is not null)
                entity.Name = dto.Name;

            if (dto.Description is not null)
                entity.Description = dto.Description;

            if (dto.CategoryName is not null)
            {
                var category = await _categoryRepository.GetAsync(c => c.Name == dto.CategoryName);

                if (category is null)
                {
                    throw new StatusCodeException(HttpStatusCode.Conflict, "This category does not exist in Categories Table");
                }

                entity.CategoryId = category.Id;
            }


            if (dto.Image is not null)
            {
                entity.ImageUrl = await _fileService.SaveImageAsync(dto.Image);
            }

            var product = await _productRepository.UpdateAsync(entity);

            return _mapper.Map<ProductViewModel>(product);
        }
    }
}
