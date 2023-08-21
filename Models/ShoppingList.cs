namespace Swiftshop.Models
{
    public class ShoppingList
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
