using Delogger.InformationWorker;
using Microsoft.Extensions.Hosting;

namespace Delogger.MicrosoftLogging
{
	internal class DeloggerWraper : IHostedService
	{
		public Delogger Delogger { get; private set; }

		public DeloggerWraper()
		{
			Delogger = new Delogger(new ThreadedInformationProcessWorker(), false);
		}

		Task IHostedService.StartAsync(CancellationToken cancellationToken)
		{
			Delogger.Start();

			return Task.CompletedTask;
		}

		Task IHostedService.StopAsync(CancellationToken cancellationToken)
		{
			Delogger.Dispose();

			return Task.CompletedTask;
		}
	}
}