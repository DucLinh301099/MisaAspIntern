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
          :isClose="alertIsClose"
          @close="alertVisible = false"
          @confirm="handleConfirm"
          @cancel="alertVisible = false"
        />
        <h1>Dashboard</h1>
        <div class="user-info">
          <img :src="userAvatar" alt="Avatar" class="avatar" />
          <span>{{ userName }}</span>
          <button @click="logout" class="logout-button">
            <i class="fa fa-sign-out"></i> Đăng xuất
          </button>
        </div>
      </div>
      <div class="search-create-container">
        <div class="search-box">
          <input
            type="text"
            v-model="searchQuery"
            @input="searchUsers"
            placeholder="Tìm Kiếm"
          />
        </div>
        <div class="create-user">
          <button class="create-button" @click="createUser">
            <i class="fa fa-user-plus"></i> Tạo mới User
          </button>
        </div>
      </div>
      <table class="user-table">
        <thead>
          <tr>
            <th>Họ Tên</th>
            <th>Email</th>
            <th>Số điện thoại</th>
            <th>Role</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in users" :key="user.id">
            <td>{{ user.firstName }} {{ user.lastName }}</td>
            <td>{{ user.email }}</td>
            <td>{{ user.phoneNumber }}</td>
            <td>{{ user.roleName }}</td>
            <td>
              <button @click="editUser(user.id)">Sửa</button>
              <button @click="deleteUser(user.id)">Xóa</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import { account } from "../../api/account";
import SideBarComponent from "../AdminPage/SideBarComponent.vue";
import MSAlert from "../BaseComponent/MSAlert.vue";
export default {
  name: "AdminComponent",
  components: {
    SideBarComponent,
    MSAlert,
  },
  data() {
    return {
      users: [],
      searchQuery: "",
      userName: localStorage.getItem("lastName") || "",
      userAvatar:
        "https://static.vecteezy.com/system/resources/thumbnails/007/407/996/small/user-icon-person-icon-client-symbol-login-head-sign-icon-design-vector.jpg", // Placeholder avatar, you can replace with actual URL
      alertMessage: "",
      alertType: "info",
      alertVisible: false,
      alertIsConfirm: false,
      confirmAction: null,
      alertIsClose: false,
    };
  },
  async created() {
    await this.loadUsers();
  },
  methods: {
    async loadUsers() {
      let res = await account.getAllUser();
      if (res.isSuccess) {
        this.users = res.data;
      }
    },
    searchUsers() {
      // Implement search functionality if needed
    },
    createUser() {
      this.$router.push({ path: `/create-user` });
    },
    editUser(userId) {
      this.$router.push({ path: `/edit-user/${userId}` });
    },
    async deleteUser(id) {
      this.showConfirm(
        "Bạn có chắc chắn muốn xóa người dùng này?",
        async () => {
          try {
            await account.deleteUserById(id);
            this.users = this.users.filter((user) => user.id !== id);
            this.showAlert("Xóa người dùng thành công", "info");
          } catch (error) {
            this.showAlert(
              "Xóa người dùng thất bại: " + error.message,
              "error"
            );
          }
        }
      );
    },
    async logout() {
      // Hiển thị hộp thoại xác nhận
      this.showConfirm("Bạn có chắc chắn muốn đăng xuất không?", async () => {
        try {
          await account.logout();
          localStorage.removeItem("role");
          localStorage.removeItem("lastName");
          this.$router.push("/login");
        } catch (error) {
          this.showAlert("Đăng xuất thất bại: " + error.message, "error");
        }
      });
    },
    showAlert(message, type) {
      this.alertMessage = message;
      this.alertType = type;
      this.alertVisible = true;
      this.alertIsConfirm = false;
      this.alertIsClose = true;
    },
    showConfirm(message, action) {
      this.alertMessage = message;
      this.confirmAction = action;
      this.alertVisible = true;
      this.alertIsConfirm = true;
      this.alertIsClose = false;
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

<style scoped>
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css");

.admin-page {
  display: flex;
  height: 100vh;
  background-color: #f0f2f5;
  font-family: Arial, sans-serif;
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
  border-radius: 5px;
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

.user-info {
  display: flex;
  align-items: center;
}

.user-info .avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  margin-right: 10px;
}

.logout-button {
  padding: 10px 20px;
  font-size: 16px;
  color: white;
  background-color: #dc3545;
  border: none;
  border-radius: 3px;
  cursor: pointer;
  font-weight: bold;
  display: flex;
  align-items: center;
  gap: 8px;
  margin-left: 10px;
}

.logout-button:hover {
  color: #bebbbb;
}

.search-create-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.search-box input {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 3px;
  font-size: 16px;
  width: 250px;
  outline: none;
}

.create-user .create-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 3px;
  cursor: pointer;
  font-weight: bold;
  font-size: 16px;
  display: flex;
  align-items: center;
}

.create-user .create-button i {
  margin-right: 8px;
}

.create-user .create-button:hover {
  background-color: #0069d9;
}

.user-table {
  width: 100%;
  border-collapse: collapse;
  background-color: #ffffff;
  border-radius: 5px;
  overflow: hidden;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.user-table th,
.user-table td {
  border: 1px solid #ddd;
  padding: 12px;
  text-align: left;
}

.user-table th {
  background-color: #28a745;
  color: #ffffff;
  font-weight: bold;
  text-transform: uppercase;
}

.user-table td.actions {
  text-align: center;
}

.user-table tr:nth-child(even) {
  background-color: #f9f9f9;
}

.user-table tr:hover {
  background-color: #f1f1f1;
}

.user-table button {
  margin-right: 10px;
  padding: 8px 12px;
  border: none;
  border-radius: 2px;
  cursor: pointer;
  font-weight: bold;
  font-family: Arial;
  transition: background-color 0.3s ease;
}

.user-table button:first-of-type {
  background-color: #ffc107;
  color: white;
}

.user-table button:first-of-type:hover {
  background-color: #e0a800;
}

.user-table button:last-of-type {
  background-color: #dc3545;
  color: white;
}

.user-table button:last-of-type:hover {
  background-color: #c82333;
}
</style>
