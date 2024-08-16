import { baseApi } from './baseApi.js';
import Api from '../api/apiConst';

export const withdrawList = {

  /**
   * function call api để xóa 1 bản ghi theo id
   * @param {*} id 
   * @returns 
   */
async deletePaymentById(id) { 
  try {
    const response = await baseApi.deleteAuthenApi(Api.deletePaymentById.url, id);
    return response.data;
   }catch (error) {  
    throw error; 
   }
  },

/**
 * function hiển thị bản ghi theo id
 * @param {*} id 
 * @returns 
 */
  async getPaymentById(id) { 
  try {
    const url = Api.getWithdrawById.url.replace("{id}", id);
    const responseData = await baseApi.getAuthenApi(url);
    return responseData.data;
   }catch (error) {  
    throw error; 
   }
  }, 

  async getPagingFilter() {
    
  }

};
