using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeWind.Modules.Identity.Application.DTOs.Responses;

public sealed record LoginResponse(
	string AccessToken,
	string RefreshToken,
	Guid UserId,
	DateTime ExpiresAt
);
