// src/services/account.js

import { base } from '../api/base.js';
import Api from '../api/apiConst';

export const account = {
  async login(emailOrPhoneNumber, password) {
    try {
      const response = await base.apiClient.post(Api.login.url, {
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
      const response = await base.apiClient.get(Api.getAllUser.url, base.addHeaders());
      console.log('Protected data fetched successfully:', response.data);
      return response.data.data;
    } catch (error) {
      console.error('Error fetching protected data:', error.response ? error.response.data : error.message);
      throw error.response ? error.response.data : error.message;
    }
  },

  async register(firstName, lastName, email, phoneNumber, password, roleId) {
    const response = await base.apiClient.post(Api.register.url, {
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
    const response = await base.apiClient.post(Api.createEmployee.url, {
      EmployeeCode: employeeCode,
      EmployeeName: employeeName,
      Department: department,
      MobilePhone: mobilePhone,
    });
    return response.data;
  },

  async createUser(firstName, lastName, email, phoneNumber, password, roleId) {
    const response = await base.apiClient.post(Api.register.url, {
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
      const response = await base.apiClient.get(`${Api.getUserById.url}${id}`, base.addHeaders());
      return response.data;
    } catch (error) {
      throw error.response ? error.response.data : error.message;
    }
  },

  async forgotPassword(email) {
    const response = await base.apiClient.post('Account/forgot-password', { Email: email });
    return response.data;
  },

  async updateUser(user) {
    const response = await base.apiClient.put(`${Api.updateUser.url}${user.id}`, user, base.addHeaders());
    return response.data;
  },

  async deleteUserById(id) {
    const response = await base.apiClient.delete(`${Api.deleteUserById.url}${id}`, base.addHeaders());
    return response.data;
  },

  async logout() {
    try {
      await base.apiClient.post('/Account/logout');
      localStorage.removeItem('role');
      localStorage.removeItem('lastName');
    } catch (error) {
      throw error.response ? error.response.data : error.message;
    }
  }
}
