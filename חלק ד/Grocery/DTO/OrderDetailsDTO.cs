namespace DTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public int? Quantity { get; set; }
        public string? order_status { get; set; }
        public int MerchandiseId { get; set; }
        public List<ItemInOrderDTO> orderItems { get; set; }
    }
}
