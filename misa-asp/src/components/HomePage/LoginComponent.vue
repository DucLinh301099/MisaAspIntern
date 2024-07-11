<template>
  <div class="login-page">
    <div class="login-container">
      <div class="logo-section-login">
        <router-link to="/">
          <img
            src="https://asp.misa.vn/Content/Images/SVG/Logo.svg"
            alt="MISA ASP Logo"
            class="main-logo1"
          />
        </router-link>
      </div>
      <div class="main-title mt-3">
        <div class="row">
          <div class="col-6-1">
            <span class="bold">Đăng nhập</span>
          </div>
          <div class="col-6-2">
            <img
              src="https://asp.misa.vn/App/Content/images/Logo2.png"
              class="float-right"
            />
          </div>
        </div>
      </div>
      <form @submit.prevent="login">
        <div class="form-group">
          <input
            type="text"
            v-model="emailOrPhoneNumber"
            placeholder="Số điện thoại/Email"
            required
          />
        </div>
        <div class="form-group">
          <input
            type="password"
            v-model="password"
            placeholder="Mật khẩu"
            required
          />
        </div>
        <button type="submit" class="login-button">Đăng nhập</button>
      </form>
      <div class="extra-links">
        <p>
          Bạn chưa có tài khoản?
          <router-link to="/register">Đăng ký</router-link>
        </p>
        <p><router-link to="/forgot-password">Quên mật khẩu?</router-link></p>
      </div>
    </div>
  </div>
</template>

<script>
import { login } from "../../api/account";

export default {
  name: "LoginComponent",
  data() {
    return {
      emailOrPhoneNumber: "",
      password: "",
    };
  },
  methods: {
    async login() {
      try {
        console.log("Sending request to login user...");
        const data = await login(this.emailOrPhoneNumber, this.password);
        console.log("User logged in:", data);
        const role = localStorage.getItem("role");
        const lastName = localStorage.getItem("lastName");
        console.log("Role from localStorage:", role);
        console.log("Last Name from localStorage:", lastName);

        if (role === "Admin") {
          this.$router.push("/payment"); // this.$router.push("/admin"); // thay thể router admin ở đây khi đăng nhập.
        } else {
          this.$router.push("/userAccount");
        }
      } catch (error) {
        console.error("There was an error logging in the user:", error);
        let errorMessage;
        if (
          error.response &&
          error.response.data &&
          error.response.data.message
        ) {
          errorMessage = error.response.data.message;
        } else {
          errorMessage = "Tài khoản hoặc mật khẩu sai. Vui lòng thử lại.";
        }
        alert(errorMessage);
      }
    },
  },
};
</script>
<style>
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #0a2540;
}

.login-container {
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
.logo-section-login {
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin-bottom: 20px;
}

.main-logo1 {
  height: 50px;
}

h2 {
  margin-bottom: 20px;
  font-size: 24px;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
  display: flex;
  gap: 10px;
}

.form-group input {
  flex: 1;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.form-group input[type="text"],
.form-group input[type="password"] {
  width: 100%;
  margin: 0;
}

.login-button {
  width: 100%;
  padding: 10px;
  border: none;
  border-radius: 4px;
  background-color: #4caf50;
  color: white;
  cursor: pointer;
  font-size: 16px;
}

.extra-links {
  margin-top: 20px;
}

.extra-links p {
  margin: 5px 0;
}

a {
  color: #1e90ff;
  text-decoration: none;
}

.extra-links a:hover {
  text-decoration: underline;
}
</style>
