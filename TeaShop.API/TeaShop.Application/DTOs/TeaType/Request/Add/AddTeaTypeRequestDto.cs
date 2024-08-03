namespace TeaShop.Application.DTOs.TeaType.Request.Add
{
    /// <summary>
    /// Dto for adding tea type
    /// </summary>
    /// <param name="CreatedAt"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public sealed record AddTeaTypeRequestDto(
        string CreatedBy,
        DateTime CreatedAt,

        string? Name,
        string? Description);
}
