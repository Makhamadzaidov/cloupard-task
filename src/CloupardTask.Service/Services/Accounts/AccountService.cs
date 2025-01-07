using AutoMapper;
using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Customers;
using CloupardTask.DataAccess.Repositories.Customers;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.Interfaces.Accounts;
using CloupardTask.Service.ViewModels.Customers;

namespace CloupardTask.Service.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public AccountService(AppDbContext dbContext, IMapper mapper)
        {
            _appDbContext = dbContext;
            _customerRepository = new CustomerRepository(_appDbContext);
            _mapper = mapper;
        }
        public async Task<CustomerViewModel> LoginAsync(CustomerLoginDto dto)
        {
            var user = await _customerRepository.GetAsync(customer => customer.Email.Equals(dto.Email));

            if (user is null)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "User not found");
            }

            if (!user.Password.Equals(dto.Password))
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.BadGateway, "Login or password wrong !!!");
            }

            return _mapper.Map<CustomerViewModel>(user);
        }
    }
}
