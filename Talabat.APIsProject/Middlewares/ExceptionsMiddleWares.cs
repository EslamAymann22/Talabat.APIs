using System.Net;

namespace Talabat.APIsProject.Middlewares
{
    public class ExceptionsMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public ExceptionsMiddleWares(RequestDelegate next ,ILogger logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (_env.IsDevelopment())
                {
                    //var Response = H
                }


            }
        }

    }
}
