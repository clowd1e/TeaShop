using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.Employee
{
    public sealed record UpdateEmployeeInfoRequestDto(
        string? FirstName,
        string? LastName,
        string? Email,
        string? Phone,
        DateTime? BirthDate,
        UpdateAddressRequestDto Address);
}
