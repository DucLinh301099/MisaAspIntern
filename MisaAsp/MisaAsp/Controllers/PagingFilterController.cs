using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Controllers.Base;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagingFilterController : BaseController
    {
        private readonly IPagingFilterService _pagingfiltertService;

        public PagingFilterController(IPagingFilterService pagingfiltertService, ResOutput response) : base(response)
        {
            _pagingfiltertService = pagingfiltertService;
        }

        /// <summary>
        /// Api tạo mới một payment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /*[HttpPost("create-payment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PagingFilter(PagingFilterVM pagingFilter)
        {

        }*/
    }
}
