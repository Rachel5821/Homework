using BLL;
using DBEntities.Models;
using IDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplierDal : ISupplierDal
    {
        public List<Supplier> GetSuppliers(string? name = "")
        {
            try
            {
                using D_Context context = new D_Context();
                if (string.IsNullOrEmpty(name))
                    return context.Suppliers.ToList();
                else
                    return context.Suppliers
                        .Where(s => s.RepresentativeName.Contains(name))
                        .ToList();
            }
            catch
            {
                return new List<Supplier>();
            }
        }
       
    }
}