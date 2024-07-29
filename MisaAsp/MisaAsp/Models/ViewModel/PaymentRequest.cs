using System.Collections.Generic;

namespace MisaAsp.Models.ViewModel
{
    public class PaymentRequest
    {
        public PaymentMasterVM PaymentMaster { get; set; }
        public List<PaymentDetailVM> PaymentDetails { get; set; }
    }
}
