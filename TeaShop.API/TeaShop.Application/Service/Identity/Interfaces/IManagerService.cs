using TeaShop.Application.DTOs.Identity.Request.Manager;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.DTOs.Identity.Response.Manager;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Interfaces
{
    public interface IManagerService
    {
        Task<Result<ManagerInfoResponseDto>> GetManagerInfo(Guid? id);
        Task<Result<IEnumerable<EmployeeInfoResponseDto>>> GetAllEmployees();
        Task<Result<EmployeeInfoResponseDto>> GetEmployeeById(Guid? id);
        Task<Result> UpdateEmployeeSalary(Guid? id, decimal newSalary, CancellationToken cancellationToken);
        Task<Result> UpdateManagerInfo(Guid? id, UpdateManagerInfoRequestDto request, CancellationToken cancellationToken);
    }
}
