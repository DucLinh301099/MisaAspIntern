import { apiClient } from "./base";

export const createBankAccount = async (accountNumber, bankName, branch, roleId) => {
  console.log('Attempting to create bank account...');
  const response = await apiClient.post('BankAccount/CreateBankAccount', {
    AccountNumber: accountNumber,
    BankName: bankName,
    Branch: branch,
    RoleId: roleId,
   
  });
  console.log('User create bank account successfully:', response.data);
  return response.data;
};