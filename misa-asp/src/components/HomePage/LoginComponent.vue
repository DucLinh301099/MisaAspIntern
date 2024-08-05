<template>
  <div>
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
        <div class="main-title">
          <div>
            <span class="bold">Đăng nhập</span>
          </div>
          <div>
            <img
              src="https://asp.misa.vn/App/Content/images/Logo2.png"
              class="float-right"
            />
          </div>
        </div>
        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <MSInput
              type="text"
              class="login-input"
              :value="emailOrPhoneNumber"
              ref="EmailOrPhoneNumber"
              @input="updateValue('emailOrPhoneNumber', $event.target.value)"
              placeholder="Số điện thoại/Email"
            />
          </div>
          <div class="form-group">
            <MSInput
              ref="Password"
              type="password"
              :value="password"
              class="login-input"
              @input="updateValue('password', $event.target.value)"
              placeholder="Mật khẩu"
            />
          </div>
          <button class="login-button" type="submit">Đăng nhập</button>
        </form>
        <div class="extra-links">
          <p>
            Bạn chưa có tài khoản?
            <router-link to="/register">Đăng ký</router-link>
          </p>
          <p>
            <router-link to="/forgot-password">Quên mật khẩu?</router-link>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { account } from "../../api/account";
import BaseForm from "../Base/BaseForm.vue";

export default {
  extends: BaseForm,
  name: "LoginComponent",

  data() {
    return {
      emailOrPhoneNumber: "",
      password: "",
    };
  },

  methods: {
    /**
     * Các function này được kế thừa từ BaseHandleSubmit
     */
    async customHandleLogic() {
      return await account.login(this.emailOrPhoneNumber, this.password);
    },

    afterCallSuccess(responseData) {
      this.showAlert("Đăng nhập thành công!", () => {
        if (responseData.data.role === "Admin") {
          this.$router.push("/admin");
        } else {
          this.$router.push("/userAccount");
        }
      });
    },
    afterCallErrorCustom(responseData) {
      this.showAlert(
        "Đăng nhập thất bại - Thông tin đăng nhập không hợp lệ",
        () => {
          if (!responseData.isSuccess) {
            this.$router.push("/login");
          }
        }
      );
    },

    updateValue(field, value) {
      this[field] = value;
      this.$refs[field].errors = null; // Xóa lỗi khi nhập lại dữ liệu
    },
  },
};
</script>
<style scoped>
.main-title {
  display: flex;
  margin-bottom: 15px;
  justify-content: space-between;
}
.bold {
  font-weight: 700;
  font-size: 20px !important;
  font-family: AvertaStdCY_Semibold, Helvetica, Arial, sans-serif;
}
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #0a2540;
}

.login-container {
  background-color: #fff;
  padding: 40px;
  border-radius: 4px;
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
  margin-bottom: 25px;
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

.login-input {
  flex: 1;
  padding: 8.5px;
  border: 1px solid #ccc;
  border-radius: 3px;
  box-sizing: border-box;
  width: 100%;
  display: flex;
  align-items: center;
  outline: none;
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
  font-family: AvertaStdCY_Semibold, Helvetica, Arial, sans-serif;
}

.extra-links p {
  margin: 5px 0;
}

a {
  color: #1e90ff;
  text-decoration: none;
}
button:hover {
  background-color: #258c28;
}
.extra-links a:hover {
  text-decoration: underline;
}
</style>
