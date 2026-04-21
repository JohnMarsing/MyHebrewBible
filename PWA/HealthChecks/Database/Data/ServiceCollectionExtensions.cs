namespace PWA.HealthChecks.Database.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseHealthChecks(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}


