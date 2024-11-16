namespace Talabat.APIsProject.Errors
{
    public class ApiExceptionResponses : ApiResponses
    {
        public string? Details { get; set; }
        public ApiExceptionResponses(int statusCode, string? message = null , string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}

