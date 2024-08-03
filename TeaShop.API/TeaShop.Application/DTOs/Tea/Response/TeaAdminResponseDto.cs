using TeaShop.Application.DTOs.TeaType.Response;

namespace TeaShop.Application.DTOs.Tea.Response
{
    /// <summary>
    /// Dto for returning tea
    /// </summary>
    /// <remarks>
    /// Used for returning tea data to the admin
    /// </remarks>
    /// <param name="Id"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    /// <param name="UpdatedBy"></param>
    /// <param name="UpdatedAt"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Price"></param>
    /// <param name="IsInStock"></param>
    /// <param name="AvailableStock"></param>
    /// <param name="TeaType"></param>
    public sealed record TeaAdminResponseDto(
        Guid Id,
        string CreatedBy,
        DateTime CreatedAt,
        string? UpdatedBy,
        DateTime? UpdatedAt,

        string Name,
        string Description,
        decimal Price,
        bool IsInStock,
        int AvailableStock,
        TeaTypeResponseDto Type);
}
