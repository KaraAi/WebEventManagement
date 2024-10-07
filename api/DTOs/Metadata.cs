namespace api.DTOs
{
  public record Metadata
  {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
  }
}
