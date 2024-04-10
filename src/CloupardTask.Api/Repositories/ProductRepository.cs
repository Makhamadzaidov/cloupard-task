using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.DbContexts;
using CloupardTask.Api.Interfaces.Repositories;
using CloupardTask.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloupardTask.Api.Repositories
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
			return predicate == null ? _dbContext.Products : _dbContext.Products.Where(predicate);
		}

		public async Task<Product> GetAsync(Expression<Func<Product, bool>> predicate = null)
		{
			return await _dbContext.Products.FirstOrDefaultAsync(predicate);
		}

		public async Task<Product> UpdateAsync(Product product)
		{
			var existingProduct = await _dbContext.Products.FindAsync(product.Id);
			if (existingProduct == null)
			{
				throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "This product was not found");
			}

			_dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
			await _dbContext.SaveChangesAsync();

			return existingProduct;
		}
	}
}
