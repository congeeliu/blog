using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using webapi.Data;
using webapi.Models;
using webapi.utils;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly JwtHelper _jwtHelper;

        public UsersController(UserContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'UserContext.User'  is null.");
            }
            user.Password = Util.GetMD5(user.Password);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Post: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]User user)
        {
            if (_context.User == null)
            {
                return NotFound("用户不存在");
            }
            //var username = jsonObject["username"].ToString();
            var findUser = await _context.User.SingleOrDefaultAsync(u => u.Username == user.Username);

            if (findUser == null)
            {
                //return NotFound("用户不存在");
                throw new Exception("用户不存在");
            }

            if (findUser.Password != user.Password)
            {
                //return NotFound("密码错误");
                throw new Exception("密码错误");
            }

            //ISession session = HttpContext.Session;
            //session.SetInt32("id", user.Id);
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            //{
            //    session.SetString("username", user.Username);
            //}
                
            //session.SetString("role", user.Role);
            //Console.WriteLine("setName: " + user.Username);
            //var username = session.GetString("username");
            //Console.WriteLine("setName2: " + username);
            //return user;
            return _jwtHelper.CreateToken(user.Username, user.Role);
        }

        [HttpGet("token")]
        public ActionResult<string> GetToken()
        {
            var token = _jwtHelper.CreateToken("username", "role");
            return token;
        }

        //[Authorize]
        [HttpGet("info")]
        public ActionResult<string> GetInfo()
        {
            //ISession session = HttpContext.Session;
            //var getId = session.GetInt32("id");
            //int id = -1;
            //if (getId != null) id = (int)getId;
            //string? username = session.GetString("username");
            //Console.WriteLine(session.Get("username"));

            //var role = session.GetString("role");
            //User user = new User(id, username, null, role);
            //Console.WriteLine("getName: " + username);
            //var username = HttpContext.AuthenticateAsync().Result.Principal.Claims.First(claim => claim.Type == "username").Value;
            //Console.WriteLine("username: " + username);
            var name = HttpContext.User.Identity.Name;
            return name;
        }
    }
}
