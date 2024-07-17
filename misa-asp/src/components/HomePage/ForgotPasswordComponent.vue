<template>
  <div class="forgot-password-page">
    <div class="forgot-password-container">
      <MSAlert
        :message="alertMessage"
        :type="alertType"
        :visible="alertVisible"
        :isClose="alertIsClose"
        @close="alertVisible = false"
        @cancel="alertVisible = false"
      />
      <div class="logo-section">
        <router-link to="/">
          <img
            src="https://asp.misa.vn/Content/Images/SVG/Logo.svg"
            alt="MISA ASP Logo"
            class="main-logo"
          />
        </router-link>
      </div>
      <h2>Quên Mật Khẩu</h2>
      <form @submit.prevent="forgotPassword">
        <div class="form-group">
          <input type="email" v-model="email" placeholder="Email" required />
        </div>
        <button type="submit" class="forgot-password-button">
          Gửi Yêu Cầu
        </button>
        <div class="extra-links">
          <p>
            Quay lại đăng nhập
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
import MSAlert from "../BaseComponent/MSAlert.vue";

export default {
  name: "ForgotPasswordComponent",
  components: {
    MSAlert,
  },
  data() {
    return {
      email: "",
      alertMessage: "",
      alertType: "info",
      alertVisible: false,
      alertIsClose: false,
    };
  },
  methods: {
    async forgotPassword() {
      const response = await account.forgotPassword(this.email);
      if (response) {
        this.showAlert(
          "Yêu cầu làm mới mật khẩu đã được gửi đến Email của bạn",
          "success"
        );
      } else {
        this.showAlert(
          "Vui lòng nhập lại email chính xác hoặc thử lại sau",
          "error"
        );
      }
    },
    showAlert(message, type) {
      this.alertMessage = message;
      this.alertType = type;
      this.alertVisible = true;
      this.alertIsConfirm = false;
      this.alertIsClose = true;
    },
  },
};
</script>

<style scoped>
.logo-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.main-logo {
  height: 60px;
}
.forgot-password-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #0a2540;
}

.forgot-password-container {
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  width: 400px;
}

h2 {
  margin-bottom: 20px;
  font-size: 24px;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.forgot-password-button {
  width: 100%;
  padding: 10px;
  border: none;
  border-radius: 4px;
  background-color: #4caf50;
  color: white;
  cursor: pointer;
  font-size: 16px;
}
</style>
