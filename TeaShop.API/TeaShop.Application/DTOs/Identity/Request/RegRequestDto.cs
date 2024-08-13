using TeaShop.Application.DTOs.Address.Request.Add;

namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed record RegRequestDto(
        string? FirstName,
        string? LastName,
        string? Password,
        string? RepeatPassword,
        string? Phone,
        string? Email,
        AddAddressRequestDto? Address,
        DateTime? BirthDate);
}
