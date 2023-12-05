using System.Net;

namespace Reviver.Workers
{
    public class ReviverWorker : IHostedService, IDisposable
    {
        private readonly ILogger<ReviverWorker> _logger;

        private Timer _timer;

        public ReviverWorker(ILogger<ReviverWorker> logger)
        {
            _logger = logger;
        }

        private void DoWork(object state)
        {
            const string appReviver = "http://appreviver.somee.com/";

            using var client = new WebClient();

            try
            {
                var data = client.DownloadData(appReviver);
                if (data.Any())
                    _logger.LogInformation("Powodzenie! WebClient.DownloadData posiada dane. Url - " + appReviver);
                else
                    _logger.LogError("Blad! WebClient.DownloadData nie posiada danych. Url - " + appReviver);
            }
            catch (Exception ex)
            {
                _logger.LogError("Blad===========//========== URL: " + appReviver, ex);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service is starting.");

            // Uruchom timer, który wywołuje metodę DoWork co 1 minutę.
            _timer = new Timer(state => DoWork(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service is stopping.");

            // Zatrzymaj timer przed zakończeniem pracy usługi.
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
