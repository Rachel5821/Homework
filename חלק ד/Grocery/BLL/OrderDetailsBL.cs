using AutoMapper;
using DBEntities.Models;
using DTO;
using IBL;
using IDAL;
using Microsoft.Data.SqlClient;

public class OrderDetailsBL : IOrderDetailBL
{
    private readonly IOrderDetailsDal _orderDetailsDal;
    private readonly MapperConfiguration configOrderConverter;

    public OrderDetailsBL(IOrderDetailsDal orderDetailsDal)
    {
        _orderDetailsDal = orderDetailsDal;
        configOrderConverter = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<OrderDetail, OrderDetailsDTO>()
                .ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.OrderItems)) // זה המיפוי הנכון
                .ReverseMap();

            cfg.CreateMap<ItemInOrder, ItemInOrderDTO>();  // המיפוי של הפריטים
        });
    }


    public List<OrderDetailsDTO> GetOrders()
    {
        var orders = _orderDetailsDal.GetOrders();
        if (orders == null || orders.Count == 0)
        {
            return new List<OrderDetailsDTO>();
        }

        List<OrderDetailsDTO> convertedList = new List<OrderDetailsDTO>();
        orders.ForEach(o => convertedList.Add(convertOrder(o)));
        return convertedList;
    }

    public List<OrderDetailsDTO> GetOrdersBySupplierId(int supplierId)
    {
        var orders = _orderDetailsDal.GetOrdersBySupplierId(supplierId);
        if (orders == null || orders.Count == 0)
        {
            return new List<OrderDetailsDTO>();
        }

        List<OrderDetailsDTO> convertList = new List<OrderDetailsDTO>();
        orders.ForEach(o => convertList.Add(convertOrder(o)));
        return convertList;
    }
    public List<OrderDetailsDTO> GetOrdersBySupplierIdIfCompleted(int supplierId)
    {
        var orders = GetOrdersBySupplierId(supplierId);
        return orders.Where(o => o.order_status == "הושלמה").ToList();
    }
    public List<OrderDetailsDTO> GetOrdersBySupplierIdIfNotCompleted(int supplierId)
    {
        var orders = GetOrdersBySupplierId(supplierId);
        return orders.Where(o => o.order_status == "נוצרה").ToList();
    }
    public void ConfirmById(int id)
    {
        _orderDetailsDal.ConfirmById(id); // קריאה לפונקציה המתאימה ב־DAL
    }

    private OrderDetailsDTO convertOrder(OrderDetail source)
    {
        var mapper = new Mapper(configOrderConverter);
        OrderDetailsDTO order = mapper.Map<OrderDetailsDTO>(source);
        return order;
    }
}