using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage ="Password es requerida")]
        [DataType(DataType.Password)]
        [StringLength(9,ErrorMessage ="Maximo 9 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage ="La contraseña debe tener almenos un caracter especial y/o numero")]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="La contraseña no coincide")]
        public string? ConfirmPassword { get; set; }
    }
}