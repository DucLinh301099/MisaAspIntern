using Microsoft.IdentityModel.Tokens;
using MisaAsp.Common;
using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MisaAsp.Services
{
    public interface IAccountService
    {
        
        Task<int> RegisterUserAsync(RegistrationRequestVM request);
        Task<AuthResult> AuthenticateUserAsync(LoginRequestVM request);
        Task<IEnumerable<UserRequestVM>> GetAllUsersAsync();
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestVM request);
        Task<IEnumerable<EmployeeVM>> GetAllEmployeeAsync();
        Task<int> CreateEmployeeAsync(EmployeeVM request);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(UpdateUserVM user);
        Task<UpdateUserVM> GetUserByIdAsync(int id);
        Task<string> GetLastNameById(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepository accountRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepo = accountRepo;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserVM user)
        {
            return await _accountRepo.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _accountRepo.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<UserRequestVM>> GetAllUsersAsync()
        {
            return await _accountRepo.GetAllUsersAsync();
        }

        public async Task<IEnumerable<EmployeeVM>> GetAllEmployeeAsync()
        {
            return await _accountRepo.GetAllEmployeeAsync();
        }

        public async Task<UpdateUserVM> GetUserByIdAsync(int id)
        {
            return await _accountRepo.GetUserByIdAsync(id);
        }

        public async Task<string> GetLastNameById(int userId)
        {
            var user = await _accountRepo.GetUserByIdAsync(userId);
            return user?.LastName;
        }

        private async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _accountRepo.IsEmailUniqueAsync(email);
        }

        private async Task<bool> IsPhoneUniqueAsync(string phoneNumber)
        {
            return await _accountRepo.IsPhoneUniqueAsync(phoneNumber);
        }
        /// <summary>
        /// Fuction xử lý đăng ký tài khoản
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> RegisterUserAsync(RegistrationRequestVM request)
        {
            if (request != null)
            {
                if (!await IsEmailUniqueAsync(request.Email))
                {
                    throw new InvalidValueException("Email","EmailUsed", "Email đã được sử dụng");
                }

                if (!await IsPhoneUniqueAsync(request.PhoneNumber))
                {
                    throw new InvalidValueException("PhoneNumber", "PhoneUsed", "Số điện thoại đã được sử dụng");
                }

                request.Password = GetMd5Hash(request.Password);
            }

            return await _accountRepo.RegisterUserAsync(request);
        }

        public async Task<int> CreateEmployeeAsync(EmployeeVM request)
        {
            return await _accountRepo.CreateEmployeeAsync(request);
        }
        /// <summary>
        /// function xử lý đăng nhập
        /// tạo token và lưu nó vào cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AuthResult> AuthenticateUserAsync(LoginRequestVM request)
        {
            if (request != null)
            {
                request.Password = GetMd5Hash(request.Password);

                if (await _accountRepo.AuthenticateUserAsync(request))
                {
                    var userRole = await _accountRepo.GetUserRoleAsync(request.EmailOrPhoneNumber);
                    if (userRole == null)
                    {
                        throw new Exception("Role không tìm thấy của người dùng này");
                    }

                    var jwtTokenHandler = new JwtSecurityTokenHandler();
                    var secretKey = _configuration.GetSection("Jwt").GetSection("SecretKey").Value;
                    if (string.IsNullOrEmpty(secretKey))
                        throw new ArgumentNullException(nameof(secretKey));

                    var tokenExpiryInHours = _configuration.GetValue<int>("Jwt:TokenExpiryInHours");
                    var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, request.EmailOrPhoneNumber),
                            new Claim(ClaimTypes.Role, userRole.RoleName)
                        }),
                        Expires = DateTime.UtcNow.AddHours(tokenExpiryInHours),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescription);
                    var tokenString = jwtTokenHandler.WriteToken(token);

                    var cookieOptions = new CookieOptions
                    {
                        //HttpOnly = true,
                        // Secure = true, // Đảm bảo cookie chỉ được gửi qua HTTPS
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(tokenExpiryInHours)
                    };
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", tokenString, cookieOptions);

                    return new AuthResult
                    {
                        Token = tokenString,
                        Role = userRole.RoleName,
                        UserId = userRole.UserId // Thêm UserId vào AuthResult
                    };
                }
            }

            return null;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestVM request)
        {
            return await _accountRepo.ForgotPasswordAsync(request);
        }

        /// <summary>
        /// Hàm gán mật khẩu thành chuỗi ký tự MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMd5Hash(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                using (var md5 = MD5.Create())
                {
                    var inputBytes = Encoding.ASCII.GetBytes(input);
                    var hashBytes = md5.ComputeHash(inputBytes);
                    var sb = new StringBuilder();
                    foreach (var t in hashBytes)
                    {
                        sb.Append(t.ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            return null;
        }
    }
}
