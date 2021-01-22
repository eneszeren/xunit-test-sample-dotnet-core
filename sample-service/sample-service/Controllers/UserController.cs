using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sample_service.Dtos;
using sample_service.Entities.Sample;
using sample_service.Helpers;
using sample_service.Services;

namespace sample_service.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _serviceUser;
        private readonly AppSettings _appSettings;
        readonly SampleDbContext _context;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings, SampleDbContext context)
        {
            _serviceUser = userService;
            _appSettings = appSettings.Value;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDto.LoginRequest loginDto)
        {

            try
            {
                if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                    return BadRequest("Login or password can not be empty!");

                GeneralDto.Response response = null;
                response = _serviceUser.Login(loginDto, Request);

                if (response == null || response.Error)
                    return BadRequest("Login or password is incorrect");


                UserDto.Authorized user = (UserDto.Authorized)response.Data;

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                List<Claim> claimsList = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Name)
                };

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claimsList.ToArray()),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(token);

                user.Token = tokenString;

                return Ok(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("Authorized")]
        public IActionResult Authorized()
        {
            return Ok(new GeneralDto.Response());
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(_serviceUser.List());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Detail")]
        public IActionResult Detail(GeneralDto.Detail detail)
        {
            try
            {
                return Ok(_serviceUser.Detail(detail));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete(GeneralDto.Delete delete)
        {
            try
            {
                delete.UserId = User.Identity.Name.ToInt();

                return Ok(_serviceUser.Delete(delete));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("Save")]
        public IActionResult Save(UserDto.Save save)
        {
            try
            {
                save.UserId = User.Identity.Name.ToInt();

                return Ok(_serviceUser.Save(save, Request));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("SelectUserList")]
        public IActionResult SelectUserList()
        {
            try
            {
                return Ok(_serviceUser.SelectList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("GetUserEmail/{userId}")]
        public IActionResult GetUserEmail(int userId)
        {
            try
            {
                return Ok(_serviceUser.GetUserGetUserEmail(userId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
