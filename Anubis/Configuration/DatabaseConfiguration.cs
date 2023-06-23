using Anubis.Application;
using Microsoft.EntityFrameworkCore;
using Anubis.Infrastracture.Dispatchers;
using Anubis.Infrastracture;

namespace Anubis.Web.Configuration
{
    public static class DatabaseConfiguration
    {
        public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(x =>
                x.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("AnubisConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            return builder;
        }
    }
}
