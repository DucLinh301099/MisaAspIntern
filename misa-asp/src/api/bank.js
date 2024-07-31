// src/api/bankAccount.js
import { baseApi } from './baseApi.js';
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
    const responseData = await baseApi.postAuthenApi(Api.bank.url, params);
    return responseData;
    
  }
};
