using MisaAsp.Models.Ulti;
using Newtonsoft.Json;
using System.Net;

namespace MisaAsp.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var res = new ResOutput();
            res.HandleError("Đã xảy ra lỗi trong quá trình xử lý yêu cầu", exception.Message);

            var result = JsonConvert.SerializeObject(res);
            return context.Response.WriteAsync(result);
        }
    }
}
