using Microsoft.AspNetCore.Mvc;
using CodeshopApi.Identity;
using Microsoft.AspNetCore.Identity;
using CodeshopApi.Models.Auth;
using CodeshopApi.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CodeshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UserManager<ApplicationUser> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, UserManager<ApplicationUser> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExist = await userManager.FindByNameAsync(model.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Responce { Status = "Error", Message = "User already exist" });
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Responce { Status = "Error", Message = "User creation failed" });
            }
            return Ok(new Responce { Message = "user created successfully", Status = "Success" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>Login([FromBody] Login model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach(var userrole in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userrole.ToString()));
                }
                var AuthSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var tocken = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires:DateTime.Now.AddHours(2),
                    claims :authClaims,
                    signingCredentials:new SigningCredentials(AuthSignKey,SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    tocken = new JwtSecurityTokenHandler().WriteToken(tocken),

                });
               
            }
            return Unauthorized();
        }
    }
}
