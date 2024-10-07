namespace api.Constants.Exceptions
{
  [Serializable] // Ensure the class can be serialized
  public class RequestGeneralException : Exception
  {
    // Default constructor
    public RequestGeneralException()
    {
    }

    // Constructor that takes a message
    public RequestGeneralException(string? message)
        : base(message) // Pass message to base Exception class
    {
    }

    // Constructor that takes a message and an inner exception
    public RequestGeneralException(string? message, Exception? innerException)
        : base(message, innerException) // Pass both message and inner exception to base Exception class
    {
    }
  }
}
