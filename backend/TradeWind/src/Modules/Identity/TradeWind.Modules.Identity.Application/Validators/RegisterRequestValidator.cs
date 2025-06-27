using FluentValidation;
using TradeWind.Modules.Identity.Application.DTOs.Requests;

namespace TradeWind.Modules.Identity.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress()
			.WithMessage("Email is required and must be a valid email address.");

		RuleFor(x => x.Password)
			.NotEmpty()
			.MinimumLength(6)
			.WithMessage("Password is required and must be at least 6 characters long.");

		RuleFor(x => x.ConfirmPassword)
			.NotEmpty()
			.Equal(x => x.Password)
			.WithMessage("Confirm Password must match Password.");
	}
}
