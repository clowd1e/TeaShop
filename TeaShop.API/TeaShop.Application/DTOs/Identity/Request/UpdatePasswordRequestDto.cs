namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed record UpdatePasswordRequestDto(
        string? NewPassword,
        string? RepeatNewPassword);
}
