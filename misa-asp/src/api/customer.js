import { base } from '../api/base.js';
import Api from '../api/apiConst.js';

export const customer = {
  /**
   * Hàm tạo mới 1 khách hàng
   * payment page
   * @param {*} objectId 
   * @param {*} objectName 
   * @param {*} taxCode 
   * @param {*} address 
   * @param {*} phoneNumber 
   * @returns 
   */
  async createCustomer(objectId, objectName, taxCode, address, phoneNumber) {
    let params = {
      ObjectId: objectId,
      ObjectName: objectName,
      TaxCode: taxCode,
      Address: address,
      PhoneNumber: phoneNumber
    };
    const reponse = await base.postApi(Api.customer.url, params);
    return reponse.data;
    
  }
};
