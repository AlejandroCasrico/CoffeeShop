using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Models.DTOs.UserDTOs
{
    public class UserDetailDTO
    {
    public string? Email { get; set; }
     public string? UserName { get; set; }
    public string? Rol { get; set; }
    public DateTime CreatedAt { get; set; }

    }
}