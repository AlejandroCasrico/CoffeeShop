using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.UserDTOs
{
    public class RegisterDTO
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
        [Required(ErrorMessage ="Password es requerida")]
        [DataType(DataType.Password)]
        [StringLength(9,ErrorMessage ="Maximo 9 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage ="La contraseña debe tener almenos un caracter especial y/o numero")]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="La contraseña no coincide")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Rol { get; set; } 
    }
}