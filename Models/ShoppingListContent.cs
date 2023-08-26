namespace Swiftshop.Models
{
    public class ShoppingListContent
    {
        public string? ListId { get; set; }
        public string? ProductId { get; set; }
        public ShoppingList? List { get; set; }
        public Product? Product { get; set; }
        public string? Description { get; set; }
    }
}
