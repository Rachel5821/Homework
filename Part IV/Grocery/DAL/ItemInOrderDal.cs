using DBEntities.Models;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemInOrderDal : IItemInOrderDal
    {

        public List<ItemInOrder> GetItemsInOrder()
        {
            try
            {
                using D_Context context = new D_Context();
                 
                return context.ItemInOrders.Select(o =>(ItemInOrder)o).ToList();
            }
            catch
            {
                return new List<ItemInOrder> { };
            }
        }
        
        public List<ItemInOrder> GetItemsInOrderByOrderId(int orderId)
        {
            try
            {
                using D_Context context = new D_Context();
                return context.ItemInOrders.Where(x => x.OrderId == orderId).ToList();
            }
            catch
            {
                return new List<ItemInOrder>();
            }
        }


    }
}
