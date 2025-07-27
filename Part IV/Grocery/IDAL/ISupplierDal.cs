using DBEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ISupplierDal
    {
       public List<Supplier> GetSuppliers(string name = "");
        //Task<int> AddUserAsync(User user);


    }
}
