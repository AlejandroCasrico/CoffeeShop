using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
         [Required]
        [MaxLength(50, ErrorMessage ="Max 50 caracteres")]
        [MinLength(8, ErrorMessage ="Min 8 caracreres")]
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
         [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email {get;set;}
      
    }
}