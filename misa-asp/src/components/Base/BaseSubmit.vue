<script>
import { baseApi } from "../../api/baseApi";

export default {
  extends: "BaseForm",
  methods: {
    /**
     * function xử lý submit form và xử lý các quá trình
     * sau khi thành công, thật bại
     * @param param0
     */
    async handleSubmit(action) {
      if (action == "cancel" || action == "close") {
        // xử lý cho đóng hoặc hủy
      } else {
        this.customValidate();

        let responseData;

        try {
          responseData = await baseApi.postAuthenApi(
            this.apiUrl,
            this.currentItem
          );
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

        switch (action) {
          case "save":
            // Logic for "Cất và Xem"
            break;
          case "saveAndAdd":
            // Logic for "Cất và Xem"
            break;
          case "saveAndPrint":
            // Logic for "Cất và In"
            break;
          case "saveAndClose":
            // Logic for "Cất và Đóng"
            break;
        }
      }
    },

    customValidate() {},

    async customHandleLogic() {},

    async afterCallSuccess() {},

    /**
     * function xử lý sau khi submit có lỗi
     * @param responseData
     */
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
