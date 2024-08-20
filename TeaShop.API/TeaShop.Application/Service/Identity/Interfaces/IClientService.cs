using TeaShop.Application.DTOs.Identity.Request.Client;
using TeaShop.Application.DTOs.Identity.Response.Client;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Interfaces
{
    public interface IClientService
    {
        Task<Result<ClientInfoResponseDto>> GetClientInfo(Guid? id);
        Task<Result> UpdateClientInfo(Guid? id, UpdateClientInfoRequestDto request, CancellationToken cancellationToken);
    }
}
