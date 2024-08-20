using TeaShop.Domain.Enums;

namespace TeaShop.Application.DTOs.Order.Request.UpdateStatus
{
    public sealed record UpdateOrderStatusRequestDto(
        Status NewStatus);
}
