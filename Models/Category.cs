using System.ComponentModel.DataAnnotations;

namespace Swiftshop.Models
{
    public class Category
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Subcategory>? Subcategories { get; set; }
    }
}
