using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entity_s;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}