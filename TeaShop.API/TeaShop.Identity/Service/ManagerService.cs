using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Identity.Request.Manager;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.DTOs.Identity.Response.Manager;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors.Identity;
using TeaShop.Application.Service.Identity.Interfaces;
using TeaShop.Domain.ValueObjects;
using TeaShop.Identity.Database;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Service
{
    public sealed class ManagerService : IManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TeaShopIdentityDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(
            UserManager<ApplicationUser> userManager,
            TeaShopIdentityDbContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmployeeInfoResponseDto>>> GetAllEmployees()
        {
            var result = new List<EmployeeInfoResponseDto>();

            var users = await _userManager.GetUsersInRoleAsync(UserRole.Employee.ToString());
            foreach (var user in users)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id.ToString() == user.Id);
                if (employee is null)
                    return UserErrors.UserNotFound;

                result.Add(new EmployeeInfoResponseDto(
                    user.FirstName, user.LastName, user.Role, user.Email,
                    employee.Phone, employee.BirthDate, employee.HireDate, employee.Address, employee.Salary));
            }

            return Result<IEnumerable<EmployeeInfoResponseDto>>.Ok(result);
        }

        public async Task<Result<EmployeeInfoResponseDto>> GetEmployeeById(Guid? id)
        {
            if (id is null)
                return Error.IdIsNull;

            var user = await _userManager.FindByIdAsync(id.ToString()!);

            if (user is null)
                return UserErrors.UserNotFound;

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
                return UserErrors.UserNotFound;

            var result = new EmployeeInfoResponseDto(
                user.FirstName, user.LastName, user.Role, user.Email,
                employee.Phone, employee.BirthDate, employee.HireDate, employee.Address, employee.Salary);

            return result.ToResult();
        }

        public async Task<Result<ManagerInfoResponseDto>> GetManagerInfo(Guid? id)
        {
            if (id is null)
                return Error.IdIsNull;

            var user = await _userManager.FindByIdAsync(id.ToString()!);
            if (user is null)
                return UserErrors.UserNotFound;

            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == id);
            if (manager is null)
                return UserErrors.UserNotFound;

            var result = new ManagerInfoResponseDto(
                user.FirstName, user.LastName, user.Role, user.Email,
                manager.Phone, manager.BirthDate, manager.HireDate, manager.Address, manager.Salary);

            return result.ToResult();
        }

        public async Task<Result> UpdateEmployeeSalary(Guid? id, decimal newSalary, CancellationToken cancellationToken)
        {
            if (id is null)
                return Error.IdIsNull;

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
                return UserErrors.UserNotFound;

            employee.Salary = newSalary;

            _context.Update(employee);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        public async Task<Result> UpdateManagerInfo(Guid? id, UpdateManagerInfoRequestDto request, CancellationToken cancellationToken)
        {
            if (id is null)
                return Error.IdIsNull;

            var validation = new UpdateManagerInfoRequestDtoValidator().Validate(request);
            if (!validation.IsValid)
                return validation;

            var user = await _userManager.FindByIdAsync(id.ToString()!);
            if (user is null)
                return UserErrors.UserNotFound;

            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == id);
            if (manager is null)
                return UserErrors.UserNotFound;

            #region Update user and manager info
            user.FirstName = request.FirstName!;
            user.LastName = request.LastName!;
            user.UserName = $"{request.FirstName}_{request.LastName}_{UserRole.Manager}";
            user.NormalizedUserName = user.UserName.ToUpper();
            user.Email = request.Email!;
            user.NormalizedEmail = user.Email.ToUpper();
            manager.Address = _mapper.Map<Address>(request.Address);
            manager.Phone = request.Phone;
            manager.BirthDate = (DateTime)request.BirthDate!;
            #endregion

            _context.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
