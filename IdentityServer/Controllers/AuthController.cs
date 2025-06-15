using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Threading.Tasks;
using static Duende.IdentityServer.IdentityServerConstants;

namespace IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);


            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(e => e.Description).ToList(), 400));
            }

            return NoContent();
        }
    }
}
