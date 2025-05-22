using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public DateTime ExpirationDate { get; set; }
    }
}
