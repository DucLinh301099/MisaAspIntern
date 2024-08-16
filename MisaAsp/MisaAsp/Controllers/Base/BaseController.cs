using Microsoft.AspNetCore.Mvc;
using MisaAsp.Attribute;
using MisaAsp.Models.Ulti;
using MisaAsp.Services.Base;

namespace MisaAsp.Controllers.Base
{
    [ModelState]
    public class BaseController : ControllerBase
    {
        protected readonly ResOutput _response;
        private IBaseService _baseService;

        public BaseController(ResOutput response, IBaseService baseService)
        {
            _response = response;
            _baseService = baseService;
        }

        [HttpPost("get-paging-filter")]
        public async Task<IActionResult> GetPagingAsync(PagingFilter pagingFilter)
        {
            
                var result = await _baseService.GetPaging(pagingFilter);
                return Ok(result);
            
        }
    }
}
