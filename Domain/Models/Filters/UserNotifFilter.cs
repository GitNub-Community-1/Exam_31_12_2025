namespace Domain.Models.Filters;

public class UserNotifFilter
{
    public int? Id { get; set; }
    public string? Message  { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool? InRead { get; set; }
    public bool? Expired { get; set; }
    
    public int? NotificationId { get; set; }
}