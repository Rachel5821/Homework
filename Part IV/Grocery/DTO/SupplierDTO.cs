using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }

        public string? ManufacturerName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string RepresentativeName { get; set; } = null!;

        public int? UserId { get; set; }  // שינוי ל-nullable int כמו במודל המקורי
    }
}
