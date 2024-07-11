using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaAsp.Models.Ulti;
using MisaAsp.Models.ViewModel;
using MisaAsp.Services;

namespace MisaAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankaccountService;
        public BankAccountController(IBankAccountService bankaccountService)
        {
            _bankaccountService = bankaccountService;
        }
        [HttpPost("CreateBankAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccount request)
        {
            var res = new ResOutput();

            try
            {
                if (!ModelState.IsValid)
                {
                    res.HandleError("Thất bại");
                }
                else
                {
                    var bankId = await _bankaccountService.CreateBankAccountAsync(request);

                    if (bankId > 0)
                    {
                        res.HandleSuccess("Đăng kí thành công", new { BankId = bankId });
                    }
                    else
                    {
                        res.HandleError("Đăng kí thất bại", new { BankId = bankId });
                    }
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.HandleError(ex.Message);
                return BadRequest(res);
            }
        }
        [HttpGet("bankAccount")]
        public async Task<IActionResult> GetBankAccountByRoleAsync(int roleId)
        {
            var res = new ResOutput();
            var bankAccount = await _bankaccountService.GetBankAccountByRoleAsync(roleId);
            if (bankAccount == null)
            {
                res.HandleError("Không tìm thấy BankAccount theo roleId này", new { RoleId = roleId });
                return Ok(res);
            }
            return Ok(bankAccount);
        }

    }
}
