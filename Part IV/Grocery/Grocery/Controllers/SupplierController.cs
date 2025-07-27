using Microsoft.AspNetCore.Mvc;
using BLL;
using DBEntities.Models;
using DTO;
using IBL;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierBL supplierBL;
        public SupplierController(ISupplierBL supplierBL)
        {
            this.supplierBL = supplierBL;
        }

        // GET: api/<SupplierController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("suppliers")] 
        public IEnumerable<SupplierDTO> GetSuppliers()
        {
            return supplierBL.GetSuppliers();
        }

        // POST api/<SupplierController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
