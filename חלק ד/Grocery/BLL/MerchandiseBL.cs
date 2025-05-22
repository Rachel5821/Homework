using IBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using AutoMapper;
using DBEntities.Models;
using DTO;

namespace BLL
{
    public class MerchandiseBL: IMerchandiseBL
    {
        private readonly IMerchandiseDal _marchendiseDal;
        private readonly MapperConfiguration configMerchandiseConverter;
        public MerchandiseBL(IMerchandiseDal marchendiseDal)
        {
            _marchendiseDal = marchendiseDal;
            configMerchandiseConverter = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Merchandise, MerchandiseDTO>()
                    .ForMember(x => x.MerchandiseId, s => s.MapFrom(p => p.MerchandiseId))
                    .ReverseMap()
                    .ForMember(x => x.MerchandiseId, s => s.MapFrom(p => p.MerchandiseId));
            });

        }
        public List<MerchandiseDTO> GetMerchandises()
        {
            var Merchandises = _marchendiseDal.GetMerchendises();
            List<MerchandiseDTO> convertedList = new List<MerchandiseDTO>();
            Merchandises.ForEach(m => convertedList.Add(convertMerchandise(m)));
            return convertedList;
        }
       
        private MerchandiseDTO convertMerchandise(Merchandise source)
        {
            var mapper = new Mapper(configMerchandiseConverter);
            MerchandiseDTO merchandise = mapper.Map<MerchandiseDTO>(source);
            return merchandise;
        }
    }
}
