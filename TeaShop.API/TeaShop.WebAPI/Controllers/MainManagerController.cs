using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Identity.Request.MainManager;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.DTOs.Identity.Response.MainManager;
using TeaShop.Application.DTOs.Identity.Response.Manager;
using TeaShop.Application.Service.Identity.Interfaces;
using TeaShop.Identity.Service;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/main-manager")]
    [ApiController]
    [Authorize(Roles = "MainManager")]
    public class MainManagerController : Controller
    {
        private readonly IMainManagerService _mainManagerService;
        
        public MainManagerController(IMainManagerService mainManagerService)
        {
            _mainManagerService = mainManagerService;
        }

        [HttpGet]
        [Route("account")]
        [ProducesResponseType(200, Type = typeof(MainManagerInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetMainManagerInfo()
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _mainManagerService.GetMainManagerInfo(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mainManagerInfo = result.Value;

            return Ok(mainManagerInfo);
        }

        [HttpPut]
        [Route("account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateMainManagerInfo([FromBody] UpdateMainManagerInfoRequestDto request)
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _mainManagerService.UpdateMainManagerInfo(id, request, default);

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
            var result = await _mainManagerService.GetAllEmployees();

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
            var result = await _mainManagerService.GetEmployeeById(id);

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
            var result = await _mainManagerService.UpdateEmployeeSalary(id, newSalary, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpGet]
        [Route("all-managers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ManagerInfoResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllManagers()
        {
            var result = await _mainManagerService.GetAllManagers();

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("manager/{id}")]
        [ProducesResponseType(200, Type = typeof(ManagerInfoResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetManagerById(Guid id)
        {
            var result = await _mainManagerService.GetManagerById(id);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("manager/{id}/update-salary")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateManagerSalary([FromRoute] Guid id, [FromBody] decimal newSalary)
        {
            var result = await _mainManagerService.UpdateEmployeeSalary(id, newSalary, default);

            if (result.IsFailure && result.Errors.ToList()[0].Code == "User.UserNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
