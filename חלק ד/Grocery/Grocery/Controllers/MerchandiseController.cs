using DTO;
using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchandiseBL marchandiseBL;
        public MerchandiseController(IMerchandiseBL marchandiseBL)
        {
            this.marchandiseBL = marchandiseBL;
        }

        // GET: api/<MerchandiseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MerchandiseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("marchandises")]
        public IEnumerable<MerchandiseDTO>GetMarchendises() {
            return marchandiseBL.GetMerchandises();
        }

        // POST api/<MerchandiseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MerchandiseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MerchandiseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
