using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Tea.Request.Add;
using TeaShop.Application.DTOs.Tea.Request.Remove;
using TeaShop.Application.DTOs.Tea.Request.Update;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.Service.Tea.Command.AddTea;
using TeaShop.Application.Service.Tea.Command.RemoveTea;
using TeaShop.Application.Service.Tea.Command.UpdateTea;
using TeaShop.Application.Service.Tea.Query.GetAllTea;
using TeaShop.Application.Service.Tea.Query.GetAllTeaAdmin;
using TeaShop.Application.Service.Tea.Query.GetTeaById;
using TeaShop.Application.Service.Tea.Query.GetTeaByIdAdmin;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/tea")]
    [ApiController]
    public class TeaController : Controller
    {
        private readonly ISender _sender;

        public TeaController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTeaAsync()
        {
            var result = await _sender.Send(new GetAllTeaQuery());

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
            
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(TeaResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaByIdQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTeaAsync([FromBody] AddTeaRequestDto request)
        {
            var result = await _sender.Send(new AddTeaCommand(request));

            if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Successfully added record with name \"{request.Name}\"");
            
        }

        [HttpDelete]
        [Route("{id}/remove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveTeaAsync([FromRoute] Guid? id)
        {
            var result = await _sender.Send(new RemoveTeaCommand(
                    new RemoveTeaRequestDto(id)
                ));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);
            else if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully removed record.");
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTeaAsync([FromRoute] Guid? id, [FromBody] UpdateTeaRequestDto request)
        {
            var result = await _sender.Send(new UpdateTeaCommand(id, request));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);
            else if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully updated record.");
        }

        [HttpGet]
        [Route("admin/all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaAdminResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTeaAdminAsync()
        {
            var result = await _sender.Send(new GetAllTeaAdminQuery());

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
        }

        [HttpGet]
        [Route("admin/{id}")]
        [ProducesResponseType(200, Type = typeof(TeaAdminResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaByIdAdminAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaByIdAdminQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Tea.TeaNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
        }
    }
}
