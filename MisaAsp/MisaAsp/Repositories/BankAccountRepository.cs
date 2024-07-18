using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using MisaAsp.Repositories.Base;


namespace MisaAsp.Repositories
{
    public interface IBankAccountRepository : IBaseRepository
    {
        Task<IEnumerable<BankAccountVM>> GetBankAccountsByTypeAsync(int typeOfBank);
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
          
            var parameters = new { p_typeofbank = typeOfBank };
            return await ExecuteProcObScalarAsync<BankAccountVM>("getbankbytype", parameters);
        }
    }
}
