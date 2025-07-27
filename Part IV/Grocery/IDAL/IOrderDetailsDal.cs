using DBEntities.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IOrderDetailsDal
    {
        List<OrderDetail> GetOrders();
        List<OrderDetail> GetOrdersBySupplierId(int supplierId);
        List<OrderDetail> GetOrdersBySupplierIdIfCompleted(int supplierId);
        List<OrderDetail> GetOrdersBySupplierIdIfNotCompleted(int supplierId);
        void ConfirmById(int id);


    }
}
