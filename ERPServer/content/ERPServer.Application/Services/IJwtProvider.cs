using ERPServer.Application.Features.Auth.Login;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}
