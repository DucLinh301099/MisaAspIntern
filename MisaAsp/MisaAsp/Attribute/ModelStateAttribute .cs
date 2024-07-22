using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MisaAsp.Models.Ulti;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MisaAsp.Attribute
{
    /// <summary>
    /// Attribute để kiểm tra trạng thái của model trước khi thực hiện hành động
    /// </summary>
    public class ModelStateAttribute : ActionFilterAttribute
    {
        // Override phương thức OnActionExecuting để kiểm tra trạng thái của model
        public override void OnActionExecuting(ActionExecutingContext currentContext)
        {
           
            if (!currentContext.ModelState.IsValid)
            {
                // Tạo danh sách các lỗi
                var errorCode = new List<ColumnError>();
                foreach (var item in currentContext.ModelState)
                {
                    // Kiểm tra nếu có lỗi trong item hiện tại
                    if (item.Value != null && item.Value.Errors.Any())
                    {
                        bool hasRequiredError = false;
                        foreach (var errorItem in item.Value.Errors)
                        {
                            // Tách thông báo lỗi theo dấu gạch dưới '_'
                            var error = errorItem.ErrorMessage.ToString().Split('_');

                            // Kiểm tra nếu lỗi là "Required"
                            if (error[0] == "Required")
                            {
                                // Thêm lỗi vào danh sách lỗi
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

                        // Nếu không có lỗi "Required"
                        if (!hasRequiredError)
                        {
                            foreach (var errorItem in item.Value.Errors)
                            {
                                var error = errorItem.ErrorMessage.ToString().Split('_');

                                // Kiểm tra nếu lỗi có đúng định dạng "Code_Text"
                                if (error.Length == 2)
                                {
                                    // Thêm lỗi vào danh sách lỗi
                                    errorCode.Add(new ColumnError
                                    {
                                        FieldName = item.Key,
                                        ErrorCode = error[0],
                                        ErrorText = error[1]
                                    });
                                }
                                else
                                {
                                    // Thêm lỗi định dạng không hợp lệ vào danh sách lỗi
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

                // Tạo đối tượng ResOutput và xử lý lỗi
                var res = new ResOutput();
                res.HandleError(null, null, errorCode);
                // Thiết lập kết quả trả về của action
                currentContext.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(res),
                    StatusCode = 200,
                    ContentType = "application/json"
                };
            }
            else
            {
                // Nếu ModelState hợp lệ, gọi phương thức của base class
                base.OnActionExecuting(currentContext);
            }
        }
    }
}
