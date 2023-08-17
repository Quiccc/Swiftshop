using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Swiftshop.Models
{
    public partial class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ShoppingList>? UserLists { get; set; }
    }
}
