using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.TeaItem.Request.Add;
using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.OrderDetails.Request.Add
{
    /// <summary>
    /// Dto for creating or updating order details
    /// </summary>
    /// <param name="Items"></param>
    /// <param name="OrderedAt"></param>
    /// <param name="ShippingAddress"></param>
    /// <param name="Discount"></param>
    public sealed record AddOrderDetailsRequestDto(
        IEnumerable<AddTeaItemRequestDto?>? Items,
        DateTime? OrderedAt,
        AddAddressRequestDto? ShippingAddress,
        double? Discount,
        Status? Status);
}
