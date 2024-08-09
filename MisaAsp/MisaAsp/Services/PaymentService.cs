using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;

public interface IPaymentService
{
    Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails);
    Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync();
    Task<bool> DeletePaymentAsync(int id);
    Task<PaymentMasterVM> GetPaymentWithDetailsByIdAsync(int id);
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

    public async Task<bool> DeletePaymentAsync(int id)
    {
        return await _paymentRepository.DeletePaymentAsync(id);
    }

    public async Task<PaymentMasterVM> GetPaymentWithDetailsByIdAsync(int id)
    {
        return await _paymentRepository.GetPaymentWithDetailsByIdAsync(id);
    }
}
