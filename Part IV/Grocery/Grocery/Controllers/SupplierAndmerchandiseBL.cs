using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAndmerchandiseBL : ControllerBase
    {
        private readonly ISupplierAndmerchandiseBL supplierAndmerchandiseBL;
        public SupplierAndmerchandiseBL(ISupplierAndmerchandiseBL supplierAndmerchandiseBL)
        {
           this.supplierAndmerchandiseBL= supplierAndmerchandiseBL;
        }
        // GET: api/<SupplierAndmerchandiseBL>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SupplierAndmerchandiseBL>/5
        [HttpGet("{id}")]
        public List<DTO.MerchandiseDTO> GetMerchandiseBySupplier(int id)
        {
            return supplierAndmerchandiseBL.GetMerchandiseBySupplier(id);
        }


        // POST api/<SupplierAndmerchandiseBL>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SupplierAndmerchandiseBL>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SupplierAndmerchandiseBL>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
