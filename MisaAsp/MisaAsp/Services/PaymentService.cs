using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;

namespace MisaAsp.Services
{
    public interface IPaymentService
    {
        Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails);
        Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync();
    }
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails)
        {
            return await _paymentRepository.AddPaymentAsync(paymentMaster, paymentDetails);
        }
        public async Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

    }
}
