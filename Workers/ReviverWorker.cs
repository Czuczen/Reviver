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
            const string reviver = "http://reviver.somee.com";
            const string hrHarmonyProductionSmarterAspUrl = "http://hrharmony2-001-site1.ctempurl.com/Employee";
            const string chilloutRoomProductionSmarterAspUrl = "http://chilloutroom2-001-site1.gtempurl.com/Account/Login";

            using var client = new WebClient();

            client.Headers.Add("X-Reviver-Request", "true");

            try
            {
                var data = client.DownloadData(reviver);
                if (data.Any())
                    _logger.LogInformation("Powodzenie! WebClient.DownloadData posiada dane. Url - " + reviver);
                else
                    _logger.LogError("Blad! WebClient.DownloadData nie posiada danych. Url - " + reviver);
            }
            catch (Exception ex)
            {
                _logger.LogError("Blad===========//========== URL: " + reviver, ex);
            }

            try
            {
                var data = client.DownloadData(hrHarmonyProductionSmarterAspUrl);
                if (data.Any())
                    _logger.LogInformation("Powodzenie! WebClient.DownloadData posiada dane. Url - " + hrHarmonyProductionSmarterAspUrl);
                else
                    _logger.LogError("Blad! WebClient.DownloadData nie posiada danych. Url - " + hrHarmonyProductionSmarterAspUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("Blad===========//========== URL: " + hrHarmonyProductionSmarterAspUrl, ex);
            }

            try
            {
                var data = client.DownloadData(chilloutRoomProductionSmarterAspUrl);
                if (data.Any())
                    _logger.LogInformation("Powodzenie! WebClient.DownloadData posiada dane. Url - " + chilloutRoomProductionSmarterAspUrl);
                else
                    _logger.LogError("Blad! WebClient.DownloadData nie posiada danych. Url - " + chilloutRoomProductionSmarterAspUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("Blad===========//========== URL: " + chilloutRoomProductionSmarterAspUrl, ex);
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
