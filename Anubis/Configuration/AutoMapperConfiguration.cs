using System.Reflection;
using Anubis.Application.UserMappings;

namespace Anubis.Web.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        { 
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappings)));

            return services;
        }
    }
}
