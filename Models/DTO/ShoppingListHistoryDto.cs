namespace Swiftshop.Models.DTO
{
    public class ShoppingListHistoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> ProductNames { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
