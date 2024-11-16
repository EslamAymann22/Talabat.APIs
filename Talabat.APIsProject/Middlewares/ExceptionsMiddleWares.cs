using System.Net;
using System.Text.Json;
using Talabat.APIsProject.Errors;

namespace Talabat.APIsProject.Middlewares
{
    public class ExceptionsMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleWares> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionsMiddleWares(RequestDelegate next, ILogger<ExceptionsMiddleWares> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ///if (_env.IsDevelopment())
                ///{
                ///    var Response = new ApiExceptionResponses((int)HttpStatusCode.InternalServerError, ex.Message,
                ///        ex.StackTrace.ToString());
                ///}
                ///else
                ///{
                ///    var Response = new ApiExceptionResponses((int)HttpStatusCode.InternalServerError);
                ///}
                var Response = _env.IsDevelopment() ? new ApiExceptionResponses((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponses((int)HttpStatusCode.InternalServerError);

                var Option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var JsonResponse = JsonSerializer.Serialize(Response, Option);
                context.Response.WriteAsync(JsonResponse);


            }
        }

    }
}
