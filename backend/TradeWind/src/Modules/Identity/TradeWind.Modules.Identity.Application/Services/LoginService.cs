using FluentValidation;
using TradeWind.Infrastructure.Database.DbContexts;
using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Modules.Identity.Application.Interfaces;
using TradeWind.Shared.Helpers;
using TradeWind.Shared.Models;

namespace TradeWind.Modules.Identity.Application.Services;

public class LoginService : ILoginService
{
	private readonly ApplicationDbContext _context;
	private readonly IValidator<LoginRequest> _loginRequestValidator;
	private readonly ITokenService _tokenService;

	public LoginService(ApplicationDbContext context, IValidator<LoginRequest> loginRequestValidator, ITokenService tokenService)
	{
		_loginRequestValidator = loginRequestValidator;
		_context = context;
		_tokenService = tokenService;
	}


	public async Task<DataResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
	{
		DataResult<LoginResponse> response = null!;
		try
		{
			var validationResult = _loginRequestValidator.Validate(request);
			if (!validationResult.IsValid)
			{
				response = new DataResult<LoginResponse>
				{
					IsSuccess = false,
					Message = ValidationHelper.GetErrorMessage(validationResult),
					Data = null
				};
				return response;
			}

			var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
			if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
			{
				response = new DataResult<LoginResponse>
				{
					IsSuccess = false,
					Message = "Invalid email or password.",
					Data = null
				};
				return response;
			}

			var (accessToken, tokenDescriptor) = _tokenService.GenerateAccessToken(user);

			response = new DataResult<LoginResponse>
			{
				IsSuccess = true,
				Message = "Login successful.",
				Data = new LoginResponse(
					AccessToken: accessToken, 
					RefreshToken: _tokenService.GenerateRefreshToken(), 
					UserId: user.Id, 
					ExpiresAt: tokenDescriptor.Expires.GetValueOrDefault())
			};

			user.LastLoginAt = DateTime.UtcNow;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			return response;
		}
		catch (Exception ex)
		{
			response = new DataResult<LoginResponse>
			{
				IsSuccess = false,
				Message = $"{ex.Message}",
				Data = null
			};
			return response;
		}

	}
}
