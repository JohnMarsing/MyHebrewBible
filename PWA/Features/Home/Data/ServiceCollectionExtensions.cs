namespace PWA.Features.Home.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddHomeData(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}


