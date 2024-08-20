using ValueObjects = TeaShop.Domain.ValueObjects;

namespace TeaShop.Application.DTOs.Identity.Response.Manager
{
    public sealed record ManagerInfoResponseDto(
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
