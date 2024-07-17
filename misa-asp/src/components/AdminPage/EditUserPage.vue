<template>
  <div class="admin-page">
    <SideBarComponent />
    <div class="content">
      <div class="header">
        <MSAlert
          :message="alertMessage"
          :type="alertType"
          :visible="alertVisible"
          :isClose="alertIsClose"
          @close="alertVisible = false"
          @cancel="alertVisible = false"
        />
        <h1><br /></h1>
      </div>
      <div class="create">
        <p class="links">
          <router-link to="/admin">Quay lại danh sách Users</router-link>
        </p>
      </div>
      <form @submit.prevent="saveUser" class="form-container" v-if="editUser">
        <h2 class="form-title">Chỉnh Sửa Thông Tin Người Dùng</h2>
        <div class="form-group-inline">
          <div class="form-group">
            <input
              type="text"
              v-model="editUser.firstName"
              placeholder="Họ và đệm"
              required
            />
          </div>
          <div class="form-group">
            <input
              type="text"
              v-model="editUser.lastName"
              placeholder="Tên"
              required
            />
          </div>
        </div>
        <div class="form-group">
          <input
            type="email"
            v-model="editUser.email"
            placeholder="Email"
            required
          />
        </div>
        <div class="form-group">
          <input
            type="text"
            v-model="editUser.phoneNumber"
            placeholder="Số điện thoại"
            required
          />
        </div>
        <div class="form-group-button">
          <button type="submit" class="edit-button">Lưu</button>
          <button type="button" @click="cancelEdit" class="cancel-button">
            Hủy
          </button>
        </div>
      </form>
      <div v-else class="loading-message">Đang tải dữ liệu người dùng...</div>
    </div>
  </div>
</template>

<script>
import { account } from "../../api/account";
import SideBarComponent from "../AdminPage/SideBarComponent.vue";
import MSAlert from "../BaseComponent/MSAlert.vue";
import MSInput from "../BaseComponent/MSInput.vue";

export default {
  name: "EditUserPage",
  components: {
    SideBarComponent,
    MSAlert,
    MSInput,
  },
  props: ["id"],
  data() {
    return {
      editUser: null,
      alertMessage: "",
      alertType: "info",
      alertVisible: false,
      alertIsClose: false,
    };
  },
  async created() {
    try {
      const response = await account.getUserById(this.id);
      this.editUser = response;
    } catch (error) {
      this.showAlert(
        "Lỗi khi hiển thị thông tin: " +
          (error.response ? error.response.data.message : error.message),
        "error"
      );
    }
  },
  methods: {
    async saveUser() {
      const response = await account.updateUser(this.editUser);

      if (response) {
        this.showAlert("Người dùng cập nhật thành công!", "success");
        setTimeout(() => {
          this.$router.push("/admin");
        }, 1500);
      } else {
        this.showAlert(
          "Lỗi khi cập nhật người dùng: " + response.message,
          "error"
        );
      }
    },

    cancelEdit() {
      this.$router.push("/admin");
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
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css");

.admin-page {
  display: flex;
  height: 100vh;
  background-color: #f0f2f5;
  font-family: Arial, sans-serif;
}
h2 {
  margin-bottom: 40px;
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
  padding: 20px 50px 50px 50px;
}

.form-group-inline {
  display: flex;
  gap: 20px;
  padding-bottom: 0px;
}

.form-group {
  margin-bottom: 15px;
  padding-bottom: 20px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ddd;
  border-radius: 2.5px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
}

.form-group input:focus {
  border-color: #1fa153;
  outline: none;
}
a {
  color: white;
}
.form-group-button {
  display: flex;
  gap: 10px;
}

.edit-button,
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
}

.edit-button {
  background-color: #4caf50; /* Màu xanh lá cây */
  color: white;
}

.cancel-button {
  background-color: #f44336; /* Màu đỏ */
  color: white;
}

.edit-button:hover,
.cancel-button:hover {
  opacity: 0.9;
}

.create {
  margin-top: 20px;
  text-align: left; /* Align left */
}

.create .links {
  display: inline-block;
  background-color: #007bff;
  color: white;
  font-size: 16px;
  font-weight: bold;
  text-align: center;
  vertical-align: middle;
  cursor: pointer;
  padding: 10px 20px;
  border-radius: 2.5px;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.create .links:hover {
  background-color: #0069d9;
}

.loading-message {
  color: #333;
  font-size: 18px;
}
</style>
