using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MongoDbIntegration.Infra.IoC.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var handlers = AppDomain.CurrentDomain.Load("MongoDbIntegration.Application");
            services.AddMediatR(handlers);

            return services;
        }
    }
}
