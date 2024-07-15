// src/services/account.js

import { base } from '../api/base.js';
import Api from '../api/apiConst';

export const account = {
  async login(emailOrPhoneNumber, password) {
    try {
      const { url, method } = Api.login;
      const response = await base.apiClient[method](url, {
        EmailOrPhoneNumber: emailOrPhoneNumber,
        Password: password
      });

      if (response.status === 200) {
        const { role, lastName } = response.data.data;
        localStorage.setItem('role', role);
        localStorage.setItem('lastName', lastName);
        return response.data;
      } else {
        throw new Error('Login failed');
      }
    } catch (error) {
      throw error;
    }
  },

  async getAllUser() {
    try {
      const { url, method } = Api.getAllUser;
      const response = await base.apiClient[method](url, base.addHeaders());
      console.log('Protected data fetched successfully:', response.data);
      return response.data.data;
    } catch (error) {
      console.error('Error fetching protected data:', error.response ? error.response.data : error.message);
      throw error.response ? error.response.data : error.message;
    }
  },

  async register(firstName, lastName, email, phoneNumber, password, roleId) {
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

  async createEmployee(employeeCode, employeeName, department, mobilePhone) {
    const { url, method } = Api.createEmployee;
    const config = await base.addHeaders();
    const response = await base.apiClient[method](url, {
      EmployeeCode: employeeCode,
      EmployeeName: employeeName,
      Department: department,
      MobilePhone: mobilePhone,
    }, config);
    return response.data;
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
