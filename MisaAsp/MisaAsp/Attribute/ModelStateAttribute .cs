using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MisaAsp.Models.Ulti;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MisaAsp.Attribute
{
    public class ModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext currentContext)
        {
            if (!currentContext.ModelState.IsValid)
            {
                var errorCode = new List<ColumnError>();
                foreach (var item in currentContext.ModelState)
                {
                    if (item.Value != null && item.Value.Errors.Any())
                    {
                        bool hasRequiredError = false;
                        foreach (var errorItem in item.Value.Errors)
                        {
                            var error = errorItem.ErrorMessage.ToString().Split('_');

                            if (error[0] == "Required")
                            {
                                errorCode.Add(new ColumnError
                                {
                                    FieldName = item.Key,
                                    ErrorCode = error[0],
                                    ErrorText = error[1]
                                });
                                hasRequiredError = true;
                                break;
                            }
                        }

                        if (!hasRequiredError)
                        {
                            foreach (var errorItem in item.Value.Errors)
                            {
                                var error = errorItem.ErrorMessage.ToString().Split('_');

                                if (error.Length == 2)
                                {
                                    errorCode.Add(new ColumnError
                                    {
                                        FieldName = item.Key,
                                        ErrorCode = error[0],
                                        ErrorText = error[1]
                                    });
                                }
                                else
                                {
                                    errorCode.Add(new ColumnError
                                    {
                                        FieldName = item.Key,
                                        ErrorCode = "InvalidFormat",
                                        ErrorText = errorItem.ErrorMessage
                                    });
                                }
                            }
                        }
                    }
                }

                var res = new ResOutput();
                res.HandleError(null, null, errorCode);
                currentContext.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(res),
                    StatusCode = 200,
                    ContentType = "application/json"
                };
            }
            else
            {
                base.OnActionExecuting(currentContext);
            }
        }
    }
}