using FamilyDataCollector.Data.Models;
using FamilyDataCollector.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FamilyDataCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager , IConfiguration conf ,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = conf;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> RegisterNewUser(DTONewUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser appuser = new() { UserName = user.userName, Email = user.email, PhoneNumber = user.phoneNumber };
                IdentityResult result = await _userManager.CreateAsync(appuser, user.password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(user.role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(user.role));
                    }

                    
                    await _userManager.AddToRoleAsync(appuser, user.role);

                    return Ok(new { message = $"User created with role {user.role}" });
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(DTOLogIn Login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Login.UserName);
                if(user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, Login.Password))
                    {
                        var claims = new List<Claim>();
                        //claims in the payload 
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }
                        //sininCredantials
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddDays(1),
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
                    ModelState.AddModelError("", "User Name is invalid");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
