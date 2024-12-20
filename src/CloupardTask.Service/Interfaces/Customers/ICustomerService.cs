using CloupardTask.Api.Commons.Utils;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.ViewModels.Customers;
using System.Linq.Expressions;

namespace CloupardTask.Service.Interfaces.Customers
{
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="dto">The customer data transfer object containing details for the new customer.</param>
        /// <returns>A view model of the created customer.</returns>
        Task<CustomerViewModel> CreateAsync(CustomerCreateDto dto);

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="email">The email of the customer to update.</param>
        /// <param name="updatedCustomer">The DTO containing updated customer details.</param>
        /// <returns>A view model of the updated customer.</returns>
        Task<CustomerViewModel> UpdateAsync(string email, CustomerUpdateDto updatedCustomer);

        /// <summary>
        /// Deletes a customer by email.
        /// </summary>
        /// <param name="email">The email of the customer to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(string email);

        /// <summary>
        /// Retrieves a single customer based on a specified condition.
        /// </summary>
        /// <param name="expression">The condition to filter the customer.</param>
        /// <returns>A view model of the matched customer, or null if not found.</returns>
        Task<CustomerViewModel?> GetAsync(Expression<Func<Customer, bool>> expression);

        /// <summary>
        /// Retrieves all customers matching a specified condition with optional pagination.
        /// </summary>
        /// <param name="expression">The condition to filter customers (optional).</param>
        /// <param name="params">Pagination parameters (optional).</param>
        /// <returns>An enumerable of customer view models.</returns>
        Task<IEnumerable<CustomerViewModel>> GetAllAsync(Expression<Func<Customer, bool>> expression = null, PaginationParams? @params = null);
    }
}
