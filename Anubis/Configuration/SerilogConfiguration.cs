using System.Collections.ObjectModel;
using System.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Anubis.Web.Configuration
{
    public static class SerilogConfiguration
    {
        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            var sinkOpts = new MSSqlServerSinkOptions { TableName = "Exceptions", SchemaName = "dbo", AutoCreateSqlTable = true };

            var columnOptions = new ColumnOptions
            {
                Id = { ColumnName = "Id", AllowNull = false },
                TimeStamp = { ColumnName = "DateThrown", ConvertToUtc = true, DataType = SqlDbType.DateTime, AllowNull = false },
                Exception = { ColumnName = "Error", DataType = SqlDbType.NVarChar, DataLength = 200, AllowNull = true },
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                        {ColumnName = "Application", PropertyName = "Application", DataType = SqlDbType.NVarChar, DataLength = 200, AllowNull = true}
                },
                MessageTemplate = { ColumnName = "MessageTemplate" }
            };

            columnOptions.Store.Remove(StandardColumn.Message);
            columnOptions.Store.Remove(StandardColumn.Level);
            columnOptions.Store.Remove(StandardColumn.Properties);

            var SolutionFullPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var tempStrings = SolutionFullPath.Split('\\');

            var solutionName = tempStrings[tempStrings.Length - 1]; ;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("SerilogConnection"),
                    sinkOptions: sinkOpts,
                    columnOptions: columnOptions
                )
                .MinimumLevel.Warning()
                .Enrich.WithProperty("Application", solutionName)
                .CreateLogger();

            builder.Logging.AddSerilog(Log.Logger);
            builder.Host.UseSerilog(Log.Logger);

            return builder;
        }
    }
}
