import { base } from '../api/base.js';

export const customer = {
  async createCustomer(objectId, objectName, taxCode, address, phoneNumber) {
    const response = await base.apiClient.post('Customer/createCustomer', {
      ObjectId: objectId,
      ObjectName: objectName,
      TaxCode: taxCode,
      Address: address,
      PhoneNumber: phoneNumber,
    });
    return response.data;
  }
};
