// src/api/bankAccount.js
import { base } from '../api/base.js';
import Api from '../api/apiConst';

export const bankAccount = {
  // function tạo mới 1 tk ngân hàng theo loại
  // có 2 loại: nhận và chi
  async createBankAccount(accountNumber, bankName, branch, typeOfBank) {
     const config = await base.addHeaders();
    const response = await base.apiClient.post(Api.bank.url, {
      AccountNumber: accountNumber,
      BankName: bankName,
      Branch: branch,
      TypeOfBank: typeOfBank,
    },config);
    return response.data;
  }
};
