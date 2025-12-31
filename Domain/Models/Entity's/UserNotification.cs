using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entity_s;

public class UserNotification : BaseEntity
{
    [Required]
    public string Message  { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool InRead { get; set; }
    public bool Expired { get; set; }
    
    public int NotificationId { get; set; }
    
    [ForeignKey(nameof(NotificationId))] 
    public NotificationType NotificationType { get; set; }
}