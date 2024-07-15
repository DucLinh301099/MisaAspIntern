using MisaAsp.Models.DTO;
using MisaAsp.Repositories;

namespace MisaAsp.Services
{
    public interface IAccountantService
    {
        Task<IEnumerable<AccountantGenericDTO>> GetServicesAsync();
    }

    public class AccountantService : IAccountantService
    {
        private readonly IAccountantRepository _accountantRepo;

        public AccountantService(IAccountantRepository accountantRepo)
        {
            _accountantRepo = accountantRepo;
        }

        public async Task<IEnumerable<AccountantGenericDTO>> GetServicesAsync()
        {
            return await _accountantRepo.GetServicesAsync();
        }
    }
}
