import { baseApi } from './baseApi.js';
import Api from '../api/apiConst';

export const withdrawList = {

async deletePaymentById(id) { 
  try {
    const response = await baseApi.deleteAuthenApi(Api.deletePaymentById.url, id);
    return response.data;
   }catch (error) {  
    throw error; 
   }
  },

  async updatePayment() { 
  try {
    const response = await baseApi.putAuthenApi(Api.updatePayment.url, id);
    return response.data;
   }catch (error) {  
    throw error; 
   }
  },
  
  async getPaymentById(id) { 
  try {
    const url = Api.getWithdrawById.url.replace("{id}", id);
    const responseData = await baseApi.getAuthenApi(url);
    return responseData.data;
   }catch (error) {  
    throw error; 
   }
  }, 

};
