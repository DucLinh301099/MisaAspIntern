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
        <h1>Chỉnh Sửa Thông Tin Người Dùng</h1>
      </div>
      <div class="create">
        <p class="links">
          <router-link to="/admin">Quay lại danh sách User</router-link>
        </p>
      </div>
      <form
        @submit.prevent="saveUser"
        class="form-container"
        v-if="editingUser"
      >
        <div class="form-group-inline">
          <div class="form-group">
            <input
              type="text"
              v-model="editingUser.firstName"
              placeholder="Họ và đệm"
              required
            />
          </div>
          <div class="form-group">
            <input
              type="text"
              v-model="editingUser.lastName"
              placeholder="Tên"
              required
            />
          </div>
        </div>
        <div class="form-group">
          <input
            type="email"
            v-model="editingUser.email"
            placeholder="Email"
            required
          />
        </div>
        <div class="form-group">
          <input
            type="text"
            v-model="editingUser.phoneNumber"
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
import { updateUser, fetchUserById } from "../../api/account";

export default {
  name: "EditUserPage",
  props: ["id"],
  data() {
    return {
      editingUser: null,
    };
  },
  async created() {
    try {
      const response = await fetchUserById(this.id);
      this.editingUser = response;
    } catch (error) {
      console.error("Error fetching user:", error);
      alert(
        "Failed to fetch user data: " +
          (error.response ? error.response.data.message : error.message)
      );
    }
  },
  methods: {
    async saveUser() {
      try {
        await updateUser(this.editingUser);
        alert("User updated successfully!");
        this.$router.push("/admin");
      } catch (error) {
        console.error("Error saving user:", error);
        alert("Failed to update user: " + error.message);
      }
    },
    cancelEdit() {
      this.$router.push("/admin");
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

.form-container {
  background-color: #fff;
  padding: 40px 20px 40px 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: auto;
}

.form-group-inline {
  display: flex;
  gap: 20px;
  padding-bottom: 0px;
}
.form-group-button {
  display: flex;
  gap: 20px;
  justify-content: flex-start;
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
  border-radius: 5px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
}

.form-group input:focus {
  border-color: #007bff;
  outline: none;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
}
a {
  color: white;
}
.edit-button {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  display: flex;
  align-items: center;
}

.edit-button:hover {
  background-color: #218838;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  display: flex;
  align-items: center;
  margin-left: 10px;
}

.cancel-button:hover {
  background-color: #c82333;
}

.create {
  margin-top: 20px;
  text-align: left; /* Align left */
}

.create .links {
  display: inline-block;
  background-color: #007bff;
  color: white;
  padding: 10px 20px;
  border-radius: 5px;
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
