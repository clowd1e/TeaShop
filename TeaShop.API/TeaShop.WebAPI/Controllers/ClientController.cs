using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Identity.Request.Client;
using TeaShop.Application.DTOs.Identity.Response.Client;
using TeaShop.Application.Service.Identity.Interfaces;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("account")]
        [ProducesResponseType(200, Type = typeof(ClientInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetClientInfo()
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _clientService.GetClientInfo(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientInfo = result.Value;

            return Ok(clientInfo);
        }

        [HttpPut]
        [Route("account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateClientInfo([FromBody] UpdateClientInfoRequestDto request)
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _clientService.UpdateClientInfo(id, request, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
