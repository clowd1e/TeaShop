using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Identity.Request.Employee;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.Service.Identity.Interfaces;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("account")]
        [ProducesResponseType(200, Type = typeof(EmployeeInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _employeeService.GetEmployeeInfo(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeInfo = result.Value;

            return Ok(employeeInfo);
        }

        [HttpPut]
        [Route("account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateEmployeeInfo([FromBody] UpdateEmployeeInfoRequestDto request)
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _employeeService.UpdateEmployeeInfo(id, request, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
