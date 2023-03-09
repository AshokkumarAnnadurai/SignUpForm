using AngularApi.Context;
using AngularApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace AngularApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("authenticate")]

        public async Task<IActionResult> Authenticate([FromBody] users userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            var user = await _authContext.Users
                .FirstOrDefaultAsync(x => x.username == userobj.username && x.Password == userobj.Password);
            if (user == null)
            {
                return NotFound(new { Message = "user not found!" });
            }
            return Ok(new { Message = "Login Success!" });
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] users userobj)
        {
            if (userobj == null)
                return BadRequest();

            //check username
            if (await CheckUserNameExistAsync(userobj.username))
                return BadRequest(new { Message = "username already exists!" });

            //check email
            if (await CheckEmailExistAsync(userobj.Email))
                return BadRequest(new { Message = "Email already exists!" });

            // password strength

            var pass = CheckPasswordStrength(userobj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            await _authContext.Users.AddAsync(userobj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "user Registered!"
            });
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            List<users> User_ = new List<users>();
            User_ = _authContext.Users.ToList();
            return Ok(User_);
        }

        /*[HttpPost]
        public async Task<IActionResult> createuser(users userobj)
        {
            await _authContext.Users.AddAsync(userobj);
            await _authContext.SaveChangesAsync();
            return Ok(userobj);
        }*/

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUsers([FromBody] users userobj)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(a => a.Id == userobj.Id);

            if (user != null)
            {
                user.Firstname = userobj.Firstname;
                user.Lastname = userobj.Lastname;
                user.Email = userobj.Email;
                user.username = userobj.username;
                user.Password = userobj.Password;
                await _authContext.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return NotFound("user not found");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(a => a.Id == id);

            if (user != null)
            {
                _authContext.Users.Remove(user);
                await _authContext.SaveChangesAsync();
                await _authContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('users', RESEED, 0)");
                return Ok(user);
            }
            else
            {
                return NotFound("user not found");
            }
        }

        private Task<bool> CheckUserNameExistAsync(string username)
            => _authContext.Users.AnyAsync(x => x.username == username);
        private Task<bool> CheckEmailExistAsync(string Email)
            => _authContext.Users.AnyAsync(x => x.Email == Email);

        private string CheckPasswordStrength(string Password)
        {
            StringBuilder sb = new StringBuilder(); 
            if(Password.Length < 8)
                sb.Append("Minimum password Length should be 8"+Environment.NewLine);
            if (!(Regex.IsMatch(Password, "[a-z]") && Regex.IsMatch(Password, "[A-Z]")
                && Regex.IsMatch(Password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            return sb.ToString();
        }
    }
}
