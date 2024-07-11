using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Data.Common;

namespace MisaAsp.Repositories
{
    public interface IAccountRepository : IBaseRepository
    {
        Task<bool> UpdateUserAsync(UpdateUser user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<UserRequest>> GetAllUsersAsync();
        
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task<int> RegisterUserAsync(RegistrationRequest request);
        Task<int> CreateEmployeeAsync(CreateEmployee request);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneUniqueAsync(string phoneNumber);
        Task<bool> AuthenticateUserAsync(LoginRequest request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<RoleAccount> GetUserRoleAsync(string emailOrPhoneNumber);
        Task<UpdateUser> GetUserByIdAsync(int id);
       // Task<UserRequest> GetLastNameById(int id);


    }

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IDbConnection connection) : base(connection) { }

        public async Task<bool> UpdateUserAsync(UpdateUser user)
        {
            var sql = "SELECT updateuser(@user_id, @first_name, @last_name, @Email, @phone_number)";
            var parameters = new
            {
                user_id = user.Id,
                first_name = user.FirstName,
                last_name = user.LastName,
                Email = user.Email,
                phone_number = user.PhoneNumber,
               
            };
            var result = await ExecuteScalarAsync<bool>(sql, parameters);
            return result;
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var sql = "SELECT delete_user(@user_id)";
            var result = await ExecuteScalarAsync<bool>(sql, new { user_id = userId });
            return result;
        }

        public async Task<IEnumerable<UserRequest>> GetAllUsersAsync()
        {
            var sql = "SELECT * FROM GetAllUsers()";
            return await QueryAsync<UserRequest>(sql);
        }
        public async Task<UpdateUser> GetUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Registrations WHERE Id = @Id";
            return await QueryFirstOrDefaultAsync<UpdateUser>(sql, new { Id = id });
        }
        //public async Task<UpdateUser> GetLastNameById(int id)
       // {
       //     var sql = "SELECT * FROM Registrations WHERE Id = @Id";
       //     return await QueryFirstOrDefaultAsync<UpdateUser>(sql, new { Id = id });
       // }

        public async Task<int> RegisterUserAsync(RegistrationRequest request)
        {
            var parameters = new
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                RoleId = request.RoleId,
            };
            return await ExecuteProcScalarAsync<int>("registeruser", parameters);
        }
        public async Task<int> CreateEmployeeAsync(CreateEmployee request)
        {
            var parameters = new
            {
                EmployeeCode = request.EmployeeCode,
                EmployeeName = request.EmployeeName,
                Department = request.Department,             
                MobilePhone = request.MobilePhone,
           
            };
            return await ExecuteProcScalarAsync<int>("createemployee", parameters);
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            var sql = "SELECT * FROM getallemployees()";
            return await QueryAsync<Employee>(sql);
        }


        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var query = "SELECT COUNT(1) FROM Registrations WHERE Email = @Email";
            var count = await ExecuteScalarAsync<int>(query, new { Email = email });
            return count == 0;
        }

        public async Task<bool> IsPhoneUniqueAsync(string phoneNumber)
        {
            var query = "SELECT COUNT(1) FROM Registrations WHERE PhoneNumber = @PhoneNumber";
            var count = await ExecuteScalarAsync<int>(query, new { PhoneNumber = phoneNumber });
            return count == 0;
        }

        public async Task<bool> AuthenticateUserAsync(LoginRequest request)
        {
            var parameters = new
            {
                EmailOrPhoneNumber = request.EmailOrPhoneNumber,
                Password = request.Password,
            };
            return await ExecuteProcScalarAsync<bool>("authenticateuser", parameters);
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var sql = "SELECT CheckEmailExists(@Email)";
            var parameters = new { request.Email };
            return await ExecuteScalarAsync<bool>(sql, parameters);
        }

        public async Task<RoleAccount> GetUserRoleAsync(string emailOrPhoneNumber)
        {
            var sql = @"
                SELECT ra.UserId, ra.RoleId, r.RoleName
                FROM RoleAccount ra
                JOIN Roles r ON ra.RoleId = r.Id
                JOIN Registrations u ON ra.UserId = u.Id
                WHERE u.Email = @EmailOrPhoneNumber OR u.PhoneNumber = @EmailOrPhoneNumber";
            return await QuerySingleOrDefaultAsync<RoleAccount>(sql, new { EmailOrPhoneNumber = emailOrPhoneNumber });
        }
    }
}
