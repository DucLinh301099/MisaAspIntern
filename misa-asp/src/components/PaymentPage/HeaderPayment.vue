<template>
  <div class="container">
    <!-- Header -->
    <div id="header-layout" class="header">
      <div class="top-header">
        <div class="left-section">
          <i class="material-icons mi-recent-log">history</i>
          <div class="title">{{ dynamicTitle }}</div>
        </div>
        <div class="center-section">
          <Multiselect
            v-model="localVoucherType"
            :options="voucherOptions"
            class="combo-input"
            @update:modelValue="updateVoucherType"
          ></Multiselect>
          <label class="inline-label">Phương thức thanh toán</label>
          <Multiselect
            v-model="localPaymentMethod"
            :options="paymentMethods"
            class="combo-input"
            @update:modelValue="updatePaymentMethod"
          ></Multiselect>
        </div>
        <div class="right-section">
          <button class="icon-button-setting">
            <i class="material-icons">settings</i>
          </button>
          <button class="icon-button-help">
            <i class="material-icons">help_outline</i>
          </button>
          <button class="icon-button-close">
            <i class="material-icons">close</i>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, watch } from "vue";
import Multiselect from "@vueform/multiselect";
import "@vueform/multiselect/themes/default.css";
import "vue-multiselect/dist/vue-multiselect.css";

// Nhận prop từ component cha
const props = defineProps({
  voucherType: {
    type: String,
    required: true,
  },
  paymentMethod: {
    type: String,
    required: true,
  },
});
const emit = defineEmits(["update:voucherType", "update:paymentMethod"]);

const localVoucherType = ref(props.voucherType);
const localPaymentMethod = ref(props.paymentMethod);

watch(
  () => props.voucherType,
  (newVal) => {
    localVoucherType.value = newVal;
  }
);

watch(
  () => props.paymentMethod,
  (newVal) => {
    localPaymentMethod.value = newVal;
  }
);

const voucherOptions = [
  "1.Trả tiền nhà cung cấp",
  "2.Trả các khoản vay",
  "3.Tạm ứng cho nhân viên",
  "4. Chi mua ngoài có hóa đơn",
  "5. Chi khác",
];

const paymentMethods = ["Ủy nhiệm chi", "Séc chuyển khoản", "Séc tiền mặt"];

const dynamicTitle = computed(() => `${localPaymentMethod.value} UNC00001`);

const updateVoucherType = (value) => {
  emit("update:voucherType", value);
};

const updatePaymentMethod = (value) => {
  emit("update:paymentMethod", value);
};
</script>

<style scoped>
@import "@vueform/multiselect/themes/default.css";
.container {
  background-color: #f4f5f8;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}

#payment-layout,
#form-layout {
  width: 45%;
  margin: 20px;
}

#header-layout {
  background-color: #f7f7f7;
  border-bottom: 1px solid #e5e5e5;
  padding: 5px 10px;
  width: 100%;
}

.top-header {
  display: flex;
  align-items: center;
  justify-content: flex-start;
  width: 100%;
  height: 45px;
}

.left-section {
  display: flex;
  align-items: center;
}

.mi-recent-log {
  font-size: 25px;
  margin-right: 10px;
  scale: 1.2;
}

.title {
  font-size: 26px;
  font-weight: bold;
}

.center-section {
  display: flex;
  align-items: center;
}

.combo-input {
  border: 1px solid #ccc;
  border-radius: 1.5px;
  min-width: 220px;
  font-size: 14px;
  height: 32px;
  min-height: 30px;
  padding: 0 8px;
  margin-left: 15px;
  margin-right: 15px;
}

.inline-label {
  margin-left: 10px;
  white-space: nowrap;
}

.right-section {
  display: flex;
  align-items: center;
  margin-left: auto;
  margin-right: 50px;
}

.icon-button-close {
  scale: 1.3;
  border: none;
  margin-left: 15px;
  background-color: #f7f7f7;
}
button:hover {
  background-color: #ccc;
}
.icon-button-setting {
  scale: 1.3;
  border: none;
  margin-left: 15px;
  background-color: #f7f7f7;
}
.icon-button-help {
  scale: 1.3;
  border: none;
  margin-left: 15px;
  background-color: #f7f7f7;
}
.icon-button i {
  font-size: 24px;
}

.body-container {
  display: flex;
  justify-content: space-around;
  font-weight: bold;
}

.form-body {
  padding: 20px;

  border-radius: 2px;
  /* box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1); */
  width: 100%;
  background-color: #f4f5f8;
}
.form-body-right {
  padding: 20px;
  background-color: #f4f5f8;

  border: 0.5px solid #666;
  border-right: none;
  border-bottom: none;
  border-top: none;
  width: 50%;
}
form {
  display: grid;
  grid-template-columns: 1fr;
}

.form-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
}
.form-row1 {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
  flex-direction: column;
}
.input-container {
  display: flex;
  flex-direction: column;
  flex: 1;
  margin-right: 20px;
}

.input-group {
  display: flex;
  align-items: center;
  position: relative;
}

.input-field {
  flex: 1;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 2ch;
  margin-top: 5px;
}

.btn-add {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 5px 10px;
  border-radius: 2px;
  cursor: pointer;
  margin-left: -30px;
  margin-top: 5px;
}
.multiselect-clear-icon {
  display: none; /* Hide clear icon */
}
.btn-add:hover {
  background-color: #218838;
}

.combo-select {
  margin-left: 5px;
}

.left-column {
  display: flex;
  flex-direction: column;

  margin-right: 20px;
}

.right-column {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  justify-content: center;
  min-width: 120px;
}

.total-amount-label {
  font-size: 16px;
  font-weight: bold;
}

.total-amount {
  font-size: 24px;
  font-weight: bold;
  margin-top: 10px;
}
</style>
