namespace TeaShop.Application.DTOs.Tea.Request.Add
{
        /// <summary>
        /// Dto for creating tea
        /// </summary>
        /// <param name="CreatedBy"></param>
        /// <param name="CreatedAt"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="Price"></param>
        /// <param name="IsInStock"></param>
        /// <param name="AvailableStock"></param>
        /// <param name="TeaTypeId"></param>
    public sealed record AddTeaRequestDto(
        string CreatedBy,
        DateTime CreatedAt,

        string? Name,
        string? Description,
        decimal? Price,
        bool? IsInStock,
        int? AvailableStock,
        Guid? TeaTypeId);
}
