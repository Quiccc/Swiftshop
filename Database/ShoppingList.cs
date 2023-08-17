using System;
using System.Collections.Generic;

namespace Swiftshop.Database;

public partial class ShoppingList
{
    public int Id { get; set; }

    public int? ListContent { get; set; }

    public int ListUser { get; set; }

    public virtual User ListUserNavigation { get; set; } = null!;
}
