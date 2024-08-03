namespace TeaShop.Application.DTOs.TeaItem.Request.Update
{
    public sealed record UpdateTeaItemRequestDto(
        Guid? TeaId,
        int? Quantity,
        double? Discount);
}
