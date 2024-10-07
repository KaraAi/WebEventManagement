namespace api.Constants.Messages
{
  public static class ExceptionMessages
  {
    public const string RequestLimitBehind5 = "Request limit must be greater than or equal to 5";
    public const string RequestLimitExceed50 = "Request limit must be less than 50";
    public const string RequestPageExceedTotalPages = "Request page must be less than or equal to total pages";
    public const string RequestPageBehind1 = "Request page must be greater than or equal to 1";
    public const string Exception400StatusCode = "Request options not allowed";
    public const string Exception401StatusCode = "Please authenticate to access this resource";
    public const string Exception403StatusCode = "You are not authorized to access this resource";
    public const string Exception404StatusCode = "Resource not found";
    public const string Exception405StatusCode = "Method not allowed";
    public const string Exception500StatusCode = "Error occurred while processing request";
  }
}