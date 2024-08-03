using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Customer.Request.Update
{
    public sealed record UpdateCustomerRequestDto(
        string? FirstName,
        string? LastName,
        string? Phone,
        string? Email,
        UpdateAddressRequestDto? Address);
}
