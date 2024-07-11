<template>
  <div class="datetime-component-wrapper">
    <div class="form-group">
      <label for="ngay-hach-toan">Ngày hạch toán</label>
      <div
        class="input-with-button"
        :class="{ focused: isNgayHachToanFocused }"
      >
        <BaseInput
          :value="ngayHachToan"
          @onInput="updateNgayHachToan"
          type="date"
          @focus="handleFocus('isNgayHachToanFocused')"
          @blur="handleBlur('isNgayHachToanFocused')"
        />
      </div>
    </div>
    <div class="form-group">
      <label for="ngay-chung-tu">Ngày chứng từ</label>
      <div class="input-with-button" :class="{ focused: isNgayChungTuFocused }">
        <BaseInput
          :value="ngayChungTu"
          @onInput="updateNgayChungTu"
          type="date"
          @focus="handleFocus('isNgayChungTuFocused')"
          @blur="handleBlur('isNgayChungTuFocused')"
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
        <BaseInput
          :value="soChungTu"
          @onInput="updateSoChungTu"
          @focus="handleFocus('isSoChungTuFocused')"
          @blur="handleBlur('isSoChungTuFocused')"
        />
      </div>
    </div>
    <div class="form-group" v-if="voucherType === '3.Tạm ứng cho nhân viên'">
      <label for="han-quyet-toan">Hạn quyết toán</label>
      <div
        class="input-with-button"
        :class="{ focused: isHanQuyetToanFocused }"
      >
        <BaseInput
          :value="hanQuyetToan"
          @onInput="updateHanQuyetToan"
          type="date"
          placeholder="DD/MM/YYYY"
          @focus="handleFocus('isHanQuyetToanFocused')"
          @blur="handleBlur('isHanQuyetToanFocused')"
        />
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../BaseComponent/BaseInputComponent.vue";

export default {
  name: "DateTimeComponent",
  components: {
    BaseInput,
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
      ngayHachToan: this.value.ngayHachToan || "",
      ngayChungTu: this.value.ngayChungTu || "",
      soChungTu: this.value.soChungTu || "",
      hanQuyetToan: this.value.hanQuyetToan || "",
      isNgayHachToanFocused: false,
      isNgayChungTuFocused: false,
      isSoChungTuFocused: false,
      isHanQuyetToanFocused: false,
    };
  },

  methods: {
    handleFocus(field) {
      this.resetFocusStates();
      this[field] = true;
    },
    handleBlur(field) {
      this[field] = false;
    },
    resetFocusStates() {
      this.isNgayHachToanFocused = false;
      this.isNgayChungTuFocused = false;
      this.isSoChungTuFocused = false;
      this.isHanQuyetToanFocused = false;
    },
    updateSoChungTu(value) {
      this.soChungTu = value;
      this.$emit("input", { ...this.value, soChungTu: value });
    },
    updateNgayHachToan(value) {
      this.ngayHachToan = value;
      this.$emit("input", { ...this.value, ngayHachToan: value });
    },
    updateNgayChungTu(value) {
      this.ngayChungTu = value;
      this.$emit("input", { ...this.value, ngayChungTu: value });
    },
    updateHanQuyetToan(value) {
      this.hanQuyetToan = value;
      this.$emit("input", { ...this.value, hanQuyetToan: value });
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
.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}

label {
  margin-bottom: 2px;
  font-weight: bold;
}

.base-input {
  width: 100%;
  height: 30px;
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 0 8px;
  box-sizing: border-box;
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
