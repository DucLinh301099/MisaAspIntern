// src/api/bankAccount.js
import { base } from '../api/base.js';

export const bankAccount = {
  // function tạo mới 1 tk ngân hàng theo loại
  // có 2 loại: nhận và chi
  async createBankAccount(accountNumber, bankName, branch, typeOfBank) {
    const response = await base.apiClient.post('BankAccount/create-bank-account', {
      AccountNumber: accountNumber,
      BankName: bankName,
      Branch: branch,
      TypeOfBank: typeOfBank,
    });
    return response.data;
  }
};
