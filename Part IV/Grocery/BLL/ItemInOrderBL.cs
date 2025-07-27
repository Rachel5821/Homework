using AutoMapper;
using DBEntities.Models;
using DTO;
using IBL;
using IDAL;

public class ItemInOrderBL : IItemInOrderBL
{
    private readonly IItemInOrderDal _itemInOrderDal;
    private readonly MapperConfiguration configItemInOrderConverter;

    public ItemInOrderBL(IItemInOrderDal itemInOrderDal)
    {
        _itemInOrderDal = itemInOrderDal;
        configItemInOrderConverter = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ItemInOrder, ItemInOrderDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemInOrderId))
                .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap()
                .ForMember(dest => dest.ItemInOrderId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity));
        });
    }

    public List<ItemInOrderDTO> GetItemsInOrder()
    {
        var itemsInOrder = _itemInOrderDal.GetItemsInOrder();
        if (itemsInOrder == null || itemsInOrder.Count == 0)
        {
            return new List<ItemInOrderDTO>();
        }

        List<ItemInOrderDTO> convertedList = new List<ItemInOrderDTO>();
        itemsInOrder.ForEach(m => convertedList.Add(convertItemsInOrder(m)));
        return convertedList;
    }

    public List<ItemInOrderDTO> GetItemsInOrderByOrderId(int orderId)
    {
        var itemsInOrder = _itemInOrderDal.GetItemsInOrderByOrderId(orderId);
        if (itemsInOrder == null || itemsInOrder.Count == 0)
        {
            return new List<ItemInOrderDTO>();
        }

        List<ItemInOrderDTO> convertedList = new List<ItemInOrderDTO>();
        itemsInOrder.ForEach(m => convertedList.Add(convertItemsInOrder(m)));
        return convertedList;
    }

    private ItemInOrderDTO convertItemsInOrder(ItemInOrder source)
    {
        var mapper = new Mapper(configItemInOrderConverter);
        ItemInOrderDTO itemsInOrders = mapper.Map<ItemInOrderDTO>(source);
        return itemsInOrders;
    }
}