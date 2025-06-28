using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TradeWind.Infrastructure.Database.DbContexts;
using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Modules.Identity.Application.Interfaces;
using TradeWind.Modules.Identity.Domain.Entities;
using TradeWind.Shared.Helpers;
using TradeWind.Shared.Models;

namespace TradeWind.Modules.Identity.Application.Services;

public class RegisterService : IRegisterService
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IValidator<RegisterRequest> _registerRequestValidator;

	public RegisterService(ApplicationDbContext dbContext, IValidator<RegisterRequest> registerRequestValidator)
	{
		_dbContext = dbContext;
		_registerRequestValidator = registerRequestValidator;
	}


	public async Task<DataResult<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
	{
		DataResult<RegisterResponse> response = null!;
		try
		{
			var validatorResult = _registerRequestValidator.Validate(request);
			if (!validatorResult.IsValid)
			{
				response = new DataResult<RegisterResponse>
				{
					IsSuccess = false,
					Message = string.Join("; ", validatorResult.Errors.Select(e => e.ErrorMessage)),
					Data = null
				};

				return response;
			}

			if (request.Password != request.ConfirmPassword)
			{
				throw new InvalidOperationException("Password and Confirm Password do not match.");
			}

			if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
			{
				throw new InvalidOperationException("User with this email already exists.");
			}

			var user = new User
			{
				Id = Guid.NewGuid(),
				Email = request.Email,
				PasswordHash = PasswordHelper.HashPassword(request.Password),
				CreatedAt = DateTime.UtcNow,
			};

			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync(cancellationToken);

			response = new DataResult<RegisterResponse>
			{
				IsSuccess = true,
				Message = "User registered successfully.",
				Data = new RegisterResponse(
					Id: user.Id,
					Email: user.Email,
					HashedPassword: user.PasswordHash,
					CreatedAt: user.CreatedAt
				)
			};
		}
		catch (Exception ex)
		{
			response = new DataResult<RegisterResponse>
			{
				IsSuccess = false,
				Message = ex.Message,
				Data = null
			};
		}

		return response;
	}
}
