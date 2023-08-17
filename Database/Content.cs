using System;
using System.Collections.Generic;

namespace Swiftshop.Database;

public partial class Content
{
    public int? ListId { get; set; }

    public int? ItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ShoppingList? List { get; set; }
}
