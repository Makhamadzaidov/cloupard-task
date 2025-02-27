﻿using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string Password { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
