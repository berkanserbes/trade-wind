namespace TradeWind.Modules.Identity.Application.DTOs.Requests;

public sealed record LoginRequest(
	string Email,
	string Password
);
