using ValueObjects = TeaShop.Domain.ValueObjects;

namespace TeaShop.Application.DTOs.Identity.Response.Client
{
    public sealed record ClientInfoResponseDto(
        string FirstName,
        string LastName,
        string Role,
        string Email,
        string? Phone,
        DateTime BirthDate,
        ValueObjects.Address Address);
}
