using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.DTOs.TeaType.Request.Add;
using TeaShop.Application.DTOs.TeaType.Request.Remove;
using TeaShop.Application.DTOs.TeaType.Request.Update;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.Service.TeaType.Command.AddTeaType;
using TeaShop.Application.Service.TeaType.Command.RemoveTeaType;
using TeaShop.Application.Service.TeaType.Command.UpdateTeaType;
using TeaShop.Application.Service.TeaType.Query.GetAllTeaTypes;
using TeaShop.Application.Service.TeaType.Query.GetAllTeaTypesAdmin;
using TeaShop.Application.Service.TeaType.Query.GetTeaByTeaType;
using TeaShop.Application.Service.TeaType.Query.GetTeaByTeaTypeAdmin;
using TeaShop.Application.Service.TeaType.Query.GetTeaTypeById;
using TeaShop.Application.Service.TeaType.Query.GetTeaTypeByIdAdmin;
using TeaShop.Domain.Exceptions.TeaType;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/teaType")]
    [ApiController]
    public class TeaTypeController : Controller
    {
        private readonly ISender _sender;

        public TeaTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaTypeResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTeaTypesAsync()
        {
            var result = await _sender.Send(new GetAllTeaTypesQuery());

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teaType = result.Value;

            return Ok(teaType);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(TeaTypeResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaTypeByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaTypeByIdQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teaType = result.Value;

            return Ok(teaType);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTeaTypeAsync([FromBody] AddTeaTypeRequestDto request)
        {
            var result = await _sender.Send(new AddTeaTypeCommand(request));

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
        public async Task<IActionResult> RemoveTeaTypeAsync([FromRoute] Guid? id)
        {
            var result = await _sender.Send(new RemoveTeaTypeCommand(
                    new RemoveTeaTypeRequestDto(id)
                ));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
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
        public async Task<IActionResult> UpdateTeaTypeAsync([FromRoute] Guid? id, [FromBody] UpdateTeaTypeRequestDto request)
        {
            var result = await _sender.Send(new UpdateTeaTypeCommand(id, request));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);
            else if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully updated record.");        
        }

        [HttpGet]
        [Route("{id}/tea")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaByTeaTypeAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaByTeaTypeQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
        }

        [HttpGet]
        [Route("admin/all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaTypeAdminResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTeaTypesAdminAsync()
        {
            var result = await _sender.Send(new GetAllTeaTypesAdminQuery());

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teaType = result.Value;

            return Ok(teaType);
        }

        [HttpGet]
        [Route("admin/{id}")]
        [ProducesResponseType(200, Type = typeof(TeaTypeAdminResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaTypeByIdAdminAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaTypeByIdAdminQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teaType = result.Value;

            return Ok(teaType);
        }

        [HttpGet]
        [Route("admin/{id}/tea")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeaAdminResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeaByTeaTypeAdminAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetTeaByTeaTypeAdminQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "TeaType.TeaTypeNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tea = result.Value;

            return Ok(tea);
        }

    }
}
