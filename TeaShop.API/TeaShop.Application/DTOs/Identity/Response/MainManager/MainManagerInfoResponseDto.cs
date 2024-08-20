using ValueObjects = TeaShop.Domain.ValueObjects;

namespace TeaShop.Application.DTOs.Identity.Response.MainManager
{
    public sealed record MainManagerInfoResponseDto(
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
