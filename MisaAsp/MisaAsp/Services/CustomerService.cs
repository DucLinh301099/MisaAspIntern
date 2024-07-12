using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;

namespace MisaAsp.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerVM>> GetAllCustomerAsync();
        Task<int> CreateCustomerAsync(CustomerVM request);
    }
     
        public class CustomerService : ICustomerService
        {
            private readonly ICustomerRepository _customerRepo;
            public CustomerService(ICustomerRepository customerRepo)
            {
                _customerRepo = customerRepo;
            }
            public async Task<IEnumerable<CustomerVM>> GetAllCustomerAsync()
            {
                return await _customerRepo.GetAllCustomerAsync();
            }
            public async Task<int> CreateCustomerAsync(CustomerVM request)
            {
                return await _customerRepo.CreateCustomerAsync(request);
            }

        }
    
}
