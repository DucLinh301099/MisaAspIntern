using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MisaAsp.Services
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetBankAccountByRoleAsync(int roleId);
        Task<int> CreateBankAccountAsync(CreateBankAccount request);
    }

    public class BankAccountService : IBankAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IBankAccountRepository _bankaccountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BankAccountService(IBankAccountRepository bankaccountRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _bankaccountRepo = bankaccountRepo;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateBankAccountAsync(CreateBankAccount request)
        {
            return await _bankaccountRepo.CreateBankAccountAsync(request);
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountByRoleAsync(int roleId)
        {
            return await _bankaccountRepo.GetBankAccountsByRoleAsync(roleId);
        }
    }
}
