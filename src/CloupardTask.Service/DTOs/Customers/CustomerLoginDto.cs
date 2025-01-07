using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Service.DTOs.Customers
{
    public class CustomerLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
