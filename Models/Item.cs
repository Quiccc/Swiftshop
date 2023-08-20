namespace Swiftshop.Models
{
    public partial class Item
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public int? SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
