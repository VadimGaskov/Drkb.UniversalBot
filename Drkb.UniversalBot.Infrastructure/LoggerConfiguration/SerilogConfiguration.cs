using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Drkb.UniversalBot.Infrastructure.LoggerConfiguration;

public static class SerilogConfiguration
{
    public static Logger GetSerilogConfiguration(ConfigurationManager configuration)
    {
        return new Serilog.LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}