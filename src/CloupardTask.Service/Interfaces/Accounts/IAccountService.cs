using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.ViewModels.Customers;

namespace CloupardTask.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<CustomerViewModel> LoginAsync(CustomerLoginDto dto);
    }
}
