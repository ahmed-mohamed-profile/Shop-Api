using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shop.Api.Data.Models;
using Shop.Api.Models;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(UserDto userDto)
        {
            if(ModelState.IsValid)
            {
               User user = new User
               {
                   UserName = userDto.Username,
                   Email = userDto.Email
               };
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if(result.Succeeded)
                {
                    return Ok("success");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest(ModelState);
                }


            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto Login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Login.Username);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, Login.Password))
                    {
                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var sc = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken
                        (
                            claims: claims,
                            issuer: configuration["Jwt:Issuer"],
                            audience: configuration["Jwt:Audience"],
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: sc
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return BadRequest(ModelState);
        }

    }
}
