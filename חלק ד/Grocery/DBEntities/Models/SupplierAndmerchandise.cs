using System;
using System.Collections.Generic;

namespace DBEntities.Models;
public partial class SupplierAndmerchandise
{
    public int? MerchandiseId { get; set; }

    public int? SupplierId { get; set; }
    public virtual Merchandise Merchandise { get; set; }
    public virtual Supplier Supplier { get; set; }
}
