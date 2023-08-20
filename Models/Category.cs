using System.ComponentModel.DataAnnotations;

namespace Swiftshop.Models
{
    public partial class Category
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Subcategory>? Subcategories { get; set; }
    }
}
