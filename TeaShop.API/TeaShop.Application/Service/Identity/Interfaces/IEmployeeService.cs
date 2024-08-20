using TeaShop.Application.DTOs.Identity.Request.Employee;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result<EmployeeInfoResponseDto>> GetEmployeeInfo(Guid? id);
        Task<Result> UpdateEmployeeInfo(Guid? id, UpdateEmployeeInfoRequestDto request, CancellationToken cancellationToken);
    }
}
