namespace api.DTOs.Responses
{
  public record DataResponses<T>
  {
    public T Data { get; init; } = default!;
    public string Message { get; init; } = null!;
    public bool Success { get; init; }
    public Metadata Metadata { get; init; } = default!;
    public int StatusCode { get; init; }
  }
}