using Dapper;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories.Base;
using Newtonsoft.Json;
using System.Data;

namespace MisaAsp.Repositories
{
    public interface IPaymentRepository : IBaseRepository
    {
        Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails);
        Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync();
        Task<bool> DeletePaymentAsync(int id);           
        Task<PaymentMasterVM> GetPaymentWithDetailsByIdAsync(int id);

    }
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(IDbConnection connection) : base(connection) { }

        public async Task<int> AddPaymentAsync(PaymentMasterVM paymentMaster, List<PaymentDetailVM> paymentDetails)
        {
            // Chuyển đổi danh sách paymentDetails thành chuỗi JSON
            var paymentDetailJson = JsonConvert.SerializeObject(paymentDetails);

            // Thiết lập các tham số cho hàm SQL
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
                CustomerName = paymentMaster.CustomerName,
                Address = paymentMaster.Address,
                BankName = paymentMaster.BankName,
                AccountNumber = paymentMaster.AccountNumber,
                BankAccountId = paymentMaster.BankAccountId,
                CustomerId = paymentMaster.CustomerId,
                EmployeeId = paymentMaster.EmployeeId,
                PaymentDetailsJson = paymentDetailJson
            };

            // Gọi hàm SQL insert_paymentmaster và lấy id của bản ghi vừa chèn
            var paymentMasterId = await ExecuteProcScalarAsync<int>("insert_paymentmaster", paymentMasterParameters);

            return paymentMasterId;
        }



        

        public async Task<IEnumerable<PaymentMasterVM>> GetAllPaymentsAsync()
        {
            var payments = await QueryProcAsync<PaymentMasterVM>("getallpayments", null);
            

            return payments;
        }
        public async Task<bool> DeletePaymentAsync(int id)
        {
            var parameters = new
            { paymentmaster_id = id, };
            var result = await ExecuteProcScalarAsync<bool>("deletepayment", parameters);
            return result;
        }
        public async Task<PaymentMasterVM> GetPaymentWithDetailsByIdAsync(int id)
        {
            var parameters = new { PaymentMasterId = id };
            var paymentDictionary = new Dictionary<int, PaymentMasterVM>();

            var result = await ExecuteProcQueryWithMappingAsync<PaymentMasterVM, PaymentDetailVM>(
                "get_payment_with_details_by_id",
                parameters,
                (payment, detail) =>
                {
                    if (!paymentDictionary.TryGetValue(payment.Id, out var currentPayment))
                    {
                        currentPayment = payment;
                        currentPayment.PaymentDetails = new List<PaymentDetailVM>();
                        paymentDictionary.Add(currentPayment.Id, currentPayment);
                    }
                    if (detail != null)
                    {
                        currentPayment.PaymentDetails.Add(detail);
                    }
                    return currentPayment;
                },
                splitOn: "detailsid" // Đảm bảo rằng tên cột này khớp với tên cột detail ID trong bảng
            );

            return paymentDictionary.Values.FirstOrDefault();
        }







    }
}
