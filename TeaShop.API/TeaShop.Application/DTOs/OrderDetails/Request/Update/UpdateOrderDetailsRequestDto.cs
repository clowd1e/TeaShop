using TeaShop.Application.DTOs.Address.Request.Update;
using TeaShop.Application.DTOs.TeaItem.Request.Update;
using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.OrderDetails.Request.Update
{
    public sealed record UpdateOrderDetailsRequestDto(
        IEnumerable<UpdateTeaItemRequestDto?>? Items,
        DateTime? OrderedAt,
        UpdateAddressRequestDto? ShippingAddress,
        double? Discount,
        Status? Status);
}
