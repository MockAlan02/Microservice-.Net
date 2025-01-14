using Serilog;
using Serilog.Sinks.Syslog;
using System.Net.Sockets;

namespace Customer.Api
{
    public static class HostBuilderExtension
    {
        public static void ConfigureSeriaLog(this IHostBuilder hostBuilder)
        {
          
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(
                    path: "logs/log-.txt",         // Ruta donde se guardan los logs
                    rollingInterval: RollingInterval.Day, // Crear un nuevo archivo cada día
                    retainedFileCountLimit: 10,    // Número máximo de archivos a retener
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                    )
                    .WriteTo.Seq("http://localhost:5341") // Seq endpoint
                    .WriteTo.Syslog(
                    "logs3.papertrailapp.com",      // Papertrail host
                    port: 31175,                   // Puerto de Papertrail (ver en tu panel de control)
                    protocol: ProtocolType.Udp // Formato estándar
                    )
                    .MinimumLevel.Information()        // Nivel mínimo de log
                    .Enrich.FromLogContext()
                    .CreateLogger();

                Serilog.Debugging.SelfLog.Enable(Console.Out);
                hostBuilder.UseSerilog();
            
        }
    }
}
