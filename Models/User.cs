namespace Swiftshop.Models
{
    public partial class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public virtual ICollection<ShoppingList>? ShoppingLists { get; set;}
        public string? UserRole { get; set; }

        public User()
        {
            this.UserRole = "User";
        }
    }
}
