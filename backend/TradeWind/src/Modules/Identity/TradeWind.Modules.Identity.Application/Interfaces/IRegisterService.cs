using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Shared.Models;

namespace TradeWind.Modules.Identity.Application.Interfaces;

public interface IRegisterService
{
	Task<DataResult<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
}
