namespace PWA.Features.Bible.TSK.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddTSKData(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
