using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class Events
  {
    [Key]
    public int EventID { get; set; }
    public required string EventCode { get; set; }
    public required string EventName { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public ICollection<User> Users { get; set; } = [];
  }
}