using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Models
{
    public class ShoppingListHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public virtual string ListContents { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime HistoryDate { get; set; }

        public ShoppingListHistory()
        {
            HistoryDate = DateTime.Now;
        }
    }
}
