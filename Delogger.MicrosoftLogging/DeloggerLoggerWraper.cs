using Delogger.Scope;
using Delogger.Scope.Log;
using Microsoft.Extensions.Logging;

namespace Delogger.MicrosoftLogging
{
	public class DeloggerLoggerWraper : IDLogger, ILogger
	{
		protected IDLogger logger;
		protected IDScope scope;

		internal DeloggerLoggerWraper(IDScope scope, IDLogger logger)
		{
			this.scope = scope;
			this.logger = logger;
		}

		public DeloggerLoggerWraper(IDScope scope)
		{
			this.scope = scope;
			logger = scope.CreateLogger();
		}

		public void Log(string message, string[]? tags = null, object[]? args = null, KeyValuePair<string, object>[]? attachments = null, WriteFlagsEnum flags = (WriteFlagsEnum)(-1))
		{
			logger.Log(message, tags, args, attachments, flags);
		}

		public void Dispose()
		{
			logger.Dispose();
		}

		#region ILogger imp
		void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			Log(formatter(state, exception), new[] { logLevel.ToString() }, null, null, WriteFlagsEnum.All);
		}

		bool ILogger.IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		IDisposable ILogger.BeginScope<TState>(TState state)
		{
			return null;
		}

		#endregion
	}

	public class DeloggerLoggerWraper<T> : DeloggerLoggerWraper, ILogger<T>, ILogger
	{
		public DeloggerLoggerWraper(IDScope scope) : base(scope, scope.CreateLogger(new LoggerCreateOptions { Tags = new[] { typeof(T).Name } }))
		{

		}
	}
}