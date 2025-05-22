using AutoMapper;
using DBEntities.Models;
using DTO;
using IBL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SupplierAndmerchandiseBL : ISupplierAndmerchandiseBL
    {

        private readonly ISupplierAndmerchandiseDal _supplierAndmerchandiseDal;
        private readonly MapperConfiguration configsupplierAndmerchandiseConverter;

        public SupplierAndmerchandiseBL(ISupplierAndmerchandiseDal supplierAndmerchandiseDal)
        {
            _supplierAndmerchandiseDal= supplierAndmerchandiseDal;
            configsupplierAndmerchandiseConverter = new MapperConfiguration(cfg =>
                cfg.CreateMap<SupplierAndmerchandise, SupplierAndmerchandiseDTO>()
                    .ForMember(x => x.SupplierId, s => s.MapFrom(p => p.SupplierId))
                    .ReverseMap()
                    .ForMember(x => x.SupplierId, s => s.MapFrom(p => p.SupplierId)));
        }


        private SupplierAndmerchandiseDTO convertSupplierAndmerchandise(SupplierAndmerchandise source)
        {
            var mapper = new Mapper(configsupplierAndmerchandiseConverter);
            SupplierAndmerchandiseDTO supplierAndmerchandise = mapper.Map<SupplierAndmerchandiseDTO>(source);
            return supplierAndmerchandise;
        }



        //public List<MerchandiseDTO> GetMerchandiseBySupplier(int id)
        //{
        //    // מכיוון שה-DAL כבר מחזיר List<MerchandiseDTO>, אין צורך בהמרה נוספת
        //    //return _supplierAndmerchandiseDal.GetMerchandiseBySupplier(id));
        //    return _supplierAndmerchandiseDal.GetMerchandiseBySupplier(id)
        //}

        public List<MerchandiseDTO> GetMerchandiseBySupplier(int id)
        {
            var merchandiseList = _supplierAndmerchandiseDal.GetMerchandiseBySupplier(id);

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Merchandise, MerchandiseDTO>()
            );

            var mapper = new Mapper(config);
            return mapper.Map<List<MerchandiseDTO>>(merchandiseList);
        }
    }
}
