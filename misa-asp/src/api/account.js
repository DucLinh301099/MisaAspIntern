import { apiClient } from './base';


const getTokenFromCookie = () => {
  const cookies = document.cookie.split(';');
  for (let cookie of cookies) {
    const [name, value] = cookie.trim().split('=');
    if (name === 'AuthToken') {
      return value;
    }
  }
  return null;
};
// Call API to login
export const login = async (emailOrPhoneNumber, password) => {
  try {
    console.log('Attempting to log in user...');
    const response = await apiClient.post('/Account/login', {
      EmailOrPhoneNumber: emailOrPhoneNumber,
      Password: password
    });

    if (response.status === 200) {
      const { role, lastName } = response.data.data;

      // Lưu role và lastName vào localStorage
      localStorage.setItem('role', role);
      localStorage.setItem('lastName', lastName);

      console.log('User logged in successfully.');
      console.log('Role received:', role);
      console.log('Last Name received:', lastName);

      return response.data;
    } else {
      console.error('Login failed:', response.data);
      throw new Error('Login failed');
    }
  } catch (error) {
    console.error('Error logging in:', error);
    throw error;
  }
};

export const fetchProtectedData = async () => {
  try {
    console.log('Fetching protected data...');
    const token = getTokenFromCookie();
    if (!token) {
      throw new Error('No token found');
    }
    console.log('Token:', token); // Log token để kiểm tra
    const response = await apiClient.get('/Account/users', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    console.log('Protected data fetched successfully:', response.data);
    return response.data.data; // Đảm bảo trả về đúng data
  } catch (error) {
    console.error('Error fetching protected data:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : error.message;
  }
};


export const register = async (firstName, lastName, email, phoneNumber, password, roleId) => {
  console.log('Attempting to register user...');
  const response = await apiClient.post('Account/createEmployee', {
    FirstName: firstName,
    LastName: lastName,
    Email: email,
    PhoneNumber: phoneNumber,
    Password: password,
    RoleId: roleId // Add RoleId to the request
  });
  console.log('User registered successfully:', response.data);
  return response.data;
};
export const createEmployee = async (employeeCode, employeeName, department, mobilePhone) => {
  console.log('Attempting to create employee...');
  const response = await apiClient.post('Account/createEmployee', {
    EmployeeCode: employeeCode,
    EmployeeName: employeeName,
    Department: department,
    MobilePhone: mobilePhone,
   
  });
  console.log('User create employee successfully:', response.data);
  return response.data;
};


export const createUser = async (firstName, lastName, email, phoneNumber, password, roleId) => {
  console.log('Attempting to create user...');
  const response = await apiClient.post('Account/register', {
    FirstName: firstName,
    LastName: lastName,
    Email: email,
    PhoneNumber: phoneNumber,
    Password: password,
    RoleId: roleId // Add RoleId to the request
  });
  console.log('User created successfully:', response.data);
  return response.data;
};

export const fetchUserById = async (id) => {
  try {
    const token = getTokenFromCookie();
    const response = await apiClient.get(`Account/users/${id}`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data; // Ensure that response.data contains the user object
  } catch (error) {
    console.error('Error fetching user by id:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : error.message;
  }
};

export const forgotPassword = async (email) => {
  console.log('Attempting to send forgot password request...');
  const response = await apiClient.post('Account/forgot-password', { Email: email });
  console.log('Forgot password request successful:', response.data);
  return response.data;
};

export const updateUser = async (user) => {
  console.log('Attempting to update user...');
  const token = getTokenFromCookie();
  const response = await apiClient.put(`Account/users/${user.id}`, user, {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
  });
  console.log('User updated successfully:', response.data);
  return response.data;
};

export const deleteUserById = async (id) => {
  const token = getTokenFromCookie();
  const response = await apiClient.delete(`Account/users/${id}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

export const logout = async () => {
  try {
    await apiClient.post('/Account/logout');
    // Xóa các mục trong localStorage nhưng không xóa token trong cookie
    localStorage.removeItem('role');
    localStorage.removeItem('lastName');
  } catch (error) {
    console.error('Error logging out:', error.response ? error.response.data : error.message);
    throw error.response ? error.response.data : error.message;
  }
};