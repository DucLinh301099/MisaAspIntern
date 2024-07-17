using Microsoft.AspNetCore.Mvc.Controllers;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace MisaAsp.Middleware
{
    public class BeforeAPIMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public BeforeAPIMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            //{
            //    var endpoint = context.GetEndpoint();
            //    if (endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor descriptor)
            //    {
            //        var actionParameters = descriptor.Parameters;
            //        foreach (var parameter in actionParameters)
            //        {
            //            if (typeof(IValidatableObject).IsAssignableFrom(parameter.ParameterType))
            //            {
            //                context.Request.EnableBuffering();
            //                context.Request.Body.Position = 0;
            //                var model = await JsonConvert.DeserializeObject(context.Request.Body, parameter.ParameterType);
            //                context.Request.Body.Position = 0;

            //                var validationContext = new ValidationContext(model, null, null);
            //                var validationResults = new List<ValidationResult>();
            //                var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            //                if (!isValid)
            //                {
            //                    var response = new
            //                    {
            //                        Success = false,
            //                        Errors = validationResults.Select(r => r.ErrorMessage)
            //                    };

            //                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //                    context.Response.ContentType = "application/json";
            //                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
