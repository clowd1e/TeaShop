using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeaShop.Application.DTOs.Identity.Request;
using TeaShop.Application.DTOs.Identity.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors.Identity;
using TeaShop.Application.Service.Identity.Interfaces;
using TeaShop.Domain.ValueObjects;
using TeaShop.Identity.Database;
using TeaShop.Identity.Models;

namespace TeaShop.Identity.Service
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly BCryptSettings _bcryptSettings;
        private readonly TeaShopIdentityDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            IOptions<BCryptSettings> bcryptSettings,
            TeaShopIdentityDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _bcryptSettings = bcryptSettings.Value;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<AuthResponseDto>> LoginAsync(AuthRequestDto request)
        {
            var validation = new AuthRequestDtoValidator().Validate(request);
            if (!validation.IsValid)
                return validation;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return LoginErrors.EmailNotFound;

            bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                return LoginErrors.InvalidPassword;

            JwtSecurityToken jwtSecurityToken = GenerateToken(user);

            AuthResponseDto response = new(
                user.Id,
                new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                user.Email,
                user.UserName);

            return response.ToResult();
        }

        public async Task<Result> RegisterClientAsync(RegRequestDto request)
        {
            var validation = new RegRequestDtoValidator().Validate(request);
            if (!validation.IsValid)
                return validation;

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
                return RegisterErrors.EmailAlreadyExists;

            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = UserRole.Client.ToString(),
                UserName = $"{request.FirstName}_{request.LastName}_{UserRole.Client}",
                NormalizedUserName = $"{request.FirstName}_{request.LastName}_{UserRole.Client}".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, _bcryptSettings.WorkFactor)
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
                return RegisterErrors.UserCreationFailed;

            var clientInfo = new Client
            {
                Id = new Guid(newUser.Id),
                Address = _mapper.Map<Address>(request.Address),
                BirthDate = (DateTime)request.BirthDate!,
                Phone = request.Phone,
            };

            await _context.Clients.AddAsync(clientInfo);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        private JwtSecurityToken GenerateToken(ApplicationUser user)
        {
            var handler = new JwtSecurityTokenHandler();

            var securityClaims = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityClaims, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateJwtSecurityToken(tokenDescriptor);

            return token;
        }

        private static ClaimsIdentity GenerateClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("id", user.Id)
            };

            claims.AddRange(user.Role
                .Split(',')
                .Select(role => new Claim(ClaimTypes.Role, role)));

            return new(claims);
        }
    }
}
