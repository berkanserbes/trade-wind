using TradeWind.Shared.Helpers;

namespace TradeWind.WebApi.Middlewares;

public class AuthenticationMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IConfiguration _configuration;

	public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		_next = next;
		_configuration = configuration;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

		if (string.IsNullOrEmpty(token))
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Unauthorized");
			return;
		}

		try
		{
			string? secretKey = _configuration["Jwt:SecretKey"];
			string? issuer = _configuration["Jwt:Issuer"];
			string? audience = _configuration["Jwt:Audience"];


			var principal = JwtTokenHelper.ValidateJwtToken(token: token,
				secretKey: secretKey,
				issuer: issuer,
				audience: audience);

			if(principal is null)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Unauthorized: Invalid or expired access token.");
				return;
			}

			context.User = principal;
			await _next(context);
		}
		catch (Exception ex)
		{
			await context.Response.WriteAsync($"Unauthorized: {ex.Message}");
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return;
		}
	}
}
