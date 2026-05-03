
namespace PWA.Features.ScrollSpy.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddScrollSpyData(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
