using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public bool IsManager { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        //public string Token { get; set; }
        public string order_status { get; set; }

        public string ManufacturerName { get; set; }
        public string PhoneNumber { get; set; }
        public string RepresentativeName { get; set; }
        public List<MerchandiseDTO> supplierMerchandises { get; set; } = new List<MerchandiseDTO>();
    }
   

}
