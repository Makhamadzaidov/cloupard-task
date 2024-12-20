using AutoMapper;
using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Api.Commons.Extensions;
using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Customers;
using CloupardTask.DataAccess.Repositories.Customers;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.Interfaces.Customers;
using CloupardTask.Service.ViewModels.Customers;
using System.Linq.Expressions;
using System.Net;

namespace CloupardTask.Service.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext dbContext, IMapper mapper)
        {
            _appDbContext = dbContext;
            _customerRepository = new CustomerRepository(_appDbContext);
            _mapper = mapper;
        }

        public async Task<CustomerViewModel> CreateAsync(CustomerCreateDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            var createdCustomer = await _customerRepository.CreateAsync(customer);
            return _mapper.Map<CustomerViewModel>(createdCustomer);
        }

        public async Task<bool> DeleteAsync(string email)
        {
            var customer = await _customerRepository.GetAsync(c => c.Email == email);
            if (customer == null)
                return false;

            await _customerRepository.DeleteAsync(customer);
            return true;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync(Expression<Func<Customer, bool>> expression = null, PaginationParams? @params = null)
        {
            var customers = _customerRepository.GetAll(expression)
                                               .OrderBy(c => c.LastName)
                                               .ToPagedAsEnumerable(@params);

            var customerViews = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                var customerView = _mapper.Map<CustomerViewModel>(customer);
                customerViews.Add(customerView);
            }
            return customerViews;
        }

        public async Task<CustomerViewModel?> GetAsync(Expression<Func<Customer, bool>> expression)
        {
            var customer = await _customerRepository.GetAsync(expression);

            if (customer is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Customer not found");
            }

            return _mapper.Map<CustomerViewModel>(customer);
        }

        public async Task<CustomerViewModel> UpdateAsync(string email, CustomerUpdateDto updatedCustomer)
        {
            var existingCustomer = await _customerRepository.GetAsync(c => c.Email == email);
            if (existingCustomer == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Customer not found");

            if (!string.IsNullOrEmpty(updatedCustomer.FirstName))
            {
                existingCustomer.FirstName = updatedCustomer.FirstName;
            }

            if (!string.IsNullOrEmpty(updatedCustomer.LastName))
            {
                existingCustomer.LastName = updatedCustomer.LastName;
            }

            if (!string.IsNullOrEmpty(updatedCustomer.PhoneNumber))
            {
                existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(updatedCustomer.Address))
            {
                existingCustomer.Address = updatedCustomer.Address;
            }

            var updatedEntity = await _customerRepository.UpdateAsync(existingCustomer);
            return _mapper.Map<CustomerViewModel>(updatedEntity);
        }
    }
}
