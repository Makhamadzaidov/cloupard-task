using CloupardTask.Api.Commons.Utils;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Categories;
using CloupardTask.Service.ViewModels.Categories;
using System.Linq.Expressions;

namespace CloupardTask.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateAsync(CategoryCreateDto dto);
        Task<CategoryViewModel> UpdateAsync(string oldCategoryName, CategoryCreateDto category);
        Task<bool> DeleteAsync(string name);
        Task<CategoryViewModel?> GetAsync(Expression<Func<Category, bool>> expression);
        Task<IEnumerable<CategoryViewModel>> GetAllAsync(Expression<Func<Category, bool>> expression = null, PaginationParams? @params = null);
    }
}
