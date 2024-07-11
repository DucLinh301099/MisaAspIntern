<template>
  <div class="admin-page">
    <div class="sidebar">
      <router-link to="/" class="logo">
        <img src="https://asp.misa.vn/Content/Images/SVG/Logo.svg" alt="Logo" />
      </router-link>
      <ul class="menu">
        <li>
          <router-link to="/"
            ><i class="fa fa-tachometer"></i> Dashboard</router-link
          >
        </li>
        <li>
          <router-link to="/"><i class="fa fa-users"></i> User</router-link>
        </li>
        <li>
          <router-link to="/"
            ><i class="fa fa-user"></i> User Profile</router-link
          >
        </li>
        <li>
          <router-link to="/"
            ><i class="fa fa-credit-card"></i> Account</router-link
          >
        </li>
        <li>
          <router-link to="/"
            ><i class="fa fa-bar-chart"></i> Charts</router-link
          >
        </li>
        <li>
          <router-link to="/"><i class="fa fa-pencil"></i> Forms</router-link>
        </li>
        <li>
          <router-link to="/"><i class="fa fa-th"></i> Apps</router-link>
        </li>
        <li>
          <router-link to="/"><i class="fa fa-map"></i> Maps</router-link>
        </li>
        <li>
          <router-link to="/"><i class="fa fa-file"></i> Pages</router-link>
        </li>
      </ul>
    </div>
    <div class="content">
      <div class="header">
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
import { deleteUserById, fetchProtectedData } from "../../api/account";

export default {
  name: "AdminComponent",
  data() {
    return {
      users: [],
      searchQuery: "",
      userName: localStorage.getItem("lastName") || "",
      userAvatar:
        "https://static.vecteezy.com/system/resources/thumbnails/007/407/996/small/user-icon-person-icon-client-symbol-login-head-sign-icon-design-vector.jpg", // Placeholder avatar, you can replace with actual URL
    };
  },
  async created() {
    await this.loadUsers();
  },
  methods: {
    async loadUsers() {
      try {
        const response = await fetchProtectedData();
        this.users = response;
      } catch (error) {
        console.error("Error fetching users:", error);
        alert(
          "Failed to fetch user data: " +
            (error.response ? error.response.data.message : error.message)
        );
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
      if (confirm("Are you sure you want to delete this user?")) {
        try {
          await deleteUserById(id);
          this.users = this.users.filter((user) => user.id !== id);
          alert("User deleted successfully!");
        } catch (error) {
          console.error("Error deleting user:", error);
          alert("Failed to delete user: " + error.message);
        }
      }
    },
    logout() {
      // Xóa role và lastName khỏi localStorage
      localStorage.removeItem("role");
      localStorage.removeItem("lastName");

      // Chuyển hướng về trang đăng nhập
      this.$router.push("/login");
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
  border-radius: 5px;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 8px;
  margin-left: 10px;
}

.logout-button:hover {
  color: #007bff;
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
  border-radius: 5px;
  font-size: 16px;
  width: 250px;
}

.create-user .create-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 5px;
  cursor: pointer;
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
  border-radius: 3px;
  cursor: pointer;
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
