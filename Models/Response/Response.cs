
namespace Library_management_system.Models.Response
{
    public class Response
    {
        public object? Data { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public string ResponseMessage { get; set; } = "Operation Completed Successfully";
    }
}
