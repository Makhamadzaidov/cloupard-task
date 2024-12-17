using CloupardTask.Api.Models;
using CloupardTask.Domain.Models;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category> GetAsync(Expression<Func<Category, bool>> predicate = null);
        IQueryable<Category> GetAllProducts(Expression<Func<Category, bool>> predicate = null);
    }
}
