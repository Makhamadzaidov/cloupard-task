using CloupardTask.Api.Commons.Utils;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.Interfaces.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCustomer), new { email = result.Email }, result);
        }

        // GET: api/Customer/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCustomer(string email)
        {
            var customer = await _customerService.GetAsync(c => c.Email == email);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] PaginationParams paginationParams)
        {
            var customers = await _customerService.GetAllAsync(null, paginationParams);
            return Ok(customers);
        }

        // PUT: api/Customer/{email}
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateCustomer(string email, [FromBody] CustomerUpdateDto updatedCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _customerService.UpdateAsync(email, updatedCustomer);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/Customer/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteCustomer(string email)
        {
            var isDeleted = await _customerService.DeleteAsync(email);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
