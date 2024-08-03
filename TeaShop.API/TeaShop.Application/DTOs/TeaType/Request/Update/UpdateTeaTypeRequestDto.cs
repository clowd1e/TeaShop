namespace TeaShop.Application.DTOs.TeaType.Request.Update
{
    /// <summary>
    /// Dto for updating tea type
    /// </summary>
    /// <param name="UpdatedBy"></param>
    /// <param name="UpdatedAt"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public sealed record UpdateTeaTypeRequestDto(
        string UpdatedBy,
        DateTime UpdatedAt,

        string? Name,
        string? Description);
}
