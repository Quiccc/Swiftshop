using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ProductImage { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public string? SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
