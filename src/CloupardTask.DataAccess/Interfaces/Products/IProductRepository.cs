using CloupardTask.Api.Models;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Interfaces.Products
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
