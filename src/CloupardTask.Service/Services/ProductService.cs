using AutoMapper;
using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.Commons.Extensions;
using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DbContexts;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Interfaces.Repositories;
using CloupardTask.Api.Interfaces.Services;
using CloupardTask.Api.Models;
using CloupardTask.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloupardTask.Api.Services
{
	public class ProductService : IProductService
	{
		private readonly AppDbContext _dbContext;
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IMapper mapper, AppDbContext dbContext)
		{
			_dbContext = dbContext;
			_productRepository = new ProductRepository(_dbContext);
			_mapper = mapper;
		}
		public async Task<Product> CreateAsync(ProductCreateDto dto)
		{
			var product = _mapper.Map<Product>(dto);

			return await _productRepository.CreateAsync(product);
		}

		public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
		{
			var entity = await _productRepository.GetAsync(expression);

			if (entity is null)
				throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product was not found");

			await _productRepository.DeleteAsync(entity);
			return true;
		}

        public Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression, PaginationParams? @params = null)
        {
            var products = _productRepository.GetAllProducts(expression)
                                              .OrderBy(p => p.Name)
                                              .ToPagedAsEnumerable(@params);
            return Task.FromResult(products);
        }


        public async Task<Product> UpdateAsync(ProductUpdateDto dto)
		{
			var entity = await _productRepository.GetAsync(p => p.Id == dto.Id);

			if (entity is null)
				throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product was not found");

			if (dto.Name is not null)
				entity.Name = dto.Name;

			if (dto.Description is not null)
				entity.Description = dto.Description;

			return await _productRepository.UpdateAsync(entity);
		}
	}
}
