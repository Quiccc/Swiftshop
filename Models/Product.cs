using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ProductImage { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public string? SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
