using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Identity.Request;
using TeaShop.Application.DTOs.Identity.Response;
using TeaShop.Application.Service.Identity.Interfaces;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthService _authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var result = await _authenticationService.LoginAsync(request);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Login.EmailNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (result.IsFailure)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Register([FromBody] RegRequestDto request)
        {
            var result = await _authenticationService.RegisterClientAsync(request);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Register.EmailAlreadyExists")
                return BadRequest(result.Errors.ToList()[0].Message);

            if (result.IsFailure)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
