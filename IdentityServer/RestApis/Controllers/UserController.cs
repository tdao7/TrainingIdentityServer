using System;
using System.Threading.Tasks;
using IdentityServer.Models.Form;
using IdentityServer.RestApis.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.RestApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginForm loginForm)
        {
            try
            {
                var tokenInfo = await _userServices.login(loginForm);
                return Ok(new { result = -1, message = tokenInfo });
            }
            catch (Exception e)
            {
                return BadRequest(new { result = -1, message = e.Message });
            }
        }
    }
}