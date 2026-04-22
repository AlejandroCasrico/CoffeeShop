using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.UserDTOs
{
    public class UserWithRoleDTO
    {
        public string? Email {get;set;}
     
        public string? Rol { get; set; } 
    }
}