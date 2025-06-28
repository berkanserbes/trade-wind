using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeWind.Modules.Identity.Application.DTOs.Requests;
using TradeWind.Modules.Identity.Application.Interfaces;

namespace TradeWind.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IRegisterService _registerService;

	public AuthController(IRegisterService registerService)
	{
		_registerService = registerService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
	{
		if (request == null)
		{
			return BadRequest("Request cannot be null.");
		}
		var result = await _registerService.RegisterAsync(request, cancellationToken);

		if (!result.IsSuccess)
		{
			return BadRequest(result);
		}
		return Ok(result);
	}
}
