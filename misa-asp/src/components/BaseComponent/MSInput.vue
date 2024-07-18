<template>
  <input
    :class="classCustom"
    :type="type"
    :value="value"
    @input="updateValue"
    @focus="handleFocus"
    @blur="handleBlur"
  />
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
    classCustom: {
      type: String,
      default: null,
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

<style></style>
