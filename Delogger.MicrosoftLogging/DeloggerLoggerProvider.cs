using Delogger.Scope;
using Delogger.Scope.Log;
using Microsoft.Extensions.Logging;

namespace Delogger.MicrosoftLogging
{
	internal class DeloggerLoggerProvider : ILoggerProvider
	{
		private readonly IDScope scope;

		internal DeloggerLoggerProvider(IDScope scope)
		{
			this.scope = scope;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new DeloggerLoggerWraper(scope, scope.CreateLogger(new LoggerCreateOptions { Tags = new[] { categoryName } }));
		}

		public void Dispose()
		{

		}
	}
}