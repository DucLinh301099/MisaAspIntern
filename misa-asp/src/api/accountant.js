
import { base } from '../api/base.js';
import Api from '../api/apiConst.js'



export const accountant = {
  // function lấy thông tin tất cả các dịch vụ kế toán hiển thị trong home page
  async getServices() {
    try {
      const { url, method } = Api.accountant;
      const response = await base.apiClient[method](url);
      return response.data;
    } catch (error) {
      console.error('Failed to fetch services:', error);
      throw error;
    }
  },
// function tìm kiếm dịch vụ kế toán theo tên
  filterServices(services, searchQuery) {
    if (!services || !Array.isArray(services.data) || !services.data.length) {
      return [];
    }

    return services.data.filter(service => 
      service.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }
};
