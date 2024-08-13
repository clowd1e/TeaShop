namespace TeaShop.Application.DTOs.Identity.Response
{
    public sealed record AuthResponseDto(
        string Id,
        string Token,
        string Email,
        string UserName);
}
