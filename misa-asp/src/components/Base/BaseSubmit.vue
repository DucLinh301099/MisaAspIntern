<script>
import { baseApi } from "../../api/baseApi";
import BaseForm from "./BaseForm.vue";

export default {
  extends: BaseForm,
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
        // trải phẳng refs ra
        let refList = [];
        for (const key in refsForm) {
          if (refsForm.hasOwnProperty(key)) {
            const item = refsForm[key];
            if (item.refComponent && item.refComponent.length) {
              const refChild = item.$refs;
              refList.push(refChild);
            } else {
              refList.push(item);
            }
          }
        }

        if (
          refList &&
          refList.length &&
          responseData.code &&
          responseData.code.length
        ) {
          for (let i = 0; i < responseData.code.length; i++) {
            let item = responseData.code[i],
              refItemError = refList.find((i) => i[item.FieldName]);
            if (refItemError) {
              refItemError[item.FieldName].errors.push(item.ErrorText);
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
