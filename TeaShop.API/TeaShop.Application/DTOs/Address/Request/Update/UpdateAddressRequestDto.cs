using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.Address.Request.Update
{
    public sealed record UpdateAddressRequestDto(
        Country? Country,
        string? City,
        string? Street,
        int? HouseNumber,
        string? PostalCode);
}
