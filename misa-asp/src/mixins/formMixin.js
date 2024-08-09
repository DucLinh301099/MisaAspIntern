// src/mixins/formMixin.js
export default {
  data() {
    return {
      alertMessage: "",
      alertVisible: false,
      alertIsConfirm: false,
      alertIsShow: false,
      confirmAction: null,
      alertType: "", // Add type for alert
    };
  },
  methods: {
    async handleSubmit() {
      this.customValidate();

      let responseData = await this.customHandleLogic();

      if (responseData.isSuccess) {
        await this.afterCallSuccess(responseData);
        await this.handleCreateSubmit(responseData);
        this.$emit("afterCallSuccess", responseData);
      } else {
        await this.afterCallError(responseData);
        await this.afterCallErrorCustom(responseData);
        this.$emit("afterCallError", responseData);
      }
    },

    customValidate() {},

    async customHandleLogic() {},

    async afterCallSuccess() {},

    async afterCallError(responseData) {
      let refsForm = this.$refs;
      if (refsForm) {
        if (responseData.code && responseData.code.length) {
          for (let i = 0; i < responseData.code.length; i++) {
            var item = responseData.code[i];
            if (refsForm[item.FieldName]) {
              refsForm[item.FieldName].errors.push(item.ErrorText);
            }
          }
        }
      }
    },
    async afterCallErrorCustom() {},
    async handleCreateSubmit() {},

    showConfirm(message, action) {
      this.alertMessage = message;
      this.confirmAction = action;
      this.alertVisible = true;
      this.alertIsConfirm = true;
      this.alertIsShow = true; // Ensure this is set to true
    },
    handleConfirm() {
      if (this.confirmAction) {
        this.confirmAction();
      }
      this.alertVisible = false;
    },
  },
};
