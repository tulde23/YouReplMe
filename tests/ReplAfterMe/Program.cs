using Microsoft.Extensions.Hosting;
using ReplAfterMe;

var builder = Host.CreateDefaultBuilder(args)
                         .AddAppConfiguration()
                         .InitializeTheme(YouReplMe.Themes.ThemeNames.Greyson)
                         .ConfigureDependencyScanning("ReplAfterMe");

var tokenSource = new CancellationTokenSource();
await builder.StartRepLoop(Config.WritePrompt, Config.WriteLogo,tokenSource);
