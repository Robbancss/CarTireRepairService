using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.DTO;
using Persistence.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Client> _signInManager;

        public AccountController(SignInManager<Client> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized("Login failed!");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}
