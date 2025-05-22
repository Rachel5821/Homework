using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEntities.Models;

namespace IDAL
{
    public interface ISupplierAndmerchandiseDal
    {
         List<Merchandise> GetMerchandiseBySupplier(int id);

    }
}
