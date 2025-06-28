using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Modules.Identity.Application.Interfaces;

namespace TradeWind.Modules.Identity.Application.Services;

public class TokenService : ITokenService
{
	public string GenerateAccessToken()
	{
		throw new NotImplementedException();
	}

	public string GenerateRefreshToken()
	{
		throw new NotImplementedException();
	}
}
