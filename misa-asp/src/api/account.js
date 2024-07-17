
import { base } from '../api/base.js';
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
  async login(emailOrPhoneNumber, password) {
    let params = {
      EmailOrPhoneNumber: emailOrPhoneNumber,
      Password: password,
    };
    const response = await base.postApi(
      Api.login.url,
      params,
      (responseData) => {
        localStorage.setItem('role', responseData.role);
        localStorage.setItem('lastName', responseData.lastName);

        if (responseData.role === 'Admin') {
          router.push('/payment');
        } else {
          router.push('/userAccount');
        }
      },
      (responseData) => {
        let errorMessage = 'Tài khoản hoặc mật khẩu sai. Vui lòng thử lại.';
        if (responseData && responseData.message) {
          errorMessage = responseData.message;
        }
        alert(errorMessage);
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
    const reponse = await base.postApi(Api.register.url, params);
    return reponse.data;
  },
   
  /**
   * Hàm hiển thị tất cả user trong admin page
   * @returns 
   */
  async getAllUser() {
    return await base.getAuthenApi(Api.getAllUser.url, null);
  },

  /**
   * Hàm tạo mới 1 employee trong payment page
   * @param {*} employeeCode 
   * @param {*} employeeName 
   * @param {*} department 
   * @param {*} mobilePhone 
   * @returns 
   */
  async createEmployee(employeeCode, employeeName, department, mobilePhone) {
    let params = {
      EmployeeCode: employeeCode,
      EmployeeName: employeeName,
      Department: department,
      MobilePhone: mobilePhone,
    };
    const reponse = await base.postApi(Api.createEmployee.url, params);
    return reponse.data;
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
    const reponse = await base.postApi(Api.register.url, params);
    return reponse.data;
  },

  /**
   * Hàm tìm kiếm user theo id
   * @param {*} id 
   * @returns 
   */
  async getUserById(id) {
    try {
      const response = await base.getAuthenApi(Api.getUserById.url,id);
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
     const reponse = await base.postApi(Api.password.url, params);
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
          const response = await base.putAuthenApi(Api.updateUser.url,user);            
          return response.data;
        } else {
          throw new Error('User not found');
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
    const response = await base.deleteAuthenApi(Api.deleteUserById.url, id);
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
    const response = await base.postApi(Api.logout.url);
    return response.data;
  } catch (error) {  
    throw error; // Ném lỗi ra để các hàm gọi bên ngoài có thể xử lý
  }
  }
}
