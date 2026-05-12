namespace PWA.Features.Bible.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddBibleData(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
