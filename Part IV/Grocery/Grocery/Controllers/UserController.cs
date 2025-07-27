using Microsoft.AspNetCore.Mvc;
using BLL;
using DBEntities.Models;
using DTO;
using IBL;
using DAL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        // GET: api/<UserController>/names
        [HttpGet("names")]  // שינוי כאן
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/<UserController>/users
        [HttpGet("users")]  // שינוי כאן
        public IEnumerable<UserDTO> GETUser()
        {
            return userBL.GETUser();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            try
            {
                await userBL.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GETUser), new { id = userDto.Id }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while creating user: " + ex.Message);
            }
        }

        [HttpGet("login/{userName}/{password}")]
        public IActionResult LogIn(string userName, string password)
        {
            var userDto = userBL.LogIn(userName, password);

            if (userDto == null)
            {
                return Unauthorized();
            }

            return Ok(userDto);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
