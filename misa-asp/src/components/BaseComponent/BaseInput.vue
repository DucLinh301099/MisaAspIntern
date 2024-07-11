<template>
  <div
    class="base-input"
    :class="{ invalid: isInputFocused && !internalValue, valid: internalValue }"
  >
    <input
      v-model="internalValue"
      @input="updateValue($event.target.value)"
      @focus="handleFocus"
      @blur="handleBlur"
    />
    <span class="input-status" v-if="isInputFocused && !internalValue">!</span>
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  props: {
    value: String,
  },
  data() {
    return {
      internalValue: this.value || "",
      isInputFocused: false,
    };
  },
  watch: {
    value(newValue) {
      this.internalValue = newValue;
    },
  },
  methods: {
    updateValue(value) {
      this.internalValue = value;
      this.$emit("input", value);
    },
    handleFocus() {
      this.isInputFocused = true;
    },
    handleBlur() {
      this.isInputFocused = false;
    },
  },
};
</script>

<style scoped>
.base-input {
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  height: 30px;
  position: relative;
}

.base-input input {
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  padding: 0 8px;
}

.invalid {
  border-color: red;
}

.valid {
  border-color: #68c75b;
}

.input-status {
  color: red;
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  font-weight: bold;
  background: white;
  border: 1px solid red;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
}
</style>
