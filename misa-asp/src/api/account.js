
import { baseApi } from './baseApi.js';
import Api from '../api/apiConst';
import router from '../router';

export const account = {
  /**
   * Hàm đăng nhập 
   * home page
   * @param {*} emailOrPhoneNumber 
   * @param {*} password 
   * @returns 
   */
  async login(emailOrPhoneNumber, password, successCallback, errorCallback) {
  let params = {
    EmailOrPhoneNumber: emailOrPhoneNumber,
    Password: password,
  };
  const response = await baseApi.postApi(
    Api.login.url,
    params,
    (responseData) => {
      localStorage.setItem('role', responseData.role);
      localStorage.setItem('lastName', responseData.lastName);
      if (successCallback) successCallback(responseData);
    },
    (responseData) => {
      let errorMessage = 'Tài khoản hoặc mật khẩu sai. Vui lòng thử lại.';
      if (responseData && responseData.message) {
        errorMessage = responseData.message;
      }
      if (errorCallback) errorCallback(responseData);
    }
  );
  return response;
},


  /**
   * Hàm đăng ký tạo mới tài khoản
   * home page
   * @param {*} firstName 
   * @param {*} lastName 
   * @param {*} email 
   * @param {*} phoneNumber 
   * @param {*} password 
   * @param {*} roleId 
   * @returns 
   */
  async register(firstName, lastName, email, phoneNumber, password, roleId) {
    let params = {
      FirstName: firstName,
      LastName: lastName,
      Email: email,
      PhoneNumber: phoneNumber,
      Password: password,
      RoleId: roleId
    };
    const responseData = await baseApi.postApi(Api.register.url, params);
    return responseData;
  },
   
  /**
   * Hàm hiển thị tất cả user trong admin page
   * @returns 
   */
  async getAllUser() {
    return await baseApi.getAuthenApi(Api.getAllUser.url, null);
  },

  /**
   * Hàm tạo mới 1 employee trong payment page
   * @param {*} employeeCode 
   * @param {*} employeeName 
   * @param {*} department 
   * @param {*} phoneNumber 
   * @returns 
   */
  async createEmployee(employeeCode, employeeName, department, phoneNumber) {
    let params = {
      EmployeeCode: employeeCode,
      EmployeeName: employeeName,
      Department: department,
      PhoneNumber: phoneNumber,
    };
    const responseData = await baseApi.postAuthenApi(Api.createEmployee.url, params);
    return responseData;
  },

  /**
   * Hàm tạo mới 1 một user trong admin page
   * @param {*} firstName 
   * @param {*} lastName 
   * @param {*} email 
   * @param {*} phoneNumber 
   * @param {*} password 
   * @param {*} roleId 
   * @returns 
   */
  async createUser(firstName, lastName, email, phoneNumber, password, roleId) {
    let params = {
      FirstName: firstName,
      LastName: lastName,
      Email: email,
      PhoneNumber: phoneNumber,
      Password: password,
      RoleId: roleId
    };
    const responseData = await baseApi.postApi(Api.register.url, params);
    return responseData;
  },

  /**
   * Hàm tìm kiếm user theo id
   * @param {*} id 
   * @returns 
   */
  async getUserById(id) {
    try {
      const response = await baseApi.getAuthenApi(Api.getUserById.url,id);
      return response.data;
    } catch (error) {
      throw error.response ? error.response.data : error.message;
    }
  },

  /**
   * Hàm quên mật khẩu
   * @param {*} email 
   * @returns 
   */
  async forgotPassword(email) {
   
    try {
      let params = {
        Email: email,
      };
     const reponse = await baseApi.postApi(Api.password.url, params);
     return reponse.data;
    }catch (error) {  
    throw error; // Ném lỗi ra để các hàm gọi bên ngoài có thể xử lý
   }
  },

  /**
   * Hàm update user theo Id
   * @param {*} user 
   * @returns 
   */
  async updateUser(user) {
      try {      
        const getUser = await this.getUserById(user.id);
        if (getUser) {              
          const responseData = await baseApi.putAuthenApi(Api.updateUser.url,user);            
          return responseData;
        } else {
          throw new Error('Không tìm thấy người dùng này');
        }
      } catch (error) {

        throw error;
      }
    },
  /**
   * Hàm xóa user theo id
   * @param {*} id 
   * @returns 
   */
  async deleteUserById(id) { 
  try {
    const response = await baseApi.deleteAuthenApi(Api.deleteUserById.url, id);
    return response.data;
   }catch (error) {  
    throw error; // Ném lỗi ra để các hàm gọi bên ngoài có thể xử lý
   }
  },

  /**
   * Hàm đăng xuất thông tin người dùng
   * Hàm này gọi api xóa token trong cookie
   */
  async logout() {
    try {
    const response = await baseApi.postApi(Api.logout.url);
    return response.data;
  } catch (error) {  
    throw error; // Ném lỗi ra để các hàm gọi bên ngoài có thể xử lý
  }
  }
}
