using System.Linq;
using System.Threading.Tasks;
using CourseApp.Identity.Dtos;
using CourseApp.Identity.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CourseApp.Identity.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
            {
                UserName = signUpDto.UserName,
                City = signUpDto.City,
                Email = signUpDto.Email
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x=>x.Description).ToList());
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Sub));
            if (userIdClaim is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null) return BadRequest();
            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email });
        }
    }
}