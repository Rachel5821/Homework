using System;
using System.Collections.Generic;

namespace DBEntities.Models;

public partial class ItemInOrder
{
    public int ItemInOrderId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }
    public virtual OrderDetail OrderDetail { get; set; }



}
