﻿namespace Swiftshop.Models
{
    public class Subcategory
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
