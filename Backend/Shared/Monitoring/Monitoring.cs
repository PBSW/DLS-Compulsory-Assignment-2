using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;

namespace Shared.Monitoring;

public class Monitoring
{
    public static readonly ActivitySource ActivitySource = new("CAL_" + Assembly.GetEntryAssembly()?.GetName().Name, "1.0.0");
    
    public static ILogger Log => Serilog.Log.Logger;

    static Monitoring()
    {
        // OpenTelemetry
        Sdk.CreateTracerProviderBuilder()
            .SetSampler(new AlwaysOnSampler())
            .AddZipkinExporter(config =>
            {
                config.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
            })
            .AddSource(ActivitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: ActivitySource.Name))
            .Build();
        
        // Logging
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Seq("http://seq:5341")
            .Enrich.WithSpan()
            .Enrich.WithMachineName()
            .Enrich.WithAssemblyName()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .CreateLogger();
    }
}