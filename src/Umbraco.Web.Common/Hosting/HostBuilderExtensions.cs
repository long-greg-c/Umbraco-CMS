using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Common.DependencyInjection;

namespace Umbraco.Cms.Web.Common.Hosting;

/// <summary>
/// Umbraco specific extensions for the <see cref="IHostBuilder"/> interface.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures an existing <see cref="IHostBuilder"/> with defaults for an Umbraco application.
    /// </summary>
    public static IHostBuilder ConfigureUmbracoDefaults(this IHostBuilder builder)
    {
#if DEBUG
        builder.ConfigureAppConfiguration(config
            => config.AddJsonFile(
                "appsettings.Local.json",
                optional: true,
                reloadOnChange: true));

#endif
        builder.ConfigureLogging(x => x.ClearProviders());

        return new UmbracoHostBuilderDecorator(builder, OnHostBuilt);
    }

    // Runs before any IHostedService starts (including generic web host).
    private static void OnHostBuilt(IHost host) =>
        StaticServiceProvider.Instance = host.Services;
}
