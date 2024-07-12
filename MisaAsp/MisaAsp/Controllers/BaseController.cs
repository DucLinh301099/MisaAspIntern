using Microsoft.AspNetCore.Mvc;
using MisaAsp.Models.Ulti;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ResOutput _response;

        public BaseController(ResOutput response)
        {
            _response = response;
        }
    }
}
