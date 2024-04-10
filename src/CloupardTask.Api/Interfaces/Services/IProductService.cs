using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;
using System.Linq.Expressions;

namespace CloupardTask.Api.Interfaces.Services
{
	public interface IProductService
	{
		Task<Product> CreateAsync(ProductCreateDto dto);
		Task<Product> UpdateAsync(ProductUpdateDto dto);
		Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression = null, PaginationParams? @params = null);
		Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
	}
}
