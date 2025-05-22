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
    public class ItemBL : IItemBL
    {
        private readonly IItemDal _itemDal;
        private readonly MapperConfiguration configItemConverter;

        public ItemBL(IItemDal itemDal)
        {
            _itemDal = itemDal;
            configItemConverter = new MapperConfiguration(cfg =>
                cfg.CreateMap<Item, ItemDTO>()
                    .ForMember(x => x.ItemId, s => s.MapFrom(p => p.ItemId))
                    .ReverseMap()
                    .ForMember(x => x.ItemId, s => s.MapFrom(p => p.ItemId))
            );
        }
        private ItemDTO convertItem(Item source)
        {
            var mapper = new Mapper(configItemConverter);
            ItemDTO item = mapper.Map<ItemDTO>(source);
            return item;
        }
        public List<ItemDTO> GetItems(string name = "") {        
            var items = _itemDal.getItems();
            List<ItemDTO> convertedList=new List<ItemDTO>();
            items.ForEach(item => convertedList.Add(convertItem(item)));
            return convertedList;
        }
    }
}
