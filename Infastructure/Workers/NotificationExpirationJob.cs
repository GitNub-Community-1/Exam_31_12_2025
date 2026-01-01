using Domain.Models.Entity_s;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Infastructure.Workers;

public class NotificationExpirationJob(ApplicationDbContext db, ILogger<NotificationExpirationJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // try
        // {
            var now = DateTime.UtcNow;
            var unread = await db.UserNotifications
                .Where(n => !n.InRead && !n.Expired)
                .ToListAsync();

            var expiredCount = 0;
            foreach (var notif in unread)
            {
                if ((now - notif.CreatedAt).TotalDays > 7)
                {
                    notif.Expired = true;
                    expiredCount++;
                }
            }

            var log = new ReportLog
            {
                DateStart = now,
                Finish = true,
                ResultDescription = $"Checked {unread.Count} notifications, expired {expiredCount} at {now}"
            };

            db.ReportLogs.Add(log);
            await db.SaveChangesAsync();
            
            logger.LogInformation($"Notification expiration job executed. Expired {expiredCount} notifications.");
        // }
        // catch (Exception ex)
        // {
        //     logger.LogError(ex, "Error in NotificationExpirationJob");
        //     throw;
        // // }
    }
}
