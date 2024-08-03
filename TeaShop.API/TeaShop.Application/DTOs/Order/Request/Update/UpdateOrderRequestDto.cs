using TeaShop.Application.DTOs.Customer.Request.Update;
using TeaShop.Application.DTOs.OrderDetails.Request.Update;

namespace TeaShop.Application.DTOs.Order.Request.Update
{
    public sealed record UpdateOrderRequestDto(
        UpdateCustomerRequestDto? Customer,
        UpdateOrderDetailsRequestDto? Details);
}
