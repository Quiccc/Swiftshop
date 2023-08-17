using System.ComponentModel.DataAnnotations;

namespace Swiftshop.Models
{
    public partial class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
