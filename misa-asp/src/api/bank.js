// src/api/bankAccount.js
import { base } from '../api/base.js';
import Api from '../api/apiConst';

export const bankAccount = {
  /**
   * Hàm tạo mới 1 tài khoản ngân hàng
   * Có 2 loại: Chi và Nhận
   * Payment Page
   * @param {*} accountNumber 
   * @param {*} bankName 
   * @param {*} branch 
   * @param {*} typeOfBank 
   * @returns 
   */
  async createBankAccount(accountNumber, bankName, branch, typeOfBank) {
    let params = {
      AccountNumber: accountNumber,
      BankName: bankName,
      Branch: branch,
      TypeOfBank: typeOfBank,
    };
    const reponse = await base.postApi(Api.bank.url, params);
    return reponse.data;
    
  }
};
