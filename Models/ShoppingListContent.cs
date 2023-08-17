namespace Swiftshop.Models
{
    public partial class ShoppingListContent
    {
        public int listId { get; set; }
        public int itemId { get; set; }
        public ShoppingList List { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
