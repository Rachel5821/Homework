
using DBEntities.Models;
using DTO;
using IDAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class OrderDetailDal : IOrderDetailsDal
{
    public List<OrderDetail> GetOrders()
    {
        try
        {
            using (D_Context context = new D_Context())
            {
                return context.OrderDetail
               .Include(o => o.OrderItems)
               .ToList();
            }
        }
        catch (SqlException ex)
        {

            Console.WriteLine($"SQL Inner Exception: {ex.InnerException?.Message}");

            return new List<OrderDetail>();

        }
    }

    public List<OrderDetail> GetOrdersBySupplierId(int supplierId)
    {
        try
        {
            using (D_Context context = new D_Context())
            {
                return context.OrderDetail
                    .Where(o => o.SupplierId == supplierId)
                        .Include(o => o.OrderItems)
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            return new List<OrderDetail>();
        }
    }
    public List<OrderDetail> GetOrdersBySupplierIdIfCompleted(int supplierId)
    {
        try
        {
            using (D_Context context = new D_Context())
                return context.OrderDetail.Where(o => o.SupplierId == supplierId && o.order_status == "הושלמה").ToList();


        }

        catch (Exception ex)
        {
            return new List<OrderDetail>();
        }
    }
    public List<OrderDetail> GetOrdersBySupplierIdIfNotCompleted(int supplierId)
    {
        try
        {
            using (D_Context context = new D_Context())
                return context.OrderDetail.Where(o => o.SupplierId == supplierId && o.order_status == "נוצרה").ToList();


        }

        catch (Exception ex)
        {
            return new List<OrderDetail>();
        }
    }
    public void ConfirmById(int id)
    {
        try
        {
            using (D_Context ctx = new D_Context())
            {
                var order = ctx.OrderDetail.FirstOrDefault(o => o.OrderId == id);
                if (order != null)
                {
                    order.order_status = "בתהליך"; // שינוי הסטטוס
                    ctx.SaveChanges(); // שמירה למסד הנתונים
                }
                else
                {
                    Console.WriteLine($"הזמנה עם מזהה {id} לא נמצאה.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("שגיאה באישור ההזמנה: " + ex.Message);
        }
    }

}