using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Modules.Identity.Application.Interfaces;
using TradeWind.Modules.Identity.Domain.Entities;

namespace TradeWind.Modules.Identity.Application.Services;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}
	public (string, SecurityTokenDescriptor) GenerateAccessToken(User user)
	{
		string secretKey = _configuration["Jwt:Secret"] ?? string.Empty;
		var expirationTime = _configuration["Jwt:ExpirationInMinutes"];
		var issuer = _configuration["Jwt:Issuer"];
		var audience = _configuration["Jwt:Audience"];

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new System.Security.Claims.ClaimsIdentity(new[]
			{
				new System.Security.Claims.Claim("id", user.Id.ToString()),
				new System.Security.Claims.Claim("email", user.Email),
				new System.Security.Claims.Claim("role", user.Role.ToString()),
				new System.Security.Claims.Claim("createdAt", user.CreatedAt.ToString("o")),
				new System.Security.Claims.Claim("updatedAt", user.UpdatedAt.ToString("o")),
				new System.Security.Claims.Claim("lastLoginAt", user.LastLoginAt?.ToString("o") ?? string.Empty)

			}),
			Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expirationTime)),
			SigningCredentials = credentials,
			Issuer = issuer,
			Audience = audience
		};

		var handler = new JsonWebTokenHandler();

		var token = handler.CreateToken(tokenDescriptor);
		return (token, tokenDescriptor);
	}

	public string GenerateRefreshToken()
	{
		return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
	}
}
