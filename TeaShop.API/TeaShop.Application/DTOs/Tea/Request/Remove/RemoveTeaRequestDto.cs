namespace TeaShop.Application.DTOs.Tea.Request.Remove
{
    /// <summary>
    /// Dto for deleting tea
    /// </summary>
    /// <param name="Id"></param>
    public sealed record RemoveTeaRequestDto(
        Guid? Id);
}
