using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace YouReplMe
{
    public class ReplHostBuilder 
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly AssemblyPaths paths;
        private readonly string _env;
        public IDictionary<object, object> Properties => _hostBuilder.Properties;

        internal ReplHostBuilder(IHostBuilder hostBuilder, AssemblyPaths paths)
        {
            _hostBuilder = hostBuilder;
            this.paths = paths;
            _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production";
            _hostBuilder.ConfigureAppConfiguration(app =>
            {

            });
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureHostConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureAppConfiguration((cxt, builder) =>
            {
                builder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{_env}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                .Build();
                configureDelegate(cxt, builder);
            }
            );
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return _hostBuilder.ConfigureServices(configureDelegate);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return _hostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            return _hostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureContainer(configureDelegate);
        }



        public IHost Build()
        {
            var factory = AssemblySearchPatternFactory.Create();
            AssemblySearchPatternFactory.Merge(paths, factory);
             _hostBuilder.UseAutoFacContainer(factory);
            return _hostBuilder.Build();
        }
    }
}