namespace api.DTOs.Events
{
  public record EventDetail
  {
    public int EventID { get; set; }
    public required string EventCode { get; set; }
    public required string EventName { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public List<EventUserDetail> Users { get; set; } = [];
  }

  public record EventUserDetail
  {
    public int UserID { get; init; }
    public required string UserCode { get; set; }
    public required string FullName { get; set; }
    public required string CCCD { get; set; }
    public required string Phone { get; set; }
    public required string Facility { get; set; }
    public required string Office { get; set; }
    public required string Email { get; set; }
    public int IsCheck { get; set; }
    public required string Description { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
  }
}