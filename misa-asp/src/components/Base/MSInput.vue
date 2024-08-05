<template>
  <div
    class="ms-input"
    :class="{
      focused: isFocused,
      'no-border': noBorder,
      'has-error': errors,
      'combobox-error': comboboxError,
      'comboboxGrid-error': comboboxGridError,
    }"
  >
    <input
      :type="type"
      :value="value"
      @input="updateValue"
      @focus="handleFocus"
      @blur="handleBlur"
      :placeholder="placeholder"
      :disabled="disabled"
      :class="{ 'input-error': errors && !comboboxError, comboboxGridError }"
    />
    <span v-if="errors && type !== 'date'" class="error-icon">!</span>
    <span v-if="errors" class="error-tooltip">{{ errors }}</span>
  </div>
</template>

<script>
export default {
  name: "MSInput",
  props: {
    value: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    label: {
      type: String,
      default: null,
    },
    placeholder: {
      type: String,
      default: "",
    },
    noBorder: {
      type: Boolean,
      default: false,
    },
    comboboxError: {
      type: Boolean,
      default: false,
    },
    comboboxGridError: {
      type: Boolean,
      default: false,
    },
    field: {
      type: String,
      default: null,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isFocused: false,
      errors: null,
    };
  },
  methods: {
    updateValue(event) {
      this.$emit("update:value", event.target.value);
      this.errors = null;
    },
    handleFocus(event) {
      this.isFocused = true;
      this.$emit("focus", event);
    },
    handleBlur(event) {
      this.isFocused = false;
      this.$emit("blur", event);
    },
    setError(item) {
      this.errors = item;
    },
  },
};
</script>

<style scoped>
.ms-input {
  position: relative;
  display: flex;
  align-items: center;
}

.ms-input input {
  outline: none;
  width: 100%;
  border: 1px solid transparent;
  height: 20px;
  font-size: 14px;
  transition: border-color 0.3s;
  padding-left: 10px;
}

.ms-input.focused {
  border: 1px solid green;
}

.ms-input.no-border {
  border: none;
}

.ms-input.has-error {
  border: 1px solid #ff6666;
}

.ms-input.combobox-error {
  border: none;
}
.ms-input.comboboxGrid-error {
  border: none;
}

.error-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  right: 10px;
  width: 18px;
  height: 18px;
  border: 1px solid #ff6666;
  color: #ff6666;
  border-radius: 50%;
  font-size: 14px;
  line-height: 18px;
  text-align: center;
  font-weight: bold;
}

.error-tooltip {
  display: block;
  position: absolute;
  background-color: #ff6666;
  color: white;
  font-size: 12px;
  padding: 5px;
  border-radius: 4px;
  top: 100%;
  left: 0;
  white-space: nowrap;
  z-index: 1000;
  font-family: AvertaStdCY_Semibold, Helvetica, Arial, sans-serif;
  margin-top: 3px;
}
</style>
