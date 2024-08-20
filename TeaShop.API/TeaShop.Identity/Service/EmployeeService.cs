using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Identity.Request.Employee;
using TeaShop.Application.DTOs.Identity.Response.Employee;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors.Identity;
using TeaShop.Application.Service.Identity.Interfaces;
using TeaShop.Domain.ValueObjects;
using TeaShop.Identity.Database;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TeaShopIdentityDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(
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

        public async Task<Result<EmployeeInfoResponseDto>> GetEmployeeInfo(Guid? id)
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

        public async Task<Result> UpdateEmployeeInfo(Guid? id, UpdateEmployeeInfoRequestDto request, CancellationToken cancellationToken)
        {
            if (id is null)
                return Error.IdIsNull;

            var validation = new UpdateEmployeeInfoRequestDtoValidator().Validate(request);
            if (!validation.IsValid)
                return validation;

            var user = await _userManager.FindByIdAsync(id.ToString()!);
            if (user is null)
                return UserErrors.UserNotFound;

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee is null)
                return UserErrors.UserNotFound;

            #region Update user and employee info
            user.FirstName = request.FirstName!;
            user.LastName = request.LastName!;
            user.UserName = $"{request.FirstName}_{request.LastName}_{UserRole.Employee}";
            user.NormalizedUserName = user.UserName.ToUpper();
            user.Email = request.Email!;
            user.NormalizedEmail = user.Email.ToUpper();
            employee.Phone = request.Phone;
            employee.BirthDate = (DateTime)request.BirthDate!;
            employee.Address = _mapper.Map<Address>(request.Address);
            #endregion

            await _userManager.UpdateAsync(user);
            _context.Employees.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
