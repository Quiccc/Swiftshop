using System;
using System.Collections.Generic;

namespace Swiftshop.Database;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public byte[] Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<ShoppingList> ShoppingLists { get; } = new List<ShoppingList>();
}
