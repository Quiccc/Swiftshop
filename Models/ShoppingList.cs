﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Swiftshop.Models
{
    public class ShoppingList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFavorited { get; set; }
        public bool? IsStarted { get; set; }
        public virtual ICollection<ShoppingListContent>? ListContents { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ShoppingList()
        {
            ListContents = null;
            IsActive = true;
            IsFavorited = false;
            IsStarted = false;
            CreatedAt = DateTime.Now;
        }
    }
}
