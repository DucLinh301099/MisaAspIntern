using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MisaAsp.Models.Ulti;
using Newtonsoft.Json;

namespace MisaAsp.Attribute
{
    public class ModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext currentContext)
        {
            if (!currentContext.ModelState.IsValid)
            {
                //quét hết từng prop sai để append lỗi
                var errorList = new List<string>();
                var errorCode = new List<ColumnError>();
                foreach (var item in currentContext.ModelState)
                {
                    if (item.Value != null && item.Value.Errors.Any())
                    {
                        foreach (var errorItem in item.Value.Errors)
                        {
                            var error = errorItem.ErrorMessage.ToString().Split('_');

                            // này là messageText
                            errorList.Add(error[1]);

                            // này là mã code
                            errorCode.Add(new ColumnError
                            {
                                FieldName = item.Key,
                                ErrorCode = error[0]
                            });
                        }
                    }
                }

                var res = new ResOutput();
                res.HandleError(string.Join(", ", errorList), null, errorCode);
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
