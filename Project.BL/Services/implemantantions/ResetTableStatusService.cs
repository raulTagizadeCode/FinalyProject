using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.DAL.Contexts;

namespace Project.BL.Services.implemantantions
{
    public class ResetTableStatusService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ResetTableStatusService> _logger;
        readonly AppDbContext _dbContext;
        public ResetTableStatusService(IServiceProvider serviceProvider, ILogger<ResetTableStatusService> logger, AppDbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    var nextRun = DateTime.Today.AddDays(1); // Sabahın 00:00 vaxtı
                    var delay = nextRun - now;

                    _logger.LogInformation($"Masaların statusu {nextRun} tarixində sıfırlanacaq.");

                    await Task.Delay(delay, stoppingToken); // Gözləmə müddəti

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var tables = dbContext.Masas.Where(t => !t.IsActive).ToList();
                        foreach (var table in tables)
                        {
                            table.IsActive = true;
                        }

                        dbContext.SaveChanges();
                    }

                    _logger.LogInformation("Bütün masaların IsActive statusu yeniləndi.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ResetTableStatusService xətası: {ex.Message}");
                }
            }
        }
    }
}