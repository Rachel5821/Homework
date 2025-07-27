using DBEntities.Models;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemDal:IItemDal
    {
        public List<Item> getItems(string? name = "")
        {
            try
            {
                using D_Context context = new D_Context();
                if (string.IsNullOrEmpty(name))
                    return context.Items.Select(i => (Item)i).ToList();
                else
                    return context.Items
                        .Where(a => a.ItemName.Contains(name))
                        .Select(u => (Item)u)
                        .ToList();
            }
            catch
            {

                return new List<Item>();
            }
        }
    }
}
