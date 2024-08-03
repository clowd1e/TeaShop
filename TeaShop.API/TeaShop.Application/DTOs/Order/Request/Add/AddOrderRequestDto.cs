using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.DTOs.OrderDetails.Request.Add;

namespace TeaShop.Application.DTOs.Order.Request.Add
{
    /// <summary>
    /// Dto for creating or updating order
    /// </summary>
    /// <param name="Customer"></param>
    /// <param name="Details"></param>
    public sealed record AddOrderRequestDto(
        AddCustomerRequestDto? Customer,
        AddOrderDetailsRequestDto? Details);
}
