using ValueObjects = TeaShop.Domain.ValueObjects;

namespace TeaShop.Application.DTOs.Identity.Response.Employee
{
    public sealed record EmployeeInfoResponseDto(
        string FirstName,
        string LastName,
        string Role,
        string Email,
        string? Phone,
        DateTime BirthDate,
        DateTime HireDate,
        ValueObjects.Address Address,
        decimal Salary);
}
