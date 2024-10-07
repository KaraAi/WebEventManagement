namespace api.Constants.Exceptions
{
  [Serializable] // Ensure the class can be serialized
  public class RequestPageExceedTotalPagesException : Exception
  {
    // Default constructor
    public RequestPageExceedTotalPagesException()
    {
    }

    // Constructor that takes a message
    public RequestPageExceedTotalPagesException(string? message)
        : base(message) // Pass message to base Exception class
    {
    }

    // Constructor that takes a message and an inner exception
    public RequestPageExceedTotalPagesException(string? message, Exception? innerException)
        : base(message, innerException) // Pass both message and inner exception to base Exception class
    {
    }
  }
}
