namespace api.Constants.Messages
{
  public static class ExceptionMessages
  {
    public const string RequestLimitBehind5 = "Request limit must be greater than or equal to 5";
    public const string RequestLimitExceed50 = "Request limit must be less than 50";
    public const string RequestPageExceedTotalPages = "Request page must be less than or equal to total pages";
    public const string RequestPageBehind1 = "Request page must be greater than or equal to 1";
    public const string BadRequest = "Request options not allowed";
    public const string UnauthorizedAccess = "Please authenticate to access this resource";
    public const string ForbiddenResourceAccess = "You are not authorized to access this resource";
    public const string NotFound = "Resource not found";
    public const string MethodNotAllowed = "Method not allowed";
    public const string InternalServerError = "Error occurred while processing request";
  }
}