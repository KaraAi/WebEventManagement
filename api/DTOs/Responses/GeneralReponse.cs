namespace api.DTOs.Responses
{
    public record GeneralResponse
    {
        public required string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}
