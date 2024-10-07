using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class User
  {
    [Key]
    public int UserID { get; set; }
    public required string UserCode { get; set; }
    public required string FullName { get; set; }
    public required string CCCD { get; set; }
    public required string Phone { get; set; }
    public required string Facility { get; set; }
    public required string Office { get; set; }
    public required string Email { get; set; }
    public bool IsCheck { get; set; }
    public required string Description { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public int EventID { get; set; }
    public Events? Events { get; set; }
  }
}