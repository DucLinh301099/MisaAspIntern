<template>
  <input
    :type="type"
    :value="value"
    @input="updateValue"
    @focus="handleFocus"
    @blur="handleBlur"
  />
  <!-- đoạn này sẽ thay thế bằng tooltip để hiển thị lỗi -->
  <!-- <span v-if="error" class="error">{{ error }}</span> -->
</template>

<script>
export default {
  name: "MSInput",
  props: {
    value: {
      type: Object,
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
  },
  computed: {
    error() {
      return this.errors.length > 0 ? this.errors[0].ErrorText : null;
    },
  },
  methods: {
    updateValue(event) {
      this.$emit("update:value", event.target.value);
    },
    handleFocus(event) {
      this.$emit("focus", event);
    },
    handleBlur(event) {
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
</style>