<template>
  <form @submit.prevent="handleSubmit">
    <slot ref="slotRef"></slot>
    <button type="submit">{{ submitButtonText }}</button>
  </form>
</template>

<script>
export default {
  name: "BaseForm",
  props: {
    submitButtonText: {
      type: String,
      default: "Submit",
    },
    customValidate: {
      type: Function,
      default: null,
    },
    customHandleLogic: {
      type: Function,
      default: null,
    },
    afterCallSuccess: {
      type: Function,
      default: null,
    },
    afterCallError: {
      type: Function,
      default: null,
    },
    refs: {
      type: Array,
      default: null,
    },
  },
  methods: {
    async handleSubmit() {
      if (this.customValidate) {
        this.customValidate();
      }

      let responseData = await this.customHandleLogic();

      if (this.afterCallSuccess && responseData.isSuccess) {
        this.afterCallSuccess();
      }

      if (this.afterCallError) {
        this.afterCallError();
      } else {
        let refsForm = this.refs;
        if (refsForm) {
          if (responseData.Code && responseData.Code.length) {
            for (let i = 0; i < responseData.Code.length; i++) {
              var item = responseData.Code[i];
              if (refsForm[item.FieldName]) {
                refsForm[item.FieldName].errors.push(item.ErrorText);
              }
            }
          }
        }
      }
    },
  },
};
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

button {
  align-self: flex-start;
  padding: 0.5rem 1rem;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}
</style>
