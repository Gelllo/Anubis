using System.Reflection;
using Anubis.Application;
using Anubis.Infrastracture;

namespace Anubis.Web.Configuration
{
    public static class HandlersConfiguration
    {
        public static IServiceCollection ConfigureHandlers(this IServiceCollection services)
        {
            services.Scan(selector =>
            {
                selector.FromAssemblyDependencies(Assembly.GetAssembly(typeof(DataContext)))
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(IQueryHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.Scan(selector =>
            {
                selector.FromAssemblyDependencies(Assembly.GetAssembly(typeof(DataContext)))
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(Anubis.Application.ICommandHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }
    }
}
