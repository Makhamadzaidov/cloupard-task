using CloupardTask.Api.DbContexts;
using CloupardTask.Api.Models;
using CloupardTask.DataAccess.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#nullable disable
namespace CloupardTask.DataAccess.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            var entity = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task DeleteAsync(Product product)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetAllProducts(Expression<Func<Product, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Products.Include(p => p.Category) : _dbContext.Products.Include(p => p.Category).Where(predicate);
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> predicate = null)
        {
            return await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(predicate);
        }
        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(product.Id);

            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
