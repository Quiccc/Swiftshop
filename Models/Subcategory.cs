namespace Swiftshop.Models
{
    public partial class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int categoryId { get; set; }
        public Category Category { get; set; }
    }
}
