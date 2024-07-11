
using Microsoft.IdentityModel.Tokens;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using MisaAsp.Models.BaseModel;
using Microsoft.AspNetCore.Http;


namespace MisaAsp.Services
{

    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<int> CreateCustomerAsync(CreateCustomer request);
    }
   
     
        public class CustomerService : ICustomerService
        {
            private readonly IConfiguration _configuration;
            private readonly ICustomerRepository _customerRepo;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public CustomerService(ICustomerRepository customerRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            {
                _customerRepo = customerRepo;
                _configuration = configuration;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
            {
                return await _customerRepo.GetAllCustomerAsync();
            }
            public async Task<int> CreateCustomerAsync(CreateCustomer request)
            {
                return await _customerRepo.CreateCustomerAsync(request);
            }

        }
    
}
