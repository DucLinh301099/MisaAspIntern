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
  
    

};
