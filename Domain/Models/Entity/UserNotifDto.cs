namespace Domain.Models.Entity;

public class UserNotifDto
{
    public int Id { get; set; }
    public string Message  { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool InRead { get; set; }
    public bool Expired { get; set; }
    
    public int NotificationId { get; set; }
}