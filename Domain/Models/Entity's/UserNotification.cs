using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entity_s;

public class UserNotification : BaseEntity
{
    [Required]
    public string Message  { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool InRead { get; set; }
    public bool Expired { get; set; }
    
    public int NotificationId { get; set; }
    public NotificationType NotificationType { get; set; }
}