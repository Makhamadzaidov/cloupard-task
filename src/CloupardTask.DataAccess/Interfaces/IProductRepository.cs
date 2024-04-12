using CloupardTask.Api.Models;
using System.Linq.Expressions;

namespace CloupardTask.Api.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<Product> GetAsync(Expression<Func<Product, bool>> predicate = null);
        IQueryable<Product> GetAllProducts(Expression<Func<Product, bool>> predicate = null);
    }
}
