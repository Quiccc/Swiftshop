using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Models
{
    public class Subcategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
