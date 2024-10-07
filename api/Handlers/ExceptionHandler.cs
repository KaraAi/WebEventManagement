using api.DTOs;
using api.DTOs.Responses;

namespace api.Handlers
{
  public static class ExceptionHandler<T>
  {
    public static DataResponses<T> QueryExceptionHandler(Exception ex, Metadata? metadata, int statusCode)
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
    public static GeneralResponse MutationExceptionHandler(Exception ex, int statusCode)
    {
      return new GeneralResponse
      {
        Message = ex.Message,
        Success = false,
        StatusCode = 500
      };
    }
  }
}