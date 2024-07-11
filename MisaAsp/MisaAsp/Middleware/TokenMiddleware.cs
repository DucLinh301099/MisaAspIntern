using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MisaAsp.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("AuthToken") && !context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Cookies["AuthToken"];
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
               
            }

            await _next(context);
        }
    }
}
