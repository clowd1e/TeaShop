using TeaShop.Application.DTOs.Address.Response;
using TeaShop.Application.DTOs.TeaItem.Response;
using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.OrderDetails.Response
{
    /// <summary>
    /// Dto for returning order details
    /// </summary>
    /// <param name="Items"></param>
    /// <param name="OrderedAt"></param>
    /// <param name="ShippingAddress"></param>
    /// <param name="Discount"></param>
    /// <param name="TotalPrice"></param>
    public sealed record OrderDetailsResponseDto (
        IEnumerable<TeaItemResponseDto> Items,
        DateTime OrderedAt,
        AddressResponseDto ShippingAddress,
        double Discount,
        decimal TotalPrice,
        Status Status);
}
