using Microsoft.AspNetCore.Mvc;
using MisaAsp.Attribute;
using MisaAsp.Models.Ulti;

namespace MisaAsp.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [ModelState]
    public class BaseController : ControllerBase
    {
        protected readonly ResOutput _response;

        public BaseController(ResOutput response)
        {
            _response = response;
        }
    }
}
