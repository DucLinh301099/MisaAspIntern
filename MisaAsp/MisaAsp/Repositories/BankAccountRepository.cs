using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MisaAsp.Repositories
{
    public interface IBankAccountRepository : IBaseRepository
    {
        Task<IEnumerable<BankAccount>> GetBankAccountsByRoleAsync(int roleId);
        Task<int> CreateBankAccountAsync(CreateBankAccount request);
    }

    public class BankAccountRepository : BaseRepository, IBankAccountRepository
    {
        public BankAccountRepository(IDbConnection connection) : base(connection) { }

        public async Task<int> CreateBankAccountAsync(CreateBankAccount request)
        {
            var parameters = new
            {
                AccountNumber = request.AccountNumber,
                BankName = request.BankName,
                Branch = request.Branch,
                RoleId = request.RoleId,
            };
            return await ExecuteProcScalarAsync<int>("createbankaccount", parameters);
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsByRoleAsync(int roleId)
        {
            var sql = "SELECT * FROM getallbankaccountbyroleid(@RoleId)";
            return await QueryAsync<BankAccount>(sql, new { RoleId = roleId });
        }
    }
}
