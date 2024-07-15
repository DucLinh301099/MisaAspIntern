// src/services/account.js

import { base } from '../api/base.js';
import Api from '../api/apiConst';

export const account = {
  async login(emailOrPhoneNumber, password) {
    let params = {
      EmailOrPhoneNumber: emailOrPhoneNumber,
      Password: password,
    };

    // Sử dụng arrow function để đảm bảo `this` trỏ đúng đến instance Vue hiện tại
    const response = await base.postApi(
      Api.login.url,
      params,
      (responseData) => {
        localStorage.setItem('role', responseData.role);
        localStorage.setItem('lastName', responseData.lastName);

        if (responseData.role === 'Admin') {
          this.$router.push('/payment'); // Replace with actual admin route if necessary.
        } else {
          this.$router.push('/userAccount');
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


  async getAllUser() {
    return response = await base.getAuthenApi(Api.getAllUser)
  },

  async register(firstName, lastName, email, phoneNumber, password, roleId) {
    let params = {
      FirstName: firstName,
      LastName: lastName,
      Email: email,
      PhoneNumber: phoneNumber,
      Password: password,
      RoleId: roleId
    };
    return await base.postApi(Api.register, params)
  },

  async createEmployee(employeeCode, employeeName, department, mobilePhone) {
    let params = {
      EmployeeCode: employeeCode,
      EmployeeName: employeeName,
      Department: department,
      MobilePhone: mobilePhone,
    };
    return await base.postApi(Api.createEmployee, params)
  },

  async createUser(firstName, lastName, email, phoneNumber, password, roleId) {
    const { url, method } = Api.register;
    const response = await base.apiClient[method](url, {
      FirstName: firstName,
      LastName: lastName,
      Email: email,
      PhoneNumber: phoneNumber,
      Password: password,
      RoleId: roleId
    });
    return response.data;
  },

  async getUserById(id) {
    try {
      const { url, method } = Api.getUserById;
      const response = await base.apiClient[method](`${url}${id}`, base.addHeaders());
      return response.data;
    } catch (error) {
      throw error.response ? error.response.data : error.message;
    }
  },

  async forgotPassword(email) {
    const { url, method } = Api.password;
    const response = await base.apiClient[method](url, { Email: email });
    return response.data;
  },

  async updateUser(user) {
    const { url, method } = Api.updateUser;
    const response = await base.apiClient[method](`${url}${user.id}`, user, base.addHeaders());
    return response.data;
  },

  async deleteUserById(id) {
    const { url, method } = Api.deleteUserById;
    const response = await base.apiClient[method](`${url}${id}`, base.addHeaders());
    return response.data;
  },

  async logout() {
    try {
      const { url, method } = Api.logout;
      await base.apiClient[method](url);
      localStorage.removeItem('role');
      localStorage.removeItem('lastName');
    } catch (error) {
      throw error.response ? error.response.data : error.message;
    }
  }
}
