<script>
import { payment } from "../../api/payment";

export default {
  methods: {
    async handleSubmit(action) {
      this.customValidate();

      let paymentData = this.payment;
      let responseData;

      try {
        responseData = await payment.createPayment(paymentData);
      } catch (error) {
        responseData = { isSuccess: false, error };
      }

      if (responseData.isSuccess) {
        await this.afterCallSuccess(responseData);
        await this.handleCreateSubmit(responseData);
        this.$emit("afterCallSuccess", action, responseData);
      } else {
        await this.afterCallError(responseData);
        await this.afterCallErrorCustom(responseData);
        this.$emit("afterCallError", responseData);
      }

      if (action === "save") {
        // Logic for "Cất và Thêm"
      } else if (action === "close") {
        // Logic for "Cất và Đóng"
      } else if (action === "print") {
        // Logic for "Cất và In"
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
  },
};
</script>
