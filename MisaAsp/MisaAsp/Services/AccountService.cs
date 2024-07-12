using Microsoft.IdentityModel.Tokens;
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
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneUniqueAsync(string phone);
        Task<int> RegisterUserAsync(RegistrationRequestVM request);
        Task<AuthResult> AuthenticateUserAsync(LoginRequestVM request);
        Task<IEnumerable<UserRequestVM>> GetAllUsersAsync();
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestVM request);
        Task<IEnumerable<EmployeeVM>> GetAllEmployeeAsync();
        Task<int> CreateEmployeeAsync(EmployeeVM request);
        Task<bool> DeleteUserAsync(int userId);
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

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _accountRepo.DeleteUserAsync(userId);
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

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _accountRepo.IsEmailUniqueAsync(email);
        }

        public async Task<bool> IsPhoneUniqueAsync(string phoneNumber)
        {
            return await _accountRepo.IsPhoneUniqueAsync(phoneNumber);
        }

        public async Task<int> RegisterUserAsync(RegistrationRequestVM request)
        {
            if (request != null)
            {
                if (!await IsEmailUniqueAsync(request.Email))
                {
                    throw new Exception("Email đã sử dụng");
                }

                if (!await IsPhoneUniqueAsync(request.PhoneNumber))
                {
                    throw new Exception("Số điện thoại đã được sử dụng");
                }

                request.Password = GetMd5Hash(request.Password);
            }

            return await _accountRepo.RegisterUserAsync(request);
        }

        public async Task<int> CreateEmployeeAsync(EmployeeVM request)
        {
            return await _accountRepo.CreateEmployeeAsync(request);
        }

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
                        Secure = true, // Đảm bảo cookie chỉ được gửi qua HTTPS
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
