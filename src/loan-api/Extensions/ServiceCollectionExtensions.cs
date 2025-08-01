using FluentValidation;
using System.Reflection;

namespace Extensions
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
            services.AddHttpClient();
            services.AddHttpClient("ScoringEngine", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8000");
            });
        
            return services;
        }
    }

}