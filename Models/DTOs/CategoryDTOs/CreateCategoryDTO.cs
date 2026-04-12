using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(40, ErrorMessage ="La cantidad de caracteres excede el limite")]
        [MinLength(3,ErrorMessage ="La cantidad de caracteres es menor al limite")]
        public string? CategoryName { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage ="La cantidad de caracteres excede el limite")]
        public string? Description { get; set; }
    }
}