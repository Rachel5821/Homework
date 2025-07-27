using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using IBL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailBL orderDetailBL;

        public OrderController(IOrderDetailBL orderDetailBL)
        {
            this.orderDetailBL=orderDetailBL;
        }


        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{supplierId}")]
        public ActionResult<IEnumerable<OrderDetailsDTO>> GetOrdersBySupplierId(int supplierId)
        {
            return Ok(orderDetailBL.GetOrdersBySupplierId(supplierId));
        }
        // GET api/<OrderController>/5
        [HttpGet("completed/{supplierId}")]
        public ActionResult<IEnumerable<OrderDetailsDTO>> GetOrdersBySupplierIdIfCompleted(int supplierId)
        {
            return Ok(orderDetailBL.GetOrdersBySupplierIdIfCompleted(supplierId));
        }
        [HttpGet("orders")]  // שינוי כאן
        public IEnumerable<OrderDetailsDTO> GETUser()//??
        {
            return orderDetailBL.GetOrders();
        }

        [HttpGet("Notcompleted/{supplierId}")]  // שינוי כאן
        public ActionResult<IEnumerable<OrderDetailsDTO>> GetOrdersBySupplierIdIfNotCompleted(int supplierId)
        {
            return orderDetailBL.GetOrdersBySupplierIdIfNotCompleted(supplierId);
        }
        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderController>/5
        // PUT api/<OrderController>/5
        [HttpPut("api/Order/{id}")]
        public IActionResult ConfirmOrder(int id)
        {
            try
            {
                orderDetailBL.ConfirmById(id);
                return Ok("Order confirmed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error confirming order: {ex.Message}");
            }
        }


        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
