namespace Domain.Models.Entity_s;

public class ReportLog : BaseEntity
{
    public DateTime DateStart { get; set; }
    public bool Finish { get; set; }
    public string ResultDescription { get; set; }
}