using Api_Compta.Interfaces;
using Api_Compta.Models.Dtos;
using Api_Compta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Compta.Controllers
{
    
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly IContextService _contextService;
        public AuthController(AuthService auth, IContextService contextService)
        {
            _auth = auth;
            _contextService = contextService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _auth.LoginAsync(request.Login, request.Password);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("context")]
        public async Task<ActionResult<UserContextDto>> GetContext()
        {
            var userId = Guid.Parse(User.FindFirst("uid")!.Value);

            var context = await _contextService.GetUserContextAsync(userId);

            return Ok(context);
        }
    }
}
