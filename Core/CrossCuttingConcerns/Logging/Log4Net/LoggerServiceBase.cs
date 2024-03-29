﻿using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;


namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        
        public LoggerServiceBase()
        {

        }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        public static void serilogConfiguration()
        {
            string connectionString = Configuration.GetConnectionString("SqlConnectionString");

            var columnOptions = new ColumnOptions
            {
                Exception = new ColumnOptions.ExceptionColumnOptions(),
                AdditionalColumns = new Collection<SqlColumn>
                { new SqlColumn("Username", SqlDbType.VarChar) }
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer
                (connectionString, sinkOptions: new SinkOptions { TableName = "webApiLogs" },null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
                .CreateLogger();
        }



        public void Info(string logMessage)
        {
            Log.Logger.Information(logMessage);
            
        }

        public void Debug(string logMessage)
        {
            Log.Logger.Debug(logMessage);
        }

        public void Error(string logMessage, Exception ex)
        {
            Log.Logger.Error(ex,logMessage);
        }

        public void Fatal(string logMessage)
        {
            Log.Logger.Fatal(logMessage);
        }

        public void Warn(string logMessage)
        {
            Log.Logger.Warning(logMessage);
        }
    }
}
