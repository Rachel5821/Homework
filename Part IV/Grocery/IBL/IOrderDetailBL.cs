using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IOrderDetailBL
    {
        List<OrderDetailsDTO> GetOrders();
        List<OrderDetailsDTO> GetOrdersBySupplierId(int supplierId);
        List<OrderDetailsDTO> GetOrdersBySupplierIdIfCompleted(int supplierId);
        List<OrderDetailsDTO> GetOrdersBySupplierIdIfNotCompleted(int supplierId);

        void ConfirmById (int id);

    }
}
