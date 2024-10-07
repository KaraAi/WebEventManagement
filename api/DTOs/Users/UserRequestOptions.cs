namespace api.DTOs.Users
{
  public record UserRequestOptions
  {
    public UserQueryOptions Query { get; set; } = new UserQueryOptions();
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 5;
    public UserSortOptions Sort { get; set; } = new UserSortOptions();
  }

  public record UserQueryOptions
  {
    public int? UserID { get; set; }
    public string? UserCode { get; set; }
    public string? FullName { get; set; }
    public string? CCCD { get; set; }
    public string? Phone { get; set; }
    public string? Facility { get; set; }
    public string? Office { get; set; }
    public string? Email { get; set; }
    public bool IsCheck { get; set; }
    public string? Description { get; set; }
    public string? UserCreated { get; set; }
    public string? UserUpdated { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public int? EventID { get; set; }
    public string? EventCode { get; set; }
    public string? EventName { get; set; }
  }

  public record UserSortOptions
  {
    public bool UserID { get; set; }
    public bool UserCode { get; set; }
    public bool FullName { get; set; }
    public bool CCCD { get; set; }
    public bool Phone { get; set; }
    public bool Facility { get; set; }
    public bool Office { get; set; }
    public bool Email { get; set; }
    public bool IsCheck { get; set; }
    public bool Description { get; set; }
    public bool UserCreated { get; set; }
    public bool UserUpdated { get; set; }
    public bool DateCreated { get; set; }
    public bool DateUpdated { get; set; }
    public bool EventId { get; set; }
    public bool EventCode { get; set; }
    public bool EventName { get; set; }
  }
}