using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;

namespace MisaAsp.Services
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountVM>> GetBankAccountsByTypeAsync(int typeOfBank);
        Task<int> CreateBankAccountAsync(BankAccountVM request);
    }

    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankaccountRepo;

        public BankAccountService(IBankAccountRepository bankaccountRepo)
        {
            _bankaccountRepo = bankaccountRepo;
        }

        public async Task<int> CreateBankAccountAsync(BankAccountVM request)
        {
            return await _bankaccountRepo.CreateBankAccountAsync(request);
        }

        public async Task<IEnumerable<BankAccountVM>> GetBankAccountsByTypeAsync(int typeOfBank)
        {
            return await _bankaccountRepo.GetBankAccountsByTypeAsync(typeOfBank);
        }
    }
}
