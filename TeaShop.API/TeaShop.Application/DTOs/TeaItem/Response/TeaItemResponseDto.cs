using TeaShop.Application.DTOs.Tea.Response;

namespace TeaShop.Application.DTOs.TeaItem.Response
{
    public sealed record TeaItemResponseDto(
        TeaResponseDto Tea,
        int Quantity,
        double Discount,
        decimal TotalPrice);
}
