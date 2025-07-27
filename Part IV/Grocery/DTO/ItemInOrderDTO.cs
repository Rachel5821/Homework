using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ItemInOrderDTO
    {
        //public int ItemInOrderId { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
    }
}
