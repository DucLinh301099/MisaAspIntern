<template>
  <div class="admin-page">
    <div class="sidebar">
      <router-link to="/" class="logo">
        <img src="https://asp.misa.vn/Content/Images/SVG/Logo.svg" alt="Logo" />
      </router-link>
      <ul class="menu">
        <li>
          <router-link to="/dashboard"
            ><i class="fa fa-tachometer"></i> Dashboard</router-link
          >
        </li>
        <li>
          <router-link to="/user"><i class="fa fa-users"></i> User</router-link>
        </li>
        <li>
          <router-link to="/user-profile"
            ><i class="fa fa-user"></i> User Profile</router-link
          >
        </li>
        <li>
          <router-link to="/account"
            ><i class="fa fa-credit-card"></i> Account</router-link
          >
        </li>
        <li>
          <router-link to="/charts"
            ><i class="fa fa-bar-chart"></i> Charts</router-link
          >
        </li>
        <li>
          <router-link to="/forms"
            ><i class="fa fa-pencil"></i> Forms</router-link
          >
        </li>
        <li>
          <router-link to="/apps"><i class="fa fa-th"></i> Apps</router-link>
        </li>
        <li>
          <router-link to="/maps"><i class="fa fa-map"></i> Maps</router-link>
        </li>
        <li>
          <router-link to="/pages"
            ><i class="fa fa-file"></i> Pages</router-link
          >
        </li>
      </ul>
    </div>
    <div class="content">
      <div class="header">
        <h1>Tạo Mới User</h1>
      </div>
      <div class="create">
        <p class="links">
          <router-link to="/admin">Quay lại danh sách User</router-link>
        </p>
      </div>
      <form @submit.prevent="createUser" class="form-container">
        <h2 class="form-title">Tạo mới người dùng</h2>
        <div class="form-group-inline">
          <div class="form-group">
            <label class="label">Họ và đệm</label>
            <input type="text" v-model="firstName" required />
          </div>
          <div class="form-group">
            <label class="label"
              >Tên
              <span class="required">*</span>
            </label>
            <input type="text" v-model="lastName" required />
          </div>
        </div>
        <div class="form-group-inline">
          <div class="form-group">
            <label class="label"
              >Email
              <span class="required">*</span>
            </label>
            <input type="text" v-model="email" required />
            <div v-if="errors.Email" class="error">{{ errors.Email }}</div>
          </div>
          <div class="form-group">
            <label class="label"
              >Số điện thoại
              <span class="required">*</span>
            </label>
            <input type="text" v-model="phoneNumber" required />
            <div v-if="errors.PhoneNumber" class="error">
              {{ errors.PhoneNumber }}
            </div>
          </div>
        </div>
        <div class="form-group-inline">
          <div class="form-group">
            <label class="label"
              >Mật khẩu
              <span class="required">*</span>
            </label>
            <input type="text" v-model="password" required />
          </div>
          <div class="form-group">
            <label class="label"
              >Quyền người dùng
              <span class="required">*</span>
            </label>
            <select v-model="roleId" required>
              <option value="1">Admin</option>
              <option value="2">User</option>
            </select>
          </div>
        </div>

        <button type="submit" class="create-button">Tạo mới User</button>
        <div v-if="generalError" class="error">{{ generalError }}</div>
      </form>
    </div>
  </div>
</template>

<script>
import { account } from "../../api/account";

export default {
  name: "CreateUserComponent",
  data() {
    return {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: "",
      errors: {},
      generalError: "",
    };
  },
  methods: {
    async createUser() {
      try {
        await account.register(
          this.firstName,
          this.lastName,
          this.email,
          this.phoneNumber,
          this.password,
          this.roleId
        );
        alert("User created successfully!");
        this.$router.push("/admin");
      } catch (error) {
        this.errors = {};
        this.generalError = "";

        // Extract error messages
        const errorMsgs = error.message.split(", ");

        // Handle specific validation errors
        this.errors.Email = errorMsgs.find((msg) =>
          msg.includes("Email error")
        );
        this.errors.PhoneNumber = errorMsgs.find((msg) =>
          msg.includes("Phone number error")
        );

        // Handle general error if no specific errors are found
        if (!this.errors.Email && !this.errors.PhoneNumber) {
          this.generalError = error.message;
        }
      }
    },
  },
};
</script>

<style scoped>
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css");

.admin-page {
  display: flex;
  height: 100vh;
  background-color: #f0f2f5;
  font-family: Arial, sans-serif;
}
.required {
  color: red;
}
h2 {
  font-weight: bold;
  padding-bottom: 30px;
  font-size: 28px;
}
.sidebar {
  width: 200px;
  background-color: #ffffff;
  padding: 20px 0;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.sidebar .logo {
  display: flex;
  justify-content: center;
  align-items: center;
  padding-bottom: 18px;
}

.sidebar .logo img {
  width: 150px;
  height: auto;
  margin-top: 10px;
}

.sidebar .menu {
  list-style-type: none;
  padding: 0;
  margin: 0;
  flex-grow: 1;
}

.sidebar .menu li {
  margin: 0;
}

.sidebar .menu li a {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: #333;
  font-size: 16px;
  padding: 18px 20px;
  border-left: 3px solid transparent;
  transition: background-color 0.3s ease, border-color 0.3s ease;
}

.sidebar .menu li a i {
  margin-right: 10px;
}

.sidebar .menu li a:hover,
.sidebar .menu li a.router-link-active {
  background-color: #f0f0f0;
  border-color: #007bff;
}

.content {
  flex: 1;
  padding: 20px;
  overflow-y: auto;
  margin-left: 15px;
  margin-right: 40px;
}

.header {
  background-color: #ffffff;
  padding: 15px 20px;
  color: #333;
  border-radius: 3px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.header h1 {
  margin: 0;
  font-size: 24px;
  color: #333;
}

.form-container {
  background-color: #fff;
  padding: 40px 20px 40px 20px;
  border-radius: 3px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: auto;
  padding: 15px 50px 50px 50px;
}

.form-group-inline {
  display: flex;
  gap: 20px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ddd;
  border-radius: 2.5px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
}
a {
  color: white;
}
.form-group input:focus,
.form-group select:focus {
  border-color: rgb(10, 146, 10);
  outline: none;
}

.form-group select {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ddd;
  border-radius: 2.5px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
  background: white;
  background-image: url("data:image/svg+xml;charset=US-ASCII,%3csvg%20xmlns%3d%22http%3a//www.w3.org/2000/svg%22%20viewBox%3d%220%200%204%205%22%3e%3cpath%20fill%3d%22%23333%22%20d%3d%22M2%205L0%202h4L2%205z%22/%3e%3c/svg%3e");
  background-repeat: no-repeat;
  background-position: right 10px center;
  background-size: 10px;
}
.create-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 2.5px;
  cursor: pointer;
  font-weight: bold;
  cursor: pointer;
  font-size: 16px;
  display: flex;
  align-items: center;
}

.create-button i {
  margin-right: 8px;
}

.create-button:hover {
  background-color: #0069d9;
}

.disclaimer {
  font-size: 14px;
  color: #555;
}

.error {
  color: red;
  font-size: 14px;
  margin-top: 5px;
}

.create {
  margin-top: 20px;
  text-align: left; /* Align left */
}

.create .links {
  display: inline-block;
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  cursor: pointer;
  font-weight: bold;
  border-radius: 2.5px;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.create .links:hover {
  background-color: #218838;
}
</style>
