namespace TeaShop.Application.DTOs.TeaType.Response
{
    /// <summary>
    /// Dto for returning tea type
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public sealed record TeaTypeResponseDto(
        Guid Id,
        string Name,
        string Description);
}
