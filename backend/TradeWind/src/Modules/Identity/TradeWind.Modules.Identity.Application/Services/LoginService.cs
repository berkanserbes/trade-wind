using FluentValidation;
using TradeWind.Infrastructure.Database.DbContexts;
using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Modules.Identity.Application.Interfaces;
using TradeWind.Shared.Models;

namespace TradeWind.Modules.Identity.Application.Services;

public class LoginService : ILoginService
{
	private readonly ApplicationDbContext _context;
	private readonly IValidator<LoginRequest> _loginRequestValidator;

	public LoginService(ApplicationDbContext context, IValidator<LoginRequest> loginRequestValidator)
	{
		_loginRequestValidator = loginRequestValidator;
		_context = context;
	}


	public Task<DataResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
