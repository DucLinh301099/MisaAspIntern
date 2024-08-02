<template>
  <MSInput
    :value="value"
    :errors="errors"
    class="date-input"
    @input="handleInput"
    :type="type"
  />
</template>

<script>
export default {
  name: "MSDatetime",
  props: {
    value: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    errors: {
      type: Array,
      default: () => [],
    },
    field: {
      type: String,
      default: null,
    },
  },
  methods: {
    handleInput(event) {
      const value = event.target.value;
      this.$emit("input", value);
    },
  },
};
</script>

<style scoped></style>
