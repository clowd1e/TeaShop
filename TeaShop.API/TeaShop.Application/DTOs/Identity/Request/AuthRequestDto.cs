namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed record AuthRequestDto(
        string? Email,
        string? Password);
}
