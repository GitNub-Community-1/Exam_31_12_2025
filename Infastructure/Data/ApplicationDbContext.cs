using Domain.Models.Entity_s;
using Domain.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<NotificationType>  NotificationTypes { get; set; }
    public DbSet<ReportLog> ReportLogs { get; set; }
}