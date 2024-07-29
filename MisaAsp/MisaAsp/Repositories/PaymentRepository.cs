using Dapper;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories.Base;
using System.Data;

namespace MisaAsp.Repositories
{
    public interface IPaymentRepository : IBaseRepository
    {
        Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails);
        Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync();

    }
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(IDbConnection connection) : base(connection) { }

        public async Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails)
        {
            var paymentMasterParameters = new
            {
                VoucherType = paymentMaster.VoucherType,
                PaymentMethod = paymentMaster.PaymentMethod,
                BillContent = paymentMaster.BillContent,
                AccountingDate = paymentMaster.AccountingDate,
                DocumentDate = paymentMaster.DocumentDate,
                DocumentNumber = paymentMaster.DocumentNumber,
                TotalAmount = paymentMaster.TotalAmount,
                EmployeeName = paymentMaster.EmployeeName,
                ObjectName = paymentMaster.ObjectName,
                Address = paymentMaster.Address,
                BankName = paymentMaster.BankName,
                AccountNumber = paymentMaster.AccountNumber,
                BankAccountId = paymentMaster.BankAccountId,
                CustomerId = paymentMaster.CustomerId,
                EmployeeId = paymentMaster.EmployeeId
            };

            var paymentMasterId = await ExecuteProcScalarAsync<int>("insert_paymentmaster", paymentMasterParameters);

            foreach (var paymentDetail in paymentDetails)
            {
                var paymentDetailParameters = new
                {
                    DebitAccount = paymentDetail.DebitAccount,
                    CreditAccount = paymentDetail.CreditAccount,
                    Amount = paymentDetail.Amount,
                    ObjectId = paymentDetail.ObjectId,
                    PaymentId = paymentMasterId,
                    ObjectName = paymentDetail.ObjectName
                };

                await ExecuteProcScalarAsync<int>("insert_paymentdetail", paymentDetailParameters);
            }

            return paymentMasterId;
        }
        public async Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync()
        {
            var payments = await QueryProcAsync<PaymentMasterVM>("getallpayments", null);

            return payments;
        }




    }
}
