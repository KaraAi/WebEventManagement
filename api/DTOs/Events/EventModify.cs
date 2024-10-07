namespace api.DTOs.Events
{
  public record ModifyEvent
  {
    public required string EventCode { get; set; }
    public required string EventName { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public List<int> UserIDs { get; set; } = [];
  }
}