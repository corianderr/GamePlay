using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GamePlay.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GamePlay.BLL.Helpers;

public static class JwtHelper
{
    private const int AccessTokenExpirationMinutes = 15; // Access token validity in minutes
    public const int RefreshTokenExpirationDays = 30;
    public static string GenerateAccessToken(ApplicationUser user, IConfiguration configuration, IEnumerable<string> userRoles)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtConfiguration:SecretKey"));
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email)
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(AccessTokenExpirationMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
    
    public static string GenerateRefreshToken(string userId, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtConfiguration:SecretKey"));
        var refreshTokenHandler = new JwtSecurityTokenHandler();
        var refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }),
            Expires = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var refreshToken = refreshTokenHandler.CreateToken(refreshTokenDescriptor);
        return refreshTokenHandler.WriteToken(refreshToken);
    }

    public static bool ValidateRefreshToken(string refreshToken, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtConfiguration:SecretKey"));
        var refreshTokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            refreshTokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out var validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}