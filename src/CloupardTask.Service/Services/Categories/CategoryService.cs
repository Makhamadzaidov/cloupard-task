using AutoMapper;
using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.Commons.Extensions;
using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Categories;
using CloupardTask.DataAccess.Repositories.Categories;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Categories;
using CloupardTask.Service.Interfaces.Categories;
using CloupardTask.Service.ViewModels.Categories;
using System.Linq.Expressions;
using System.Net;

namespace CloupardTask.Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext dbContext, IMapper mapper)
        {
            _appDbContext = dbContext;
            _categoryRepository = new CategoryRepository(_appDbContext);
            _mapper = mapper;
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var createdCategory = await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryViewModel>(createdCategory);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var category = await _categoryRepository.GetAsync(c => c.Name == name);
            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(Expression<Func<Category, bool>> expression = null, PaginationParams? @params = null)
        {

            var categories = _categoryRepository.GetAllProducts(expression)
                                              .OrderBy(p => p.Name)
                                              .ToPagedAsEnumerable(@params);

            var categoryViews = new List<CategoryViewModel>();

            foreach (var categoryView in categories)
            {
                var category = _mapper.Map<CategoryViewModel>(categoryView);
                categoryViews.Add(category);
            }
            return categoryViews;
        }

        public async Task<CategoryViewModel?> GetAsync(Expression<Func<Category, bool>> expression)
        {
            var category = await _categoryRepository.GetAsync(expression);

            if (category is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
            }

            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> UpdateAsync(string oldCategoryName, CategoryCreateDto updatedCategory)
        {
            var existingCategory = await _categoryRepository.GetAsync(c => c.Name == oldCategoryName);
            if (existingCategory == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");

            if (existingCategory.Name is not null)
            {
                existingCategory.Name = updatedCategory.Name;
            }
            if (existingCategory.Description is not null)
            {
                existingCategory.Description = updatedCategory.Description;
            }

            var updatedEntity = await _categoryRepository.UpdateAsync(existingCategory);
            return _mapper.Map<CategoryViewModel>(updatedEntity);
        }
    }
}
