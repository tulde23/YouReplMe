namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Registrations
    {
        public static IServiceCollection AddYouReplMeDependencies(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors
                                      .AddDependencyScanning()

                                     .AddOptions();
            return serviceDescriptors;
        }
    }
}