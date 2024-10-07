namespace api.Constants.Exceptions
{
  [Serializable] // Ensure the class can be serialized
  public class RequestLimitBehind5Exception : Exception
  {
    // Default constructor
    public RequestLimitBehind5Exception()
    {
    }

    // Constructor that takes a message
    public RequestLimitBehind5Exception(string? message)
        : base(message) // Pass message to base Exception class
    {
    }

    // Constructor that takes a message and an inner exception
    public RequestLimitBehind5Exception(string? message, Exception? innerException)
        : base(message, innerException) // Pass both message and inner exception to base Exception class
    {
    }
  }
}
