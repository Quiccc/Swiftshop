namespace Swiftshop.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ShoppingListContent> listContents { get; set; }
        public int subcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
