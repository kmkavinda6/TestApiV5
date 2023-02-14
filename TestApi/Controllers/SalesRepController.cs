using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TestApi.Data;
using TestApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRepController : ControllerBase
    {
        private readonly WebApiDbContext _context;
        private readonly IConfiguration _configuration;

        public SalesRepController(WebApiDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/<AuthenticationController>
        /*[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/<AuthenticationController>/5
        /* [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }*/

        // POST api/<AuthenticationController>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SalesRep model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Add the user to the database
            _context.SalesReps.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _context.SalesReps.FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.SalesRepID.ToString()),
          
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return token and user details
            return Ok(new
            {
                token = tokenString,
                user = new
                {
                    user.SalesRepID,
                    user.UserName,
                    user.Lname,
                    user.FName,              
                    user.Address,
                    user.Nic,
                    user.PhoneNo,
                    user.Email,
                    
                }
            });
        }

        // GET: api/SalesRepstest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesRep>>> GetSalesReps()
        {
            return await _context.SalesReps.ToListAsync();
        }

        // GET: api/SalesRepstest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesRep>> GetSalesRep(int id)
        {
            var salesRep = await _context.SalesReps.FindAsync(id);

            if (salesRep == null)
            {
                return NotFound();
            }

            return salesRep;
        }

        // PUT: api/SalesRepstest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesRep(int id, SalesRep salesRep)
        {
            if (id != salesRep.SalesRepID)
            {
                return BadRequest();
            }

            _context.Entry(salesRep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRepExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalesRepstest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesRep>> PostSalesRep(SalesRep salesRep)
        {
            _context.SalesReps.Add(salesRep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesRep", new { id = salesRep.SalesRepID }, salesRep);
        }

        // DELETE: api/SalesRepstest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesRep(int id)
        {
            var salesRep = await _context.SalesReps.FindAsync(id);
            if (salesRep == null)
            {
                return NotFound();
            }

            _context.SalesReps.Remove(salesRep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesRepExists(int id)
        {
            return _context.SalesReps.Any(e => e.SalesRepID == id);
        }


        
    }
}
