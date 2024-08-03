using TeaShop.Application.DTOs.TeaType.Response;

namespace TeaShop.Application.DTOs.Tea.Response
{
    /// <summary>
    /// Dto for returning tea
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Price"></param>
    /// <param name="IsInStock"></param>
    /// <param name="AvailableStock"></param>
    /// <param name="Type"></param>
    public sealed record TeaResponseDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        bool IsInStock,
        int AvailableStock,
        TeaTypeResponseDto Type);
}
