namespace TradeWind.Modules.Identity.Application.DTOs.Responses;

public sealed record RegisterResponse(
	Guid Id,
	string Email,
	string HashedPassword,
	DateTime CreatedAt
);
