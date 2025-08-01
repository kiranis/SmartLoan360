using System.Reflection;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoanApiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("ScoringApi", client =>
            {
                // For local development, use localhost instead of scoring-engine
                client.BaseAddress = new Uri("http://localhost:8000");
            });

            return services;
        }
    }

}