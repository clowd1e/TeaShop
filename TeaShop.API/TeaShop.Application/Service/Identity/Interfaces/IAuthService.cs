using TeaShop.Application.DTOs.Identity.Request;
using TeaShop.Application.DTOs.Identity.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> LoginAsync(AuthRequestDto request);
        Task<Result> RegisterClientAsync(RegRequestDto request);
    }
}
