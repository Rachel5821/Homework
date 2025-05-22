using System;
using System.Collections.Generic;

namespace DBEntities.Models;
public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }



   public virtual ICollection<Merchandise> Merchandises { get; set; } = new List<Merchandise>();
}
