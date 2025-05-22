using DBEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IItemInOrderDal
    {
        List<ItemInOrder> GetItemsInOrder();
        List<ItemInOrder> GetItemsInOrderByOrderId(int orderId);
    }
}
