using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, ResOutput _response) : base(_response)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Api đăng ký mới 1 tài khoản
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
       
        public async Task<IActionResult> Register([FromBody] RegistrationRequestVM request)
        {
            if (!ModelState.IsValid)
            {
                _response.HandleError();
                return Ok(_response);
            }

            var userId = await _accountService.RegisterUserAsync(request);

            if (userId > 0)
            {
                _response.HandleSuccess("Đăng kí thành công", new { UserId = userId });
            }
            else
            {
                _response.HandleError("Đăng kí thất bại", new { UserId = userId });
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api tạo mới 1 nhân viên
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("createEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeVM request)
        {
            if (!ModelState.IsValid)
            {
                _response.HandleError();
                return Ok(_response);
            }

            var employeeId = await _accountService.CreateEmployeeAsync(request);

            if (employeeId > 0)
            {
                _response.HandleSuccess("Tạo mới thành công", new { EmployeeId = employeeId });
            }
            else
            {
                _response.HandleError("Tạo mới thất bại", new { EmployeeId = employeeId });
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api hiển thị tất cả các nhân viên hiện có
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee")]
        
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await _accountService.GetAllEmployeeAsync();

            if (employees != null && employees.Any())
            {
                _response.HandleSuccess("Lấy thông tin nhân viên thành công", employees);
            }
            else
            {
                _response.HandleError("Lấy thông tin nhân viên thất bại");
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api đăng nhập 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]    
        public async Task<IActionResult> Login([FromBody] LoginRequestVM request)
        {
            if (!ModelState.IsValid)
            {
                _response.HandleError("Thất bại");
                return Ok(_response);
            }

            if (Request.Cookies.ContainsKey("AuthToken"))
            {
                Response.Cookies.Delete("AuthToken");
            }

            var authResult = await _accountService.AuthenticateUserAsync(request);

            if (authResult != null && !string.IsNullOrEmpty(authResult.Role))
            {
                var user = await _accountService.GetUserByIdAsync(authResult.UserId);
                _response.HandleSuccess("Đăng nhập thành công", new { Role = authResult.Role, Token = authResult.Token, LastName = user.LastName });
            }
            else
            {
                _response.HandleError("Thông tin đăng nhập không hợp lệ");
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            _response.HandleSuccess("Đăng xuất thành công");
            return Ok(_response);
        }

        /// <summary>
        /// Api hiển thị tất cả các user
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        [Authorize(Roles = "Admin")] // Chỉ admin mới có quyền truy cập
        public async Task<IActionResult> GetUsers()
        {
            var users = await _accountService.GetAllUsersAsync();

            if (users != null && users.Any())
            {
                _response.HandleSuccess("Lấy thông tin người dùng thành công", users);
            }
            else
            {
                _response.HandleError("Lấy thông tin người dùng thất bại");
            }
            return Ok(_response);
        }

        [HttpGet("users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _accountService.GetUserByIdAsync(id);
            if (user == null)
            {
                _response.HandleError("Không tìm thấy tài khoản");

            }
            else
            {
                _response.HandleSuccess(" Tìm thấy tài khoản" ,user);
                
            }
            return Ok(_response);
        }

        /// <summary>
        /// Api cập nhật User theo id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")] // Chỉ admin và người dùng mới có quyền truy cập
        public async Task<IActionResult> UpdateUser(UpdateUserVM user)
        {
            var result = await _accountService.UpdateUserAsync(user);
            if (result != null)
            {
                _response.HandleSuccess("Cập nhật người dùng thành công",user);
            }
            else
            {
                _response.HandleError("Cập nhật người dùng thất bại");
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api xóa 1 user theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("users/{id}")]
        [Authorize(Roles = "Admin")] // Chỉ admin mới có quyền truy cập
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _accountService.DeleteUserAsync(id);
            if (deleted != null)
            {
                _response.HandleSuccess("Xóa người dùng thành công");
            }
            else
            {
                _response.HandleError("Xóa người dùng thất bại");
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api quên mật khẩu   
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
       
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestVM request)
        {
            var _response = new ResOutput();
            if (!ModelState.IsValid)
            {
                _response.HandleError();
                return Ok(_response);
            }

            var result = await _accountService.ForgotPasswordAsync(request);

            if (result)
            {
                _response.HandleSuccess("Liên kết đặt lại mật khẩu đã được gửi đến email của bạn");
            }
            else
            {
                _response.HandleError("Không tìm thấy email");
            }
            return Ok(_response);
        }
    }
}
