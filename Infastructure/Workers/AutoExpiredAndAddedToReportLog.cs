using Domain.Models.Entity_s;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infastructure.Workers;

public class AutoExpiredAndAddedToReportLog(IServiceScopeFactory _scopeFactory) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var now = DateTime.UtcNow;
            var unread = await db.UserNotifications
                .Where(n => !n.InRead && !n.Expired)
                .ToListAsync(stoppingToken);

            foreach (var notif in unread)
            {
                if ((now - notif.CreatedAt).TotalDays > 7)
                {
                    notif.Expired = true;
                }
            }

            var log = new ReportLog
            {
                DateStart = now,
                Finish = true,
                ResultDescription = $"Checked {unread.Count} notifications at {now}"
            };

            db.ReportLogs.Add(log);
            await db.SaveChangesAsync(stoppingToken);

                

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}