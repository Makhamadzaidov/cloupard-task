using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;
using CloupardTask.Service.ViewModels.Products;
using System.Linq.Expressions;

namespace CloupardTask.Service.Interfaces.Products
{
    public interface IProductService
    {
        Task<ProductViewModel> CreateAsync(ProductCreateDto dto);
        Task<ProductViewModel> UpdateAsync(string oldProductName, ProductUpdateDto dto);
        Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression = null, PaginationParams? @params = null);
        Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
        Task<ProductViewModel?> GetAsync(Expression<Func<Product, bool>> expression);
    }
}
