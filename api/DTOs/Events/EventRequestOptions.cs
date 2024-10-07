namespace api.DTOs.Events
{
  public record EventRequestOptions
  {
    public EventQueryOptions Query { get; set; } = new EventQueryOptions();
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 5;
    public EventSortOptions Sort { get; set; } = new EventSortOptions();
  }

  public record EventQueryOptions
  {
    public int? EventID { get; set; }
    public string? EventCode { get; set; }
    public string? EventName { get; set; }
    public string? UserCreated { get; set; }
    public string? UserUpdated { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
  }

  public record EventSortOptions
  {
    public bool EventID { get; set; }
    public bool EventCode { get; set; }
    public bool EventName { get; set; }
    public bool UserCreated { get; set; }
    public bool UserUpdated { get; set; }
    public bool DateCreated { get; set; }
    public bool DateUpdated { get; set; }
  }
}