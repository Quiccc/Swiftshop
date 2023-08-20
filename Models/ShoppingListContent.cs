namespace Swiftshop.Models
{
    public partial class ShoppingListContent
    {
        public int? ListId { get; set; }
        public int? ItemId { get; set; }
        public ShoppingList? List { get; set; }
        public Item? Item { get; set; }
        public int? Quantity { get; set; }
    }
}
