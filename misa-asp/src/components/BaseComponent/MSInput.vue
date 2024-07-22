<template>
  <div class="ms-input" :class="{ focused: isFocused, 'no-border': noBorder, 'has-error': error }">
    <input
      :type="type"
      :value="value"
      @input="updateValue"
      @focus="handleFocus"
      @blur="handleBlur"
      :placeholder="placeholder"
    />
    <span v-if="error" class="error-icon">!</span>
    <span v-if="error" class="error-tooltip">{{ error }}</span>
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
    errors: {
      type: Array,
      default: () => [],
    },
    placeholder: {
      type: String,
      default: "",
    },
    noBorder: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isFocused: false,
    };
  },
  computed: {
    error() {
      return this.errors && this.errors.length ? this.errors.join(", ") : "";
    },
  },
  methods: {
    updateValue(event) {
      this.$emit("update:value", event.target.value);
    },
    handleFocus(event) {
      this.isFocused = true;
      this.$emit("focus", event);
    },
    handleBlur(event) {
      this.isFocused = false;
      this.$emit("blur", event);
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
  border: none;
  height: 20px;
  font-size: 14px;
}

.ms-input.focused {
  border: 1px solid green;
}

.ms-input.no-border {
  border: none;
}

.error-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  right: 10px;
  width: 20px;
  height: 20px;
  border: 2px solid red;
  color: red;
  border-radius: 50%;
  font-size: 14px;
  line-height: 18px;
  text-align: center;
  font-weight: bold;
}

.error-tooltip {
  display: block;
  position: absolute;
  background-color: rgba(255, 0, 0, 0.8);
  color: white;
  font-size: 12px;
  padding: 5px;
  border-radius: 4px;
  top: 100%;
  left: 0;
  white-space: nowrap;
  z-index: 1000;
}
</style>
