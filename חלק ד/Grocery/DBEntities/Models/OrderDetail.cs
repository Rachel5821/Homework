// מה ששכחת להוסיף: Navigation Property במודל OrderDetail

using System;
using System.Collections.Generic;
namespace DBEntities.Models;
public partial class OrderDetail
{
    public int OrderId { get; set; }
    public int SupplierId { get; set; }
    public int MerchandiseId { get; set; }
    public int? Quantity { get; set; }
    public string? order_status { get; set; }
    public virtual List<ItemInOrder> OrderItems { get; set; } = new List<ItemInOrder>();

    //public virtual Supplier Supplier { get; set; }
}