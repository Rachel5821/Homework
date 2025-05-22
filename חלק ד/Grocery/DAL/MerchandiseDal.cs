using DBEntities.Models;
using IBL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MerchandiseDal :IMerchandiseDal
    {
        public List<Merchandise> GetMerchendises()
        {
            try
            {
                using D_Context context = new D_Context();
                return context.Merchandises.Select(u => (Merchandise)u).ToList();
            }
            catch
            {

                return new List<Merchandise>();
            }
        }
    }
}
