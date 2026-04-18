namespace PWA.Features.Parasha.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddParashaData(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}


