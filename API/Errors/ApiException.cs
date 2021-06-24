namespace API.Errors
{
    public class ApiException
    {
        //if we don't provide a message and details then both properties are going to be set null 
        public ApiException(int statusCode,string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = details;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        //the "detail" going to be the stack trace that we get in order to make this easier to use
        public string Detail { get; set; }
    }
}