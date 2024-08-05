<template>
  <MSInput
    :value="value"
    class="date-input"
    @input="handleInput"
    :type="type"
    ref="inputComponent"
  />
</template>

<script>
export default {
  name: "MSDatetime",
  data() {
    return {
      errors: null,
      
    };
  },
  props: {
    value: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    
  },
  watch: {
    errors(newVal) {
      let refList = this.$refs;
      if (refList["inputComponent"]) {
        refList["inputComponent"].setError(newVal);
      }
    },
  },
  methods: {
    handleInput(event) {
      const value = event.target.value;
      this.$emit("input", value);
      this.errors = null;
      this.$refs.inputComponent.setError(null);
    },
    setError(item) {
      this.errors = item;
    },
  },
};
</script>

<style scoped></style>
