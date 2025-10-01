namespace Core.IAM.Tokens.Models;
public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
