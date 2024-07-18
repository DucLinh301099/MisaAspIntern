using MisaAsp.Models.DTO;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories.Base;
using System.Data;

namespace MisaAsp.Repositories
{
    public interface IAccountantRepository : IBaseRepository
    {
        Task<IEnumerable<AccountantGenericDTO>> GetServicesAsync();
    }

    public class AccountantRepository : BaseRepository, IAccountantRepository
    {
        public AccountantRepository(IDbConnection connection) : base(connection) { }

        public async Task<IEnumerable<AccountantGenericDTO>> GetServicesAsync()
        {

            return await QueryProcAsync<AccountantGenericDTO>("getallservices", null);
        }
    }
}
