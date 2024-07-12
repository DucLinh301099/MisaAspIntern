import { apiClient } from "./base";

export const createBankAccount = async (accountNumber, bankName, branch, typeOfBank) => {
  console.log('Attempting to create bank account...');
  const response = await apiClient.post('BankAccount/create-bank-account', {
    AccountNumber: accountNumber,
    BankName: bankName,
    Branch: branch,
    TypeOfBank: typeOfBank,
   
  });
  console.log('User create bank account successfully:', response.data);
  return response.data;
};