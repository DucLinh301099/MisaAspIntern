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
      <div class="main-title "> 
          <div >
            <span class="bold">Đăng Ký</span>
          </div>
          <div >
            <img
              src="https://asp.misa.vn/App/Content/images/Logo2.png"
              class="float-right"
            />
          </div>      
      </div>
      <form @submit.prevent="register">
        <div class="form-group-inline">
          <div class="form-group-r">
            <input
              type="text"
              v-model="firstName"
              placeholder="Họ và đệm"
              required
            />
          </div>
          <div class="form-group-r">
            <input type="text" v-model="lastName" placeholder="Tên" required />
          </div>
        </div>
        <div class="form-group-r">
          <input type="text" v-model="email" placeholder="Email" required />
        </div>
        <div class="form-group-r">
          <input
            type="text"
            v-model="phoneNumber"
            placeholder="Số điện thoại"
            required
          />
        </div>
        <div class="form-group-r">
          <input
            type="password"
            v-model="password"
            placeholder="Mật khẩu"
            required
          />
        </div>
        <div class="form-group-r">
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
import { account } from "../../api/account";

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
    };
  },
  methods: {
    async register() {
      try {
        await account.register(
          this.firstName,
          this.lastName,
          this.email,
          this.phoneNumber,
          this.password,
          this.roleId
        );
        alert("Người dùng đã được đăng ký!");
        this.$router.push("/login");
      } catch (error) {
        alert("Người dùng tạo mới thất bại!", "error");
      }
    },
  },
};
</script>
<style scoped>
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
  font-weight: bold;
  font-size: 20px;
}
.main-title {
  font-family: AvertaStdCY_Semibold, Helvetica, Arial, sans-serif;
  justify-content: space-between;
  display:flex;
  margin-bottom: 10px;
}



.float-right {
  float: right !important;
}
img {
  vertical-align: middle;
  border-style: none;
}

.form-group-r {
  margin-bottom: 20px;
  display: flex;
  gap: 10px;
}
.form-group-r input,
.form-group-r select{
  flex: 1;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 3px;
  outline: none;
}
.form-group-inline {  
  display: flex;
  justify-content: space-between;
  padding-bottom: 5px;
}
.form-group-inline {
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
