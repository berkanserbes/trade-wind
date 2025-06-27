using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Modules.Identity.Application.Interfaces;

namespace TradeWind.Modules.Identity.Application.Services;

public class RegisterService : IRegisterService
{
	public Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
