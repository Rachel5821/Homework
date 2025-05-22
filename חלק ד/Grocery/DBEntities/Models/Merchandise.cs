using System;
using System.Collections.Generic;

namespace DBEntities.Models;
public partial class Merchandise
{
    public int MerchandiseId { get; set; }

    public int? ItemId { get; set; }

    public decimal? Price { get; set; }

    public int? MinimumQuantity { get; set; }

    public virtual Item? Item { get; set; }


}
