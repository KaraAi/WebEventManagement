using api.DTOs;
using api.DTOs.Responses;

namespace api.Handlers
{
  public static class ExceptionHandler<T>
  {
    public static DataResponses<T> DataExceptionHandler(Exception ex, Metadata metadata, int statusCode)
    {
      return new DataResponses<T>
      {
        Data = default!,
        Message = ex.Message,
        Success = false,
        Metadata = metadata,
        StatusCode = statusCode
      };
    }
  }
}