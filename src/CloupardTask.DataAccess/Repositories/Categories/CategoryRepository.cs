using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Categories;
using CloupardTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

#nullable disable
namespace CloupardTask.DataAccess.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            var entity = await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Category> GetAllProducts(Expression<Func<Category, bool>> predicate = null)
            => predicate == null ? _dbContext.Categories : _dbContext.Categories.Where(predicate);

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> predicate = null)
            => await _dbContext.Categories.FirstOrDefaultAsync(predicate);

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.Id);

            _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await _dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
