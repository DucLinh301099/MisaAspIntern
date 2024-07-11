<!-- src/components/InformationInput.vue -->
<template>
  <div class="information-input-wrapper">
    <div class="input-container">
      <label for="so-cmnd">Số CMND</label>
      <BaseInput
        v-model="soCMND"
        placeholder="Số CMND"
        class="base-input cmnd-input"
        :class="{ focus: isCMNDFocused }"
        @focus="handleFocus('isCMNDFocused')"
        @blur="handleBlur('isCMNDFocused')"
      />
    </div>
    <div class="input-container-2">
      <div class="input-field">
        <label for="ngay-cap">Ngày cấp</label>
        <BaseInput
          v-model="ngayCap"
          type="date"
          placeholder="dd/mm/yyyy"
          class="base-input date-input"
          :class="{ focus: isDateFocused }"
          @focus="handleFocus('isDateFocused')"
          @blur="handleBlur('isDateFocused')"
        />
      </div>
      <div class="input-field">
        <label for="noi-cap">Nơi cấp</label>
        <BaseInput
          v-model="noiCap"
          placeholder="Nơi cấp"
          class="base-input place-input"
          :class="{ focus: isAddressFocused }"
          @focus="handleFocus('isAddressFocused')"
          @blur="handleBlur('isAddressFocused')"
        />
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../BaseComponent/BaseInputComponent.vue";

export default {
  name: "InformationInput",
  components: {
    BaseInput,
  },
  data() {
    return {
      soCMND: "",
      ngayCap: "",
      noiCap: "",
      isAddressFocused: false,
      isDateFocused: false,
      isCMNDFocused: false,
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
  },
};
</script>

<style scoped>
.information-input-wrapper {
  display: flex;
  gap: 20px;
  margin-bottom: 8px;
}

.input-container {
  flex: 1;
  max-width: 45%;
  display: flex;
  flex-direction: column;
}

.input-container-2 {
  flex: 1;
  display: flex;
  justify-content: space-between;
  gap: 16px;
  width: 45%;
}

.input-field {
  flex: 1;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 4px;
  font-weight: bold;
}
.input-wrapper.base-input.cmnd-input.focus {
  border-color: green;
}
.input-wrapper.base-input.date-input.focus {
  border-color: green;
}
.input-wrapper.base-input.place-input.focus {
  border-color: green;
}
.base-input {
  width: 100%;
  height: 32px;
  border: 1px solid #999;
  border-radius: 2px;
  padding: 0 8px;
  box-sizing: border-box;
}

.base-input input[type="date"] {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  padding: 0 8px;
}

@media (max-width: 768px) {
  .information-input-wrapper {
    flex-direction: column;
    gap: 10px;
  }
  .input-container,
  .input-container-2 {
    width: 100%;
  }
  .input-container-2 {
    flex-direction: column;
  }
}
</style>
