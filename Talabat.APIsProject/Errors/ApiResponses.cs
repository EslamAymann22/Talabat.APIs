
namespace Talabat.APIsProject.Errors
{
    public class ApiResponses
    {

        int StatusCode;
        string? Message = null;

        public ApiResponses(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode();
        }

        private string? GetDefaultMessageForStatusCode()
        {
            {
                // 500 => Internal Server Error
                // 400 => Bad Request
                // 401 => Unthorizored
                // 404 => Not Found
                // C# 7
                return StatusCode switch
                {
                    400 => "Bad Request",
                    401 => "You are not Authorized",
                    404 => "Resource Not Found",
                    500 => "Internal Server Error",
                    _ => null
                };
            }
        }
    }
}

