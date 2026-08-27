namespace Library_management_system.Models.Exceptions
{
    public class CustomExceptions:System.Exception
    {
        public int StatusCode { get; set; } = 400;

        public CustomExceptions(string message) : base(message) { 
        }
        public CustomExceptions(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
