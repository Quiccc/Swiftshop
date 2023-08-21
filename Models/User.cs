using Microsoft.AspNetCore.Identity;

namespace Swiftshop.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public virtual ICollection<ShoppingList>? ShoppingLists { get; set;}
        public string? UserRole { get; set; }

        public User()
        {
            this.UserRole = "User";
        }
    }
}
