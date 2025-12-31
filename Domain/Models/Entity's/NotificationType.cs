using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entity_s;

public class NotificationType : BaseEntity
{
    [Required]
    public string Type { get; set; }

    public List<UserNotification> UserNotifications { get; set; }
}