
import { base } from '../api/base.js';
import Api from '../api/apiConst.js'

export const accountant = {
  /**
   * Hàm hiện thị tất cả các dịch vụ kế toán trong home page
   * @returns 
   */
  async getServices() {     
      return await base.getApi(Api.accountant.url, null);
      
    
  },
/**
 * Hàm tìm kiếm dịch vụ kế toán theo tên
 * @param {*} services 
 * @param {*} searchQuery 
 * @returns 
 */
  filterServices(services, searchQuery) {
    if (!services || !Array.isArray(services.data) || !services.data.length) {
      return [];
    }

    return services.data.filter(service => 
      service.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }
};
