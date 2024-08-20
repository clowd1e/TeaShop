using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Identity.Request.Manager;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.DTOs.Identity.Response.Manager;
using TeaShop.Application.Service.Identity.Interfaces;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/manager")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        [Route("account")]
        [ProducesResponseType(200, Type = typeof(ManagerInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetManagerInfo()
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _managerService.GetManagerInfo(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var managerInfo = result.Value;

            return Ok(managerInfo);
        }

        [HttpPut]
        [Route("account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateManagerInfo([FromBody] UpdateManagerInfoRequestDto request)
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _managerService.UpdateManagerInfo(id, request, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpGet]
        [Route("all-employees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeInfoResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _managerService.GetAllEmployees();

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("employee/{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var result = await _managerService.GetEmployeeById(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("employee/{id}/update-salary")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateEmployeeSalary([FromRoute] Guid id, [FromBody] decimal newSalary)
        {
            var result = await _managerService.UpdateEmployeeSalary(id, newSalary, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
