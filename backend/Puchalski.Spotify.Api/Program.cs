using Puchalski.Spotify.Api;
using Serilog;
using System.Reflection;

public class Program {
    public static void Main(string[] args) {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) => {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                config.AddJsonFile(assemblyPath + "\\appsettings.spotify.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
    }
}