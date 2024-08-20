using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.Client
{
    public sealed record UpdateClientInfoRequestDto(
        string? FirstName,
        string? LastName,
        string? Email,
        string? Phone,
        DateTime? BirthDate,
        UpdateAddressRequestDto Address);
}
