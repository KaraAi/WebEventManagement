namespace api.Constants.Exceptions
{
  [Serializable] // Ensure the class can be serialized
  public class RequestLimitExceed50Exception : Exception
  {
    // Default constructor
    public RequestLimitExceed50Exception()
    {
    }

    // Constructor that takes a message
    public RequestLimitExceed50Exception(string? message)
        : base(message) // Pass message to base Exception class
    {
    }

    // Constructor that takes a message and an inner exception
    public RequestLimitExceed50Exception(string? message, Exception? innerException)
        : base(message, innerException) // Pass both message and inner exception to base Exception class
    {
    }
  }
}
