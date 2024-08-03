namespace TeaShop.Application.DTOs.Tea.Request.Update
{
    /// <summary>
    /// Dto for updating tea
    /// </summary>
    /// <param name="UpdatedBy"></param>
    /// <param name="UpdatedAt"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Price"></param>
    /// <param name="IsInStock"></param>
    /// <param name="AvailableStock"></param>
    /// <param name="TeaTypeId"></param>
    public sealed record UpdateTeaRequestDto(
        string UpdatedBy,
        DateTime UpdatedAt,

        string? Name,
        string? Description,
        decimal? Price,
        bool? IsInStock,
        int? AvailableStock,
        Guid? TeaTypeId);
}
