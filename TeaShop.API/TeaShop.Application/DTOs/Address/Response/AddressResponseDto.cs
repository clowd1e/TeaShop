using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.Address.Response
{
    /// <summary>
    /// Dto for returning address
    /// </summary>
    /// <param name="Country"></param>
    /// <param name="City"></param>
    /// <param name="Street"></param>
    /// <param name="HouseNumber"></param>
    /// <param name="PostalCode"></param>
    public sealed record AddressResponseDto(
        Country Country,
        string City,
        string Street,
        int? HouseNumber,
        string PostalCode);
}
