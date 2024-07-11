<!-- src/components/CreateCustomer.vue -->
<template>
  <div class="create-customer">
    <h2 class="form-title">Thông tin nhà cung cấp</h2>
    <form @submit.prevent="createCustomer">
      <div class="form-row">
        <div class="input-container">
          <label for="object-id"
            >Mã khách hàng<span class="required">*</span>
          </label>
          <input
            type="text"
            id="object-id"
            v-model="objectId"
            class="input-field-a"
            required
          />
        </div>
        <div class="input-container">
          <label for="object-name"
            >Tên khách hàng<span class="required">*</span></label
          >
          <input
            type="text"
            id="object-name"
            v-model="objectName"
            class="input-field-a"
            required
          />
        </div>
      </div>
      <div class="form-row">
        <div class="input-container">
          <label for="tax-code"
            >Mã số thuế<span class="required">*</span></label
          >
          <input
            type="text"
            id="tax-code"
            v-model="taxCode"
            class="input-field-a"
            required
          />
        </div>
        <div class="input-container">
          <label for="phone">Điện thoại<span class="required">*</span></label>
          <input
            type="text"
            id="phone"
            v-model="phoneNumber"
            class="input-field-a"
            required
          />
        </div>
      </div>
      <div class="form-row">
        <div class="input-container full-width">
          <label for="address">Địa chỉ<span class="required">*</span></label>
          <input
            type="text"
            id="address"
            v-model="address"
            class="input-field"
            required
          />
        </div>
      </div>
      <div class="divide-customer"><!----></div>
      <div v-if="generalError" class="error-message">{{ generalError }}</div>
      <div class="form-actions">
        <div class="btn-cancel-container">
          <button type="button" class="btn cancel-btn" @click="$emit('close')">
            Hủy
          </button>
        </div>
        <div class="btn-container">
          <button type="button" class="btn save-btn" @click="save">Cất</button>

          <button type="submit" class="btn submit-btn">Cất và Thêm</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { CreateCustomer } from "../../api/customer";

export default {
  name: "CreateCustomer",
  data() {
    return {
      objectId: "",
      objectName: "",
      taxCode: "",
      address: "",
      phoneNumber: "",
      generalError: "",
    };
  },
  methods: {
    async createCustomer() {
      try {
        console.log("Attempting to create customer...");
        const data = await CreateCustomer(
          this.objectId,
          this.objectName,
          this.taxCode,
          this.address,
          this.phoneNumber
        );
        console.log("Tạo mới Customer thành công:", data);
        alert("Tạo mới Customer thành công!");
        this.$router.push("/payment");
      } catch (error) {
        console.error("Lỗi khi tạo mới Customer:", error);
        this.generalError = error.message;
      }
    },
  },
};
</script>

<style scoped>
.create-customer {
  background-color: #fff;

  border-radius: 10px;
  max-width: 900px;
  padding-top: 5px;
}
.divide-customer {
  width: 100%;
  border-top: 1px solid #e0e0e0;

  padding-bottom: 5px;
  position: relative;
}
.form-title {
  font-size: 24px;
  margin-bottom: 30px;
  color: #333;
}

.form-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 25px;
}

.input-container {
  display: flex;
  flex-direction: column;
  flex: 1;
  margin-right: 40px;
}

.input-container.full-width {
  flex: 0 0 100%;
  margin-right: 0;
}
.required {
  color: red;
}
.input-container:last-child {
  margin-right: 0;
}

label {
  font-weight: 600;
  margin-bottom: 8px;
}

.input-field-a {
  padding: 8px;
  border: 1px solid #babec5;
  border-radius: 2.5px;
  font-size: 14px;
  outline: none;
  color: #333;
  width: 200px;
}
.input-field {
  padding: 8px;
  border: 1px solid #babec5;
  border-radius: 2.5px;
  font-size: 14px;
  outline: none;
  color: #333;
}
.input-field:focus {
  border-color: #28a745;
}
.input-field-a:focus {
  border-color: #28a745;
}
.form-actions {
  display: flex;
  justify-content: flex-end;
  margin-top: 18px;
}

.btn-container {
  flex: 1;
  display: flex;
  justify-content: flex-end;
  font-weight: bold;
}

.btn {
  padding: 10px 20px;
  border: 1px solid #8d9096;
  border-radius: 3px;
  cursor: pointer;
  background-color: white;
  font-weight: 600;
}

.cancel-btn {
  color: #000;
}

.save-btn {
  color: #000;
  margin-right: 13px;
}

.submit-btn {
  background-color: #28a745;
  color: white;
  border: none;
}

.submit-btn:hover {
  background-color: #218838;
}

.error-message {
  color: red;
  margin-bottom: 20px;
}
</style>
