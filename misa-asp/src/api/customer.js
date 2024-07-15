import { base } from '../api/base.js';
import Api from '../api/apiConst.js';

export const customer = {
  async createCustomer(objectId, objectName, taxCode, address, phoneNumber) {
    const config = await base.addHeaders();
    const response = await base.apiClient.post(Api.customer.url, {
      ObjectId: objectId,
      ObjectName: objectName,
      TaxCode: taxCode,
      Address: address,
      PhoneNumber: phoneNumber,
    },config);
    return response.data;
  }
};
