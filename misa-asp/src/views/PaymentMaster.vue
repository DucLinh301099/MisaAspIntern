<template>
  <div>
    <Header />
    <div class="body-payment">
      <div class="side-bar">
        <!-- SideBar -->
        <SideBar />
      </div>
      <div class="main-content">
        <!-- Main content goes here -->
        <MainContent />
      </div>
    </div>
  </div>
</template>

<script>
import Header from "../components/PaymentMaster/Header.vue";
import SideBar from "../components/PaymentMaster/SideBar.vue";
import MainContent from "../components/PaymentMaster/MainContent.vue";

export default {
  name: "Payment",
  components: {
    Header,
    SideBar,
    MainContent,
  },
};
</script>

<style scoped>
body {
  margin: 0;
  font-family: Arial, sans-serif;
}

header {
  width: 100%;
  background-color: #f8f8f8;
  padding: 10px 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  position: fixed;
  top: 0;
  z-index: 1000;
}

.body-payment {
  display: flex;
  margin-top: 40px;
}

.side-bar {
  width: 20%;
  background-color: #f8f8f8;
  padding: 10px;
  border-right: 1px solid #ddd;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}

.main-content {
  width: 80%;
  padding: 20px;
  overflow-y: auto;
  background-color: #ddd;
}
</style>
