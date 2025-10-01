using Core.IAM.Identity.Tokens.Features.Generate;
using Core.IAM.Identity.Tokens.Features.Refresh;
using Core.IAM.Tokens.Models;

namespace Core.IAM.Identity.Tokens.Services;
public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(TokenGenerationCommand request, string ipAddress, CancellationToken cancellationToken);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenCommand request, string ipAddress, CancellationToken cancellationToken);

}
