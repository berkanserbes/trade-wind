using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TradeWind.Shared.Helpers;

public static class JwtTokenHelper
{
	public static ClaimsPrincipal? ValidateJwtToken(string token, string? secretKey = "", string? issuer = "", string? audience = "")
	{
		if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
			return null;
		if (string.IsNullOrWhiteSpace(token))
			return null;
		try
		{
			var handler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(secretKey);

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = issuer,
				ValidateAudience = true,
				ValidAudience = audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			ClaimsPrincipal? principal = handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
			if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				return null;
			}

			return principal;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
