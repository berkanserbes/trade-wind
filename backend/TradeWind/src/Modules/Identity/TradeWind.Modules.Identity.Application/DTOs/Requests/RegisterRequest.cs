namespace TradeWind.Modules.Identity.Application.DTOs.Requests;

public sealed record RegisterRequest(
	string Email,
	string Password,
	string ConfirmPassword
);
