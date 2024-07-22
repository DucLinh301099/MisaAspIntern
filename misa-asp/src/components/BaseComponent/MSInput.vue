<template>
  <div class="ms-input" :class="{ focused: isFocused, 'no-border': noBorder }">
    <input
      :type="type"
      :value="value"
      @input="updateValue"
      @focus="handleFocus"
      @blur="handleBlur"
      :placeholder="placeholder"
    />
    <!-- đoạn này sẽ thay thế bằng tooltip để hiển thị lỗi -->
    <span v-if="error" class="error">{{ error }}</span>
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
.error {
  color: red;
  font-size: 12px;
}
.ms-input {
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
</style>
