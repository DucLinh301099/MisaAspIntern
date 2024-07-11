<template>
  <div class="register-page">
    <div class="register-container">
      <div class="logo-section-register">
        <router-link to="/">
          <img
            src="https://asp.misa.vn/Content/Images/SVG/Logo.svg"
            alt="MISA ASP Logo"
            class="main-logo2"
          />
        </router-link>
      </div>
      <div class="main-title mt-3">
        <div class="row">
          <div class="col-6-1">
            <span class="bold">Đăng Ký</span>
          </div>
          <div class="col-6-2">
            <img
              src="https://asp.misa.vn/App/Content/images/Logo2.png"
              class="float-right"
            />
          </div>
        </div>
      </div>
      <form @submit.prevent="register">
        <div class="form-group-inline">
          <div class="form-group">
            <input
              type="text"
              v-model="firstName"
              placeholder="Họ và đệm"
              required
            />
          </div>
          <div class="form-group">
            <input type="text" v-model="lastName" placeholder="Tên" required />
          </div>
        </div>
        <div class="form-group">
          <input type="text" v-model="email" placeholder="Email" required />
          <div v-if="errors.Email" class="error">{{ errors.Email }}</div>
        </div>
        <div class="form-group">
          <input
            type="text"
            v-model="phoneNumber"
            placeholder="Số điện thoại"
            required
          />
          <div v-if="errors.PhoneNumber" class="error">
            {{ errors.PhoneNumber }}
          </div>
        </div>
        <div class="form-group">
          <input
            type="password"
            v-model="password"
            placeholder="Mật khẩu"
            required
          />
        </div>
        <div class="form-group">
          <select v-model="roleId" required>
            <option value="" disabled>Chọn quyền</option>
            <option value="1">Admin</option>
            <option value="2">User</option>
          </select>
        </div>
        <p class="disclaimer">
          Bằng cách bấm vào nút Nhận tư vấn miễn phí, bạn đã đồng ý với
          <a href="https://www.misa.vn/privacy/">Chính sách quyền riêng tư</a>
          của MISA.
        </p>
        <button type="submit" class="register-button">Đăng ký</button>
        <div v-if="generalError" class="error">{{ generalError }}</div>
        <div class="extra-links">
          <p>
            Bạn đã có tài khoản?
            <router-link to="/login">Đăng Nhập</router-link>
          </p>
          <p><router-link to="/href">Trợ giúp</router-link></p>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { register } from "../../api/account";

export default {
  name: "RegisterComponent",
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
    async register() {
      try {
        console.log("Sending request to register user...");
        const data = await register(
          this.firstName,
          this.lastName,
          this.email,
          this.phoneNumber,
          this.password,
          this.roleId
        );
        console.log("User registered:", data);
        alert("Registration successful!");
        this.$router.push("/login");
      } catch (error) {
        console.error("There was an error registering the user:", error);
        this.errors = {};
        this.generalError = "";

        // Handle specific validation errors
        if (error.message.includes("Email error")) {
          this.errors.Email = error.message
            .split(", ")
            .find((msg) => msg.includes("Email error"));
        }
        if (error.message.includes("Phone number error")) {
          this.errors.PhoneNumber = error.message
            .split(", ")
            .find((msg) => msg.includes("Phone number error"));
        }

        // Handle general error
        if (!this.errors.Email && !this.errors.PhoneNumber) {
          this.generalError = error.message;
        }
      }
    },
  },
};
</script>
<style>
.register-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #0a2540;
}

.register-container {
  background-color: #fff;
  padding: 20px 40px 40px 40px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  width: 400px;
}
.error-message {
  display: none;
  color: #f45d48;
  bottom: 8px;
  font-size: 12px;
}
.logo-section-register {
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin-bottom: 20px;
}

.main-logo1 {
  height: 50px;
}
.register-button {
  width: 100%;
  padding: 10px;
  border: none;
  border-radius: 4px;
  background-color: #4caf50;
  color: white;
  cursor: pointer;
  font-size: 16px;
}
.bold {
  font-weight: 550;
}
.main-title {
  font-family: AvertaStdCY_Semibold, Helvetica, Arial, sans-serif;
  font-size: 20px !important;
}
.mt-3,
.my-3 {
  margin-top: 1rem !important;
}
.row {
  display: -ms-flexbox;
  display: flex;
  -ms-flex-wrap: wrap;
  flex-wrap: wrap;
  padding-bottom: 15px;
}
.col-6-1 {
  -ms-flex: 0 0 50%;
  flex: 0 0 50%;
  max-width: 50%;
  text-align: left;
}
.col-6-2 {
  -ms-flex: 0 0 50%;
  flex: 0 0 50%;
  max-width: 50%;
}
.float-right {
  float: right !important;
}
img {
  vertical-align: middle;
  border-style: none;
}

.form-group {
  margin-bottom: 15px;
  position: relative;
}
.form-group-inline {
  display: flex;
  justify-content: space-between;
  padding-bottom: 20px;
}
.form-group-inline .form-group {
  flex: 0 0 48%;
  margin-bottom: 0;
}
.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  width: 100%; /* Make the label take full width */
}

.disclaimer {
  font-size: 12px;
  color: #666;
  margin-bottom: 20px;
}
.disclaimer a {
  color: #00a65a;
}

input {
  line-height: normal;
}
.submit-btn {
  display: inline-block;
  padding: 10px 20px;
  background-color: #00a65a;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

button,
select {
  text-transform: none;
}
button {
  overflow: visible;
}
.submit-btn:hover {
  background-color: #008c4a;
}
</style>
