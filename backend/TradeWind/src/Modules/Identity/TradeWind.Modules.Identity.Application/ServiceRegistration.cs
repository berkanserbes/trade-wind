using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TradeWind.Modules.Identity.Application.Interfaces;
using TradeWind.Modules.Identity.Application.Services;
using TradeWind.Modules.Identity.Application.Validators;

namespace TradeWind.Modules.Identity.Application;

public static class ServiceRegistration
{
	public static IServiceCollection ConfigureValidators(IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
		services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

		return services;
	}

	public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IRegisterService, RegisterService>();
		services.AddScoped<ILoginService, LoginService>();

		return services;
	}
}
