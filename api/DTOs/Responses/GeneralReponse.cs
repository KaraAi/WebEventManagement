namespace api.DTOs.Responses
{
    public record GeneralResponse(int StatusCode, string Message = null!);
}
