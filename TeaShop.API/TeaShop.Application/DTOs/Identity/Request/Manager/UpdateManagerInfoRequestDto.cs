using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.Manager
{
    public sealed record UpdateManagerInfoRequestDto(
        string? FirstName,
        string? LastName,
        string? Email,
        string? Phone,
        DateTime? BirthDate,
        UpdateAddressRequestDto Address);
}
