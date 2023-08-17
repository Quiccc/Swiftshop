using System.ComponentModel.DataAnnotations;

namespace Swiftshop.Models
{
    public partial class ShoppingList
    {
        [Key]
        public string Id { get; set; }
        public Content? ListContent { get; set; }
        public User ListUser { get; set; }
    }
}
