<template>
  <div class="datetime-component-wrapper">
    <div class="form-group">
      <label for="ngay-hach-toan">Ngày hạch toán</label>
      <div class="input-with-button-1">
        <MSInput
          :value="ngayHachToan"
          class="base-input"
          @input="updateValue('ngayHachToan', $event.target.value)"
          type="date"
        />
      </div>
    </div>
    <div class="form-group">
      <label for="ngay-chung-tu">Ngày chứng từ</label>
      <div class="input-with-button-1">
        <MSInput
          class="base-input"
          :value="ngayChungTu"
          @input="updateValue('ngayChungTu', $event.target.value)"
          type="date"
        />
      </div>
    </div>
    <div class="form-group">
      <label for="so-chung-tu">Số chứng từ</label>
      <div
        class="input-with-button"
        :class="{
          infocused: isSoChungTuFocused && !soChungTu,
          focused: soChungTu && isSoChungTuFocused,
        }"
      >
        <MSInput
          class="base-input"
          :value="soChungTu"
          @input="updateValue('soChungTu', $event.target.value)"
          @focus="handleFocus()"
          @blur="handleBlur()"
        />
      </div>
    </div>
    <div class="form-group" v-if="voucherType === '3.Tạm ứng cho nhân viên'">
      <label for="han-quyet-toan">Hạn quyết toán</label>
      <div class="input-with-button">
        <MSInput
          class="base-input"
          :value="hanQuyetToan"
          @input="updateValue('hanQuyetToan', $event.target.value)"
          type="date"
        />
      </div>
    </div>
  </div>
</template>

<script>
import MSInput from "../BaseComponent/MSInput.vue";

export default {
  name: "DateTimeInput",
  components: {
    MSInput,
  },
  props: {
    voucherType: {
      type: String,
      required: true,
    },
    value: {
      type: Object,
      default: () => ({}),
    },
  },
  data() {
    return {
      ngayHachToan: this.value.ngayHachToan,
      ngayChungTu: this.value.ngayChungTu,
      soChungTu: this.value.soChungTu,
      hanQuyetToan: this.value.hanQuyetToan,

      isSoChungTuFocused: false,
    };
  },
  watch: {
    value: {
      handler(newVal) {
        this.ngayHachToan = newVal.ngayHachToan;
        this.ngayChungTu = newVal.ngayChungTu;
        this.soChungTu = newVal.soChungTu;
        this.hanQuyetToan = newVal.hanQuyetToan;
      },
      deep: true,
      immediate: true,
    },
  },
  methods: {
    handleFocus() {
      this.resetFocusStates();
      this.isSoChungTuFocused = true;
    },
    handleBlur() {
      this.isSoChungTuFocused = false;
    },
    resetFocusStates() {
      this.isSoChungTuFocused = false;
    },
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
.input-with-button-1:focus-within {
  border-color: green;
}
.input-with-button:focus {
  border-color: green;
}
.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}
.base-input {
  width: 100%;
  height: 30px;
  border: none;
  padding: 0 8px;
  box-sizing: border-box;
  outline: none;
}
label {
  margin-bottom: 10px;
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
