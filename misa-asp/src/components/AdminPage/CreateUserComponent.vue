<template>
  <div class="admin-page">
    <SideBarComponent />
    <div class="content">
      <div class="header">
        <MSAlert
          :message="alertMessage"
          :type="alertType"
          :visible="alertVisible"
          :isConfirm="alertIsConfirm"
          :isShow="alertIsShow"
          @confirm="handleConfirm"
        />
        <div class="create">
          <p class="links">
            <router-link class="text" to="/admin"
              >Quay lại danh sách User</router-link
            >
          </p>
        </div>
      </div>

      <form @submit.prevent="createUser" class="form-container">
        <div class="form-input">
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
            </div>
            <div class="form-group">
              <label class="label"
                >Số điện thoại
                <span class="required">*</span>
              </label>
              <input type="text" v-model="phoneNumber" required />
            </div>
          </div>
          <div class="form-group-inline">
            <div class="form-group">
              <label class="label"
                >Mật khẩu
                <span class="required">*</span>
              </label>
              <input type="password" v-model="password" required />
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
          <div class="form-group-button">
            <button type="submit" class="create-button">Tạo mới</button>
            <button type="button" @click="cancelCreate" class="cancel-button">
              Hủy
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { account } from "../../api/account";
import SideBarComponent from "../AdminPage/SideBarComponent.vue";
import MSAlert from "../BaseComponent/MSAlert.vue";

export default {
  name: "CreateUserComponent",
  components: {
    SideBarComponent,
    MSAlert,
  },
  data() {
    return {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: "",

      alertMessage: "",
      alertType: "info",
      alertVisible: false,
      alertIsConfirm: false,
      confirmAction: null,
      alertIsShow: true,
    };
  },
  methods: {
    async createUser() {
      const response = await account.register(
        this.firstName,
        this.lastName,
        this.email,
        this.phoneNumber,
        this.password,
        this.roleId
      );
      if (response) {
        this.showConfirm("Người dùng tạo mới thành công!", () =>
          this.$router.push("/admin")
        );
      } else {
        this.showConfirm(
          "Lỗi khi cập nhật người dùng: " + response.message,
          "error"
        );
      }
    },
    cancelCreate() {
      this.$router.push("/admin");
    },
    showConfirm(message, action) {
      this.alertMessage = message;
      this.confirmAction = action;
      this.alertVisible = true;
      this.alertIsConfirm = true;
      this.alertIsShow = false;
    },
    handleConfirm() {
      if (this.confirmAction) {
        this.confirmAction();
      }
      this.alertVisible = false;
    },
  },
};
</script>

<style>
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
.form-input {
  background-color: #ffffff;
  padding: 40px;
  border-radius: 3px;
  max-width: 600px;
  margin: 0px auto;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
h2 {
  font-weight: bold;
  padding-bottom: 20px;
  font-size: 30px;
  color: #333;
}
.form-group-button {
  display: flex;
  gap: 10px;
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
  background-color: #f9f9f9;
  padding: 40px 20px 40px 20px;
  border-radius: 3px;
  width: auto;
  margin: 0 auto;
}

.form-group-inline {
  display: flex;
  gap: 20px;
  margin-bottom: 20px;
}

.form-group {
  flex: 1;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}

.form-group input,
.form-group select {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ddd;
  border-radius: 2.5px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
}

.form-group input:focus,
.form-group select:focus {
  border-color: #1fa153;
  outline: none;
}

.form-group select {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  background: white;
  background-image: url("data:image/svg+xml;charset=US-ASCII,%3csvg%20xmlns%3d%22http%3a//www.w3.org/2000/svg%22%20viewBox%3d%220%200%204%205%22%3e%3cpath%20fill%3d%22%23333%22%20d%3d%22M2%205L0%202h4L2%205z%22/%3e%3c/svg%3e");
  background-repeat: no-repeat;
  background-position: right 10px center;
  background-size: 10px;
}
.create-button,
.cancel-button {
  display: inline-block;
  padding: 10px 20px;
  font-size: 16px;
  font-weight: bold;
  text-align: center;
  vertical-align: middle;
  cursor: pointer;
  border: none;
  border-radius: 3px;
  width: 120px;
}
.create-button {
  background-color: #1fa153;
  color: white;
}
.cancel-button {
  background-color: #f44336;
  color: white;
}
.text {
  color: #fff;
}
.create-button i {
  margin-right: 8px;
}

.create-button:hover {
  background-color: #14743a;
}
.cancel-button:hover {
  background-color: #913721;
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
  text-align: left;
}

.create .links {
  display: inline-block;
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  cursor: pointer;
  font-weight: bold;
  border-radius: 3px;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.create .links:hover {
  background-color: #218838;
}
</style>
