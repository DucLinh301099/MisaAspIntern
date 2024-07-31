using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Data.Common;
using MisaAsp.Repositories.Base;

namespace MisaAsp.Repositories
{
    public interface IAccountRepository : IBaseRepository
    {
        Task<bool> UpdateUserAsync(UpdateUserVM user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<UserRequestVM>> GetAllUsersAsync();
        
        Task<IEnumerable<EmployeeVM>> GetAllEmployeeAsync();
        Task<int> RegisterUserAsync(RegistrationRequestVM request);
        Task<int> CreateEmployeeAsync(EmployeeVM request);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneUniqueAsync(string phoneNumber);
        Task<bool> AuthenticateUserAsync(LoginRequestVM request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestVM request);
        Task<RoleAccount> GetUserRoleAsync(string emailOrPhoneNumber);
        Task<UpdateUserVM> GetUserByIdAsync(int id);
      


    }

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IDbConnection connection) : base(connection) { }

        public async Task<bool> UpdateUserAsync(UpdateUserVM user)
        {
         
            var parameters = new
            {
                user_id = user.Id, 
                first_name = user.FirstName,
                last_name = user.LastName,
                Email = user.Email,
                phone_number = user.PhoneNumber
            };
            var result = await ExecuteProcScalarAsync<bool>("updateuser", parameters);
            return result;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {          
            var parameters = new
            { user_id = id, };
            var result = await ExecuteProcScalarAsync<bool>("delete_user", parameters );
            return result;
        }

        public async Task<IEnumerable<UserRequestVM>> GetAllUsersAsync()
        {
            
            return await QueryProcAsync<UserRequestVM>("getallusers", null);
        }

        public async Task<IEnumerable<EmployeeVM>> GetAllEmployeeAsync()
        {
            
            return await QueryProcAsync<EmployeeVM>("getallemployees", null);
        }
        public async Task<UpdateUserVM> GetUserByIdAsync(int id)
        {          
            var parameters = new
            { p_id = id, };
            return await ExecuteProcSingleScalarAsync<UpdateUserVM>("getuserbyid", parameters);
        }
        

        public async Task<int> RegisterUserAsync(RegistrationRequestVM request)
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
        public async Task<int> CreateEmployeeAsync(EmployeeVM request)
        {
            var parameters = new
            {
                EmployeeCode = request.EmployeeCode,
                EmployeeName = request.EmployeeName,
                Department = request.Department,
                PhoneNumber = request.PhoneNumber,
           
            };
            return await ExecuteProcScalarAsync<int>("createemployee", parameters);
        }
       


        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var parameters = new { p_email = email };
            var count = await ExecuteProcSingleScalarAsync<int>("getusercountbyemail", parameters);
            return count == 0;
        }

        public async Task<bool> IsPhoneUniqueAsync(string phoneNumber)
        {
            var parameters = new { p_phoneNumber = phoneNumber };
            var count = await ExecuteProcSingleScalarAsync<int>("getusercountbyphonenumber", parameters);
            return count == 0;
        }

        public async Task<bool> AuthenticateUserAsync(LoginRequestVM request)
        {
            var parameters = new
            {
                EmailOrPhoneNumber = request.EmailOrPhoneNumber,
                Password = request.Password,
            };
            return await ExecuteProcScalarAsync<bool>("authenticateuser", parameters);
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestVM request)
        {
            var parameters = new
            {
                Email = request.Email
            };
            return await ExecuteProcSingleScalarAsync<bool>("checkemailexists", parameters);
           
        }

        public async Task<RoleAccount> GetUserRoleAsync(string emailOrPhoneNumber)
        {
            
            var parameters = new { EmailOrPhoneNumber = emailOrPhoneNumber };
            return await ExecuteProcSingleScalarAsync<RoleAccount>("getuserrole", parameters);
        }

    }
}
