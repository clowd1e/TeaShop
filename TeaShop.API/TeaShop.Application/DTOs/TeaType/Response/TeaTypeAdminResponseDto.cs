namespace TeaShop.Application.DTOs.TeaType.Response
{
    /// <summary>
    /// Dto for returning tea type
    /// </summary>
    /// <remarks>
    /// Used for returning tea type data to the admin
    /// </remarks>
    /// <param name="Id"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    /// <param name="UpdatedBy"></param>
    /// <param name="UpdatedAt"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public sealed record TeaTypeAdminResponseDto(
        Guid Id,
        string CreatedBy,
        DateTime CreatedAt,
        string? UpdatedBy,
        DateTime? UpdatedAt,

        string Name,
        string Description);
}
