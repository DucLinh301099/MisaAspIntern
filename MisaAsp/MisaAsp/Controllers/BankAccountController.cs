using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Controllers.Base;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : BaseController
    {
        private readonly IBankAccountService _bankaccountService;

        public BankAccountController(IBankAccountService bankaccountService, ResOutput _response) : base(_response)
        {
            _bankaccountService = bankaccountService;
        }

        /// <summary>
        /// Api tạo mới 1 tài khoản ngân hàng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-bank-account")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountVM request)
        {
            var bankId = await _bankaccountService.CreateBankAccountAsync(request);
            
            if (bankId > 0)
            {
                _response.HandleSuccess("Đăng kí thành công", new { BankId = bankId });
            }
            else
            {
                _response.HandleError("Đăng kí thất bại", new { BankId = bankId });
            }

            return Ok(_response);
        }

        /// <summary>
        /// Api hiển thị bankaccount theo roleid
        /// dùng roleid để xác định 2 loại tài khoản
        /// 1 là tài khoản chi, 2 là tài khoản nhận
        /// </summary>
        /// <param name="typeOfBank"></param>
        /// <returns></returns>
        [HttpGet("bank-account")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBankAccountsByTypeAsync(int typeOfBank)
        {
            var bankAccount = await _bankaccountService.GetBankAccountsByTypeAsync(typeOfBank);

            if (bankAccount == null)
            {
                _response.HandleError("Không tìm thấy BankAccount theo roleId này", new { TypeOfBank = typeOfBank });
            }
            else
            {
                _response.HandleSuccess("Tìm thấy BankAccount", bankAccount);
            }
            
            return Ok(_response);
        }
    }
}
