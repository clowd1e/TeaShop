namespace TeaShop.Application.DTOs.TeaType.Request.Remove
{
    /// <summary>
    /// Dto for deleting tea type
    /// </summary>
    /// <param name="Id"></param>
    public sealed record RemoveTeaTypeRequestDto(
        Guid? Id);
}
