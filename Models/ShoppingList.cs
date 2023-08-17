namespace Swiftshop.Models
{
    public partial class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection <ShoppingListContent>? listContents { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}
