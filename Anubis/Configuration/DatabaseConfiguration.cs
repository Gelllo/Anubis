using Anubis.Application;
using Anubis.Application.Repository;
using Microsoft.EntityFrameworkCore;
using Anubis.Infrastructure.Dispatchers;
using Anubis.Infrastructure;
using Anubis.Infrastructure.Repository;

namespace Anubis.Web.Configuration
{
    public static class DatabaseConfiguration
    {
        public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AnubisContext>(x =>
                x.UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("AnubisConnection")));
            builder.Services.AddDbContext<ExceptionalContext>(x =>
                x.UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("ExceptionalConnection")));
            builder.Services.AddScoped<IAnubisUnitOfWork<AnubisContext>, AnubisUnitOfWork>();
            builder.Services.AddScoped<IApplicationExceptionsRepository, ApplicationExceptionsRepository>();
            builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            return builder;
        }
    }
}
