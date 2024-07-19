<template>
  <div class="sidebar">
    <router-link to="/" class="logo">
      <img src="https://asp.misa.vn/Content/Images/SVG/Logo.svg" alt="Logo" />
    </router-link>
    <ul class="menu">
      <li>
        <router-link to="/admin"
          ><i class="fa fa-tachometer"></i> Quản lý</router-link
        >
      </li>
      <li>
        <router-link to="/"><i class="fa fa-users"></i> Người dùng</router-link>
      </li>
      <li>
        <router-link to="/payment"
          ><i class="fa fa-bar-chart"></i> Kế toán</router-link
        >
      </li>

      <!-- Những trang này khi có thời gian sẽ phát triển thêm -->

      <!-- <li>
        <router-link to="/"><i class="fa fa-bar-chart"></i> Charts</router-link>
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
      </li> -->
    </ul>
  </div>
</template>

<script>
export default {
  name: "SideBarComponent",
};
</script>

<style scoped>
@import url("https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css");

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
</style>
