namespace api.Constants.Exceptions
{
  [Serializable] // Ensure the class can be serialized
  public class RequestPageBehind1Exception : Exception
  {
    // Default constructor
    public RequestPageBehind1Exception()
    {
    }

    // Constructor that takes a message
    public RequestPageBehind1Exception(string? message)
        : base(message) // Pass message to base Exception class
    {
    }

    // Constructor that takes a message and an inner exception
    public RequestPageBehind1Exception(string? message, Exception? innerException)
        : base(message, innerException) // Pass both message and inner exception to base Exception class
    {
    }
  }
}
