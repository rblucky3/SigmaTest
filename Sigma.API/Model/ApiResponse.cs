namespace Sigma.API.Model
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> ValidationMessages { get; set; }

        public ApiResponse()
        { }

        public ApiResponse(int statusCode, string message, List<string> validationMessages = null)
        {
            StatusCode = statusCode;
            Message = message;
            ValidationMessages = validationMessages ?? new List<string>();
        }
    }
}
