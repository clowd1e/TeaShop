using TeaShop.Application.DTOs.Address.Response;

namespace TeaShop.Application.DTOs.Customer.Response
{
    /// <summary>
    /// Dto for returning customer
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Email"></param>
    /// <param name="Address"></param>
    public sealed record CustomerResponseDto(
        Guid Id,
        string FirstName,
        string LastName,
        string? Phone,
        string? Email,
        AddressResponseDto Address);
}
