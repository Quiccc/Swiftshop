namespace Swiftshop.Models
{
    public class ShoppingListContent
    {
        public string? ListId { get; set; }
        public string? ItemId { get; set; }
        public ShoppingList? List { get; set; }
        public Product? Item { get; set; }
        public int? Quantity { get; set; }
    }
}
