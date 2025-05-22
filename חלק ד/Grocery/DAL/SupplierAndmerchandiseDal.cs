using DBEntities.Models;
using DTO;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplierAndmerchandiseDal : ISupplierAndmerchandiseDal
    {
        public List<Merchandise> GetMerchandiseBySupplier(int id)
        {
            try
            {
                using D_Context context = new D_Context();
                if (id > 0)
                {
                    return context.SupplierAndmerchandises
                      .Where(s => s.SupplierId == id)
                      .Select(s => s.Merchandise)  // החזרת אובייקטי Merchandise ישירות
                      .ToList();
                }
                return new List<Merchandise>();
            }
            catch (Exception ex)
            {
                return new List<Merchandise>();
            }
        }
    }
}
