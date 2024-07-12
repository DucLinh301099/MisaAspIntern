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
        Task<IEnumerable<BankAccountVM>> GetBankAccountsByTypeAsync(int roleId);
        Task<int> CreateBankAccountAsync(BankAccountVM request);
    }

    public class BankAccountRepository : BaseRepository, IBankAccountRepository
    {
        public BankAccountRepository(IDbConnection connection) : base(connection) { }

        public async Task<int> CreateBankAccountAsync(BankAccountVM request)
        {
            var parameters = new
            {
                AccountNumber = request.AccountNumber,
                BankName = request.BankName,
                Branch = request.Branch,
                TypeOfBank = request.TypeOfBank,
            };
            return await ExecuteProcScalarAsync<int>("createbankaccount", parameters);
        }

        public async Task<IEnumerable<BankAccountVM>> GetBankAccountsByTypeAsync(int typeOfBank)
        {
            var sql = "SELECT * FROM getbankbytype(@TypeOfBank)";
            return await QueryAsync<BankAccountVM>(sql, new { TypeOfBank = typeOfBank });
        }
    }
}
