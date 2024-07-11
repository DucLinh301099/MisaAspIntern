<template>
  <div class="footer-payment">
    <button class="cancel-btn" @click="cancel">Hủy</button>

    <div class="dropdown">
      <button class="save-btn" @click="submitForm('save')">Cất</button>
      <div class="dropdown-container">
        <button class="btn save-add-btn" @click="submitForm('saveAndPrint')">
          Cất và In
        </button>
        <button class="dropdown-toggle" @click="toggleDropdown">&#9660;</button>
        <div class="dropdown-menu" v-if="isDropdownVisible">
          <button @click="handleAction('add')">Cất và Thêm</button>
          <button @click="handleAction('close')">Cất và Đóng</button>
          <button @click="handleAction('print')">Cất và In</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "FooterPayment",
  data() {
    return {
      isDropdownVisible: false,
    };
  },
  methods: {
    toggleDropdown() {
      this.isDropdownVisible = !this.isDropdownVisible;
    },
    handleAction(action) {
      console.log(`Action selected: ${action}`);
      this.isDropdownVisible = false;
      this.submitForm(action);
    },
    async submitForm(action) {
      try {
        const paymentData = this.$root.$data.payment; // đảm bảo payment có dữ liệu hoặc nó là 1 prop
        console.log("Submitting payment data: ", paymentData);

        // api call từ backend
        const response = await axios.post("/api/payment", paymentData);

        console.log("Payment submitted successfully", response);

        // xử lý các submit khác nhau
        if (action === "add") {
          // Logic for "Cất và Thêm"
        } else if (action === "close") {
          // Logic for "Cất và Đóng"
        } else if (action === "print") {
          // Logic for "Cất và In"
        }
      } catch (error) {
        console.error("Error submitting payment", error);
      }
    },
    cancel() {
      console.log("Cancel action");
      // logic khi cancel
    },
  },
};
</script>

<style scoped>
.footer-payment {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #38393d;
  padding: 10px 20px;
  position: fixed;
  bottom: 0;
  width: 100%;
  box-shadow: 0 -2px 4px rgba(0, 0, 0, 0.1);
  z-index: 1000;
}

.btn {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}

.cancel-btn {
  background-color: #38393d;
  color: white;
  padding: 10px 20px;
  border: 1px solid #6b6c72;
  border-radius: 3px;
  font-size: 16px;
  cursor: pointer;
}

.save-btn {
  background-color: #38393d;
  color: white;
  margin-right: 10px;
  padding: 10px 20px;
  border: 1px solid #6b6c72;
  border-radius: 3px;
  font-size: 16px;
  cursor: pointer;
}

.save-add-btn {
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 2.5px 0 0 2.5px;
}

.dropdown {
  display: flex;
  align-items: center;
  margin-right: 45px;
}

.dropdown-container {
  position: relative;
  display: flex;
  align-items: center;
}

.dropdown-toggle {
  padding: 10px;
  border: 1px solid #fff;
  border-top: none;
  border-right: none;
  border-bottom: none;
  background-color: #28a745;
  color: white;
  cursor: pointer;
  border-radius: 0 2.5px 2.5px 0;
  font-size: 16px;
}

.dropdown-menu {
  position: absolute;
  bottom: 100%; /* Position above the dropdown container */
  left: 0;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  z-index: 1000;
  display: flex;
  flex-direction: column;
  border: 1px solid #babec5;
}

.dropdown-menu button {
  padding: 10px 20px;
  font-size: 16px;
  background: none;
  border: none;
  cursor: pointer;
  text-align: left;
}

.dropdown-menu button:hover {
  background-color: #68c75b;
}
</style>
