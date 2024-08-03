using TeaShop.Application.DTOs.Address.Request.Add;

namespace TeaShop.Application.DTOs.Customer.Request.Add
{
    /// <summary>
    /// Dto for creating or updating customer
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Phone"></param>
    /// <param name="Email"></param>
    /// <param name="Address"></param>
    public sealed record AddCustomerRequestDto(
        string? FirstName,
        string? LastName,
        string? Phone,
        string? Email,
        AddAddressRequestDto? Address);
}
