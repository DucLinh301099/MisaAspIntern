<template>
  <div class="datetime-component-wrapper">
    <div class="form-group">
      <label for="ngay-hach-toan">Ngày hạch toán</label>
      <MSInput
        :value="accountingDate"
        ref="AccountingDate"
        :errors="accountingDateErrors"
        class="date-input"
        @input="updateValue('accountingDate', $event.target.value)"
        type="date"
        placeholder="DD/MM/YY"
      />
    </div>
    <div class="form-group">
      <label for="ngay-chung-tu">Ngày chứng từ</label>
      <MSInput
        class="date-input"
        ref="DocumentDate"
        :errors="documentDateErrors"
        :value="documentDate"
        @input="updateValue('documentDate', $event.target.value)"
        type="date"
      />
    </div>
    <div class="form-group">
      <label for="so-chung-tu">Số chứng từ</label>
      <MSInput
        class="date-input"
        ref="DocumentNumber"
        :errors="documentNumberErrors"
        :value="documentNumber"
        @input="updateValue('documentNumber', $event.target.value)"
      />
    </div>
    <div class="form-group" v-if="voucherType === '3.Tạm ứng cho nhân viên'">
      <label for="han-quyet-toan">Hạn quyết toán</label>
      <MSInput
        class="date-input"
        :value="hanQuyetToan"
        @input="updateValue('hanQuyetToan', $event.target.value)"
        type="date"
      />
    </div>
  </div>
</template>

<script>
import BaseSubmit from "../Base/BaseSubmit.vue";
export default {
  name: "DateTimeComponent",
  extends: BaseSubmit,
  props: {
    voucherType: {
      type: String,
      required: true,
    },
    value: {
      type: Object,
      default: () => ({}),
    },
    accountingDateErrors: {
      type: Array,
      default: () => [],
    },
    documentDateErrors: {
      type: Array,
      default: () => [],
    },
    documentNumberErrors: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    const today = new Date().toISOString().split("T")[0]; // luôn hiển thị ngày hôm nay
    return {
      accountingDate: this.value.accountingDate || today,
      documentDate: this.value.documentDate || today,
      documentNumber: this.value.documentNumber,
      hanQuyetToan: this.value.hanQuyetToan || today,
    };
  },
  watch: {
    value: {
      handler(newVal) {
        const today = new Date().toISOString().split("T")[0];
        this.accountingDate = newVal.accountingDate || today;
        this.documentDate = newVal.documentDate || today;
        this.documentNumber = newVal.documentNumber;
        this.hanQuyetToan = newVal.hanQuyetToan || today;
      },
      deep: true,
      immediate: true,
    },
  },
  methods: {
    updateValue(field, value) {
      this[field] = value;
      this.$emit("update:value", {
        ...this.value,
        [field]: value,
      });
    },
  },
};
</script>

<style scoped>
.datetime-component-wrapper {
  display: flex;
  flex-direction: column;
  width: 300px;
  margin: 0 auto;
  border: 1px solid #999;
  border-top: none;
  border-bottom: none;
  border-right: none;
  padding-left: 25px;
  padding-top: 5px;
}
.input-with-button {
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  margin-right: auto;
  height: 30px;
  width: 70%;
}
.input-with-button-1 {
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  margin-right: auto;
  height: 30px;
  width: 70%;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 12px;
}
.date-input {
  box-sizing: border-box;
  outline: none;
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  margin-right: auto;
  height: 29px;
  width: 70%;
  display: flex;
  align-items: center;
  background-color: #fff;
}
label {
  margin-bottom: 8px;
  font-weight: bold;
}

.base-input input[type="date"] {
  width: calc(100% - 30px);
  border: none;
  outline: none;
  padding: 0 8px;
  box-sizing: border-box;
}

.base-input input {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  padding: 0 8px;
  box-sizing: border-box;
}

.base-input input[type="date"]::-webkit-calendar-picker-indicator {
  position: absolute;
  right: 8px;
  top: 8px;
  height: 14px;
  width: 14px;
  cursor: pointer;
}

.error-border {
  border: 2px solid red;
}
.focused {
  border-color: green;
}
.infocused {
  border-color: red;
}
.success-border {
  border: 1px solid green;
}
</style>
