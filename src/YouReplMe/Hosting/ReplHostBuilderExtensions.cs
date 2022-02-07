using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Hosting
{
    public static class ReplHostBuilderExtensions
    {
        private static string _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production";

        public static IHostBuilder AddAppConfiguration(this IHostBuilder builder, string userSecretKey = null)
        {
            if (!ANSIInitializer.Init(false)) ANSIInitializer.Enabled = false;
            return builder.ConfigureAppConfiguration(builder =>
            {
                builder
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{_env}.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables()
                   .Build();
                if (!string.IsNullOrEmpty(userSecretKey))
                {
                    builder.AddUserSecrets(userSecretKey);
                }
            });
        }

        public static IHostBuilder ConfigureDependencyScanning(this IHostBuilder builder, params string[] assemblyPatterns)
        {
            var factory = AssemblySearchPatternFactory.Create().Custom(x => x.StartsWith("YouReplMe"));

            AssemblySearchPatternFactory.Merge(AssemblyPaths.From(assemblyPatterns), factory);
            builder.ConfigureServices(services => services.AddYouReplMeDependencies());
            return builder.UseAutoFacContainer(factory);
        }

        public static async ValueTask StartRepLoop(this IHostBuilder hostBuilder,
            Func<string> prompt = null,
            Func<string> logo = null,
            CancellationTokenSource tokenSource = default)
        {
            tokenSource = tokenSource ?? new CancellationTokenSource();

            using (var host = hostBuilder.Build())
            {
                var replHost = host.Services.GetService<IReplHost>();
                replHost.DisplayLogo(logo);
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    replHost.DisplayPrompt(prompt);
                    await replHost.StartAsync(host, tokenSource);
                }
            }
        }
    }
}