using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.MainManager
{
    public sealed record UpdateMainManagerInfoRequestDto(
        string? FirstName,
        string? LastName,
        string? Email,
        string? Phone,
        DateTime? BirthDate,
        UpdateAddressRequestDto Address);
}
