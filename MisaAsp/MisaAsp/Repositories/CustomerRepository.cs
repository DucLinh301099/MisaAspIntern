using MisaAsp.Models.BaseModel;
using MisaAsp.Models.ViewModel;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Data.Common;

namespace MisaAsp.Repositories
{

    public interface ICustomerRepository : IBaseRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<int> CreateCustomerAsync(CreateCustomer request);

    }
    
        
        public class CustomerRepository : BaseRepository, ICustomerRepository
        {
            public CustomerRepository(IDbConnection connection) : base(connection) { }
            public async Task<int> CreateCustomerAsync(CreateCustomer request)
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
            public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
            {
                var sql = "SELECT * FROM getallcustomers()";
                return await QueryAsync<Customer>(sql);
            }
        } 
}
