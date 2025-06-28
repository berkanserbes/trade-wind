using FluentValidation;
using TradeWind.Modules.Identity.Application.DTOs.Requests;

namespace TradeWind.Modules.Identity.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress()
			.WithMessage("Email is required and must be a valid email address.");
		RuleFor(x => x.Password)
			.NotEmpty()
			.WithMessage("Password is required.");
	}
}
