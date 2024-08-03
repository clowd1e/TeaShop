using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.DTOs.OrderDetails.Response;

namespace TeaShop.Application.DTOs.Order.Response
{
    /// <summary>
    /// Dto for returning order
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Customer"></param>
    /// <param name="Details"></param>
    public sealed record OrderResponseDto(
        Guid Id,
        CustomerResponseDto Customer,
        OrderDetailsResponseDto Details);
}
