using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Modules.Identity.Domain.Entities;

namespace TradeWind.Modules.Identity.Application.Interfaces;

public interface ITokenService
{
	(string, SecurityTokenDescriptor) GenerateAccessToken(User user);
	string GenerateRefreshToken();
}
