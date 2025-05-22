using BLL;
using DTO;
using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInOrderController : ControllerBase
    {
        private readonly IItemInOrderBL itemInOrderBL;

        public ItemInOrderController(IItemInOrderBL itemInOrderBL)
        {
            this.itemInOrderBL = itemInOrderBL;
        }

        [HttpGet("itemsInOrder")]
        public IEnumerable<ItemInOrderDTO> GetItemsInOrder()
        {
            return itemInOrderBL.GetItemsInOrder(); 
        }
        [HttpGet("itemsInOrderByOrderId/{orderId}")]
        public IEnumerable<ItemInOrderDTO> GetItemsInOrderByOrderId(int orderId)
        {
            return itemInOrderBL.GetItemsInOrderByOrderId(orderId);
        }


        // GET: api/<ItemInOrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ItemInOrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemInOrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemInOrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemInOrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
