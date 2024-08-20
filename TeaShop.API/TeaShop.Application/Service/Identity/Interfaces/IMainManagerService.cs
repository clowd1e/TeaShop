using TeaShop.Application.DTOs.Identity.Request.MainManager;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.DTOs.Identity.Response.MainManager;
using TeaShop.Application.DTOs.Identity.Response.Manager;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Interfaces
{
    public interface IMainManagerService
    {
        Task<Result<MainManagerInfoResponseDto>> GetMainManagerInfo(Guid? id);
        Task<Result<IEnumerable<EmployeeInfoResponseDto>>> GetAllEmployees();
        Task<Result<IEnumerable<ManagerInfoResponseDto>>> GetAllManagers();
        Task<Result<EmployeeInfoResponseDto>> GetEmployeeById(Guid? id);
        Task<Result<ManagerInfoResponseDto>> GetManagerById(Guid? id);
        Task<Result> UpdateEmployeeSalary(Guid? id, decimal newSalary, CancellationToken cancellationToken);
        Task<Result> UpdateManagerSalary(Guid? id, decimal newSalary, CancellationToken cancellationToken);
        Task<Result> UpdateMainManagerInfo(Guid? id, UpdateMainManagerInfoRequestDto request, CancellationToken cancellationToken);
    }
}
