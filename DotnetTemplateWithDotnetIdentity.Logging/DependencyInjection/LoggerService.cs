﻿using DotnetTemplateWithDotnetIdentity.Logging.Database;
using DotnetTemplateWithDotnetIdentity.Logging.Extensions;
using DotnetTemplateWithDotnetIdentity.Logging.LogRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotnetTemplateWithDotnetIdentity.Logging.DependencyInjection
{
    public static class LoggerService
    {
        public static void AddAppLogger(this ILoggingBuilder loggingBuilder, IServiceCollection services, IConfiguration config, string configKeyForConnStr = "AppLogDbConnection")
        {
            string logConnStr = config.GetConnectionString(configKeyForConnStr);
            string minimulLogLevel = config["Logging:LogLevel:DotnetTemplateWithDotnetIdentity.Logging.DbLogger"] ?? config["Logging:LogLevel:Default"] ?? "Information";
            loggingBuilder.AddDbLogger(config =>
            {
                config.ConnectionString = logConnStr;
                Enum.TryParse(minimulLogLevel, true, out LogLevel configLoglevel);
                config.MinimumLogLevel = configLoglevel;
            });

            services.AddScoped<LogDbContext>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<DbLoggerProvider>();
        }
    }
}
