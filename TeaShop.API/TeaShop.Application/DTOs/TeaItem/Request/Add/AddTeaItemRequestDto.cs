namespace TeaShop.Application.DTOs.TeaItem.Request.Add
{
    public sealed record AddTeaItemRequestDto(
        Guid? TeaId,
        int? Quantity,
        double? Discount);
}
