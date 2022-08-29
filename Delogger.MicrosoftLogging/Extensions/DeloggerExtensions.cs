using Delogger.Scope;
using Delogger.Scope.Log;
using Delogger.Scope.Perf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Delogger.MicrosoftLogging.Extensions
{
	public static class DeloggerExtensions
	{
		public static IHostBuilder SetUpDelogger(this IHostBuilder host)
		{
			host.ConfigureServices((_, services) =>
			{
				services.AddSingleton<DeloggerWraper>();
				services.AddHostedService<DeloggerWraper>(p => p.GetRequiredService<DeloggerWraper>());

				services.AddSingleton<IDScope>((sp) => sp.GetRequiredService<DeloggerWraper>().Delogger.RootScope);

				services.AddScoped<IDLogger>((sp) => sp.GetRequiredService<IDScope>().CreateLogger());
				services.AddScoped<IDPerfMonitor>(provider => provider.GetRequiredService<IDScope>().CreatePerfMonitor());

				services.AddSingleton<ILoggerProvider>(provider => new DeloggerLoggerProvider(provider.GetRequiredService<IDScope>()));

				services.AddScoped<ILogger>(provider => new DeloggerLoggerWraper(provider.GetRequiredService<IDScope>()));
			});

			return host;
		}
	}
}