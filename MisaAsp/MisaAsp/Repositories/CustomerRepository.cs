using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Data.Common;
using MisaAsp.Repositories.Base;

namespace MisaAsp.Repositories
{

    public interface ICustomerRepository : IBaseRepository
    {
        Task<IEnumerable<CustomerVM>> GetAllCustomerAsync();
        Task<int> CreateCustomerAsync(CustomerVM request);

    }
    
        
        public class CustomerRepository : BaseRepository, ICustomerRepository
        {
            public CustomerRepository(IDbConnection connection) : base(connection) { }
            public async Task<int> CreateCustomerAsync(CustomerVM request)
            {
                var parameters = new
                {
                    ObjectId = request.ObjectId,
                    ObjectName = request.ObjectName,
                    TaxCode = request.TaxCode,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    
                };
                return await ExecuteProcScalarAsync<int>("createcustomer", parameters);
            }
            public async Task<IEnumerable<CustomerVM>> GetAllCustomerAsync()
            {
                
                return await QueryProcAsync<CustomerVM>("getallcustomers", null);
        }
        } 
}
