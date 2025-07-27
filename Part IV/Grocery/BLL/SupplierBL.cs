using AutoMapper;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using DBEntities.Models;    
using DTO;

namespace BLL
{
    public class SupplierBL : ISupplierBL
    {

        private readonly ISupplierDal _supplierDal;
        private readonly MapperConfiguration configSupplierConverter;
        public SupplierBL(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
            configSupplierConverter = new MapperConfiguration(cfg =>
    cfg.CreateMap<Supplier, SupplierDTO>()
        .ForMember(x => x.SupplierId, s => s.MapFrom(p => p.SupplierId))
        .ForMember(x => x.ManufacturerName, s => s.MapFrom(p => p.ManufacturerName))
        .ForMember(x => x.PhoneNumber, s => s.MapFrom(p => p.PhoneNumber))
        .ForMember(x => x.RepresentativeName, s => s.MapFrom(p => p.RepresentativeName))
        .ForMember(x => x.UserId, s => s.MapFrom(p => p.UserId))
        .ReverseMap()
        .ForMember(x => x.SupplierId, s => s.MapFrom(p => p.SupplierId))
        .ForMember(x => x.ManufacturerName, s => s.MapFrom(p => p.ManufacturerName))
        .ForMember(x => x.PhoneNumber, s => s.MapFrom(p => p.PhoneNumber))
        .ForMember(x => x.RepresentativeName, s => s.MapFrom(p => p.RepresentativeName))
        .ForMember(x => x.UserId, s => s.MapFrom(p => p.UserId))
        .ForMember(x => x.User, s => s.Ignore())
);
        }
        public List<SupplierDTO> GetSuppliers(string name = "")
        {
            var suppliers = _supplierDal.GetSuppliers();
            List<SupplierDTO> convertedList = new List<SupplierDTO>();
            suppliers.ForEach(s=>convertedList.Add(convertSupplier(s)));
            return convertedList;

        }

        private SupplierDTO convertSupplier(Supplier source)
        {
            var mapper = new Mapper(configSupplierConverter);
            SupplierDTO supplier = mapper.Map<SupplierDTO>(source);
            return supplier;
        }
    }
}
