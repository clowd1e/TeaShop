using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Identity.Request.Client;
using TeaShop.Application.DTOs.Identity.Response.Client;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors.Identity;
using TeaShop.Application.Service.Identity.Interfaces;
using TeaShop.Domain.ValueObjects;
using TeaShop.Identity.Database;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Service
{
    public sealed class ClientService : IClientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TeaShopIdentityDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(
            UserManager<ApplicationUser> userManager,
            TeaShopIdentityDbContext context,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ClientInfoResponseDto>> GetClientInfo(Guid? id)
        {
            if (id is null)
                return Error.IdIsNull;

            var user = await _userManager.FindByIdAsync(id.ToString()!);
            if (user is null)
                return UserErrors.UserNotFound;

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client is null)
                return UserErrors.UserNotFound;

            var result = new ClientInfoResponseDto(
                user.FirstName, user.LastName, user.Role, user.Email,
                client.Phone, client.BirthDate, client.Address);

            return result.ToResult();
        }

        public async Task<Result> UpdateClientInfo(Guid? id, UpdateClientInfoRequestDto request, CancellationToken cancellationToken)
        {
            if (id is null)
                return Error.IdIsNull;

            var validation = new UpdateClientInfoRequestDtoValidator().Validate(request);
            if (!validation.IsValid)
                return validation.ToResult();

            var user = await _userManager.FindByIdAsync(id.ToString()!);
            if (user is null)
                return UserErrors.UserNotFound;

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client is null)
                return UserErrors.UserNotFound;

            #region Update user and client info
            user.FirstName = request.FirstName!;
            user.LastName = request.LastName!;
            user.UserName = $"{request.FirstName}_{request.LastName}_{UserRole.Client}";
            user.NormalizedUserName = user.UserName.ToUpper();
            user.Email = request.Email!;
            user.NormalizedEmail = user.Email.ToUpper();
            client.Address = _mapper.Map<Address>(request.Address);
            client.Phone = request.Phone;
            client.BirthDate = (DateTime)request.BirthDate!;
            #endregion

            await _userManager.UpdateAsync(user);
            _context.Clients.Update(client);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
