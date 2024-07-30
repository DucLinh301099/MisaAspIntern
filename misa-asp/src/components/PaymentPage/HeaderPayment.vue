<template>
  <div class="container">
    <!-- Header -->
    <div id="header-layout" class="header">
      <div class="top-header">
        <div class="left-section">
          <i class="material-icons mi-recent-log">history</i>
          <div class="title">{{ title }}</div>
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
            class="combo-input-1"
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
  documentNumber: {
    type: String,
    required: true,
  },
});
const emit = defineEmits(["update:voucherType", "update:paymentMethod"]);

const localVoucherType = ref(props.voucherType);
const localPaymentMethod = ref(props.paymentMethod);
const localdocumentNumber = ref(props.documentNumber);

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

watch(
  () => props.documentNumber,
  (newVal) => {
    localdocumentNumber.value = newVal;
  }
);

const voucherOptions = [
  "1. Trả tiền nhà cung cấp",
  "2. Trả các khoản vay",
  "3. Tạm ứng cho nhân viên",
  "4. Chi mua ngoài có hóa đơn",
  "5. Chi khác",
];

const paymentMethods = ["Ủy nhiệm chi", "Séc chuyển khoản", "Séc tiền mặt"];

const title = computed(
  () => `${localPaymentMethod.value} ${localdocumentNumber.value}`
);

const updateVoucherType = (value) => {
  emit("update:voucherType", value);
};

const updatePaymentMethod = (value) => {
  emit("update:paymentMethod", value);
};
</script>

<style scoped>
/* Updated CSS */
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
  justify-content: space-between;
  width: 100%;
  height: 45px;
}

.left-section {
  display: flex;
  align-items: center;
  width: 350px;
}

.mi-recent-log {
  font-size: 25px;
  margin-right: 5px;
  scale: 1.2;
}

.title {
  font-size: 24px;
  font-weight: bold;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  flex-grow: 1;
}

.center-section {
  display: flex;
  align-items: center;
  padding-left: 5px;
}

.combo-input {
  border: 1px solid #ccc;
  border-radius: 1.5px;
  min-width: 250px;
  font-size: 14px;
  height: 32px;
  min-height: 30px;

  margin-left: 5px;
  margin-right: 15px;
}
.combo-input-1 {
  border: 1px solid #ccc;
  border-radius: 1.5px;
  min-width: 200px;
  font-size: 14px;
  height: 32px;
  min-height: 30px;

  margin-left: 15px;
  margin-right: 5px;
}
.inline-label {
  margin-left: 5px;
  white-space: nowrap;
  font-weight: 300;
  margin-bottom: 0;
}

.right-section {
  display: flex;
  align-items: center;
  margin-left: auto;
  padding-right: 30px;
}

.icon-button-close {
  scale: 1.3;
  border: none;
  margin-left: 10px;
  background-color: #f7f7f7;
}

button:hover {
  background-color: #ccc;
}

.icon-button-setting {
  scale: 1.3;
  border: none;
  margin-left: 10px;
  background-color: #f7f7f7;
}

.icon-button-help {
  scale: 1.3;
  border: none;
  margin-left: 10px;
  background-color: #f7f7f7;
}

.icon-button i {
  font-size: 24px;
}

.multiselect-clear-icon {
  display: none;
}

.btn-add:hover {
  background-color: #218838;
}
</style>
