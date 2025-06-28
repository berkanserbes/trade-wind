using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.DTOs.Responses;
using TradeWind.Shared.Models;

namespace TradeWind.Modules.Identity.Application.Interfaces;

public interface ILoginService
{
	Task<DataResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
