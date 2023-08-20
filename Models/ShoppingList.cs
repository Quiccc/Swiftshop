namespace Swiftshop.Models
{
    public partial class ShoppingList
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
