<script>
import { baseApi } from "../../api/baseApi";
import BaseForm from "./BaseForm.vue";

export default {
  extends: BaseForm,
  data() {
    return {
      isDisabled: false, // Thêm biến trạng thái
    };
  },
  methods: {
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
            if (responseData.isSuccess && action === "save") {
              // Kiểm tra nếu action là "save"
              this.isDisabled = true; // Vô hiệu hóa các input
            }
            break;
          case "saveAndAdd":
            // Logic for "Cất và đóng"
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

    async afterCallError(responseData) {
      let refsForm = this.$refs;

      if (refsForm) {
        // trải phẳng refs ra
        let refList = [];
        this.getRefByParent(refsForm, refList);
        if (
          refList &&
          refList.length &&
          responseData.code &&
          responseData.code.length
        ) {
          for (let i = 0; i < responseData.code.length; i++) {
            let item = responseData.code[i],
              refItemError = refList.find(
                (i) => i[item.FieldName.toLowerCase()]
              );
            if (refItemError) {
              refItemError[item.FieldName.toLowerCase()].setError(
                item.ErrorText
              );
            }
          }
        }
      }
    },

    getRefByParent(refsComponent, refList, parentKey = "") {
      for (let key in refsComponent) {
        if (refsComponent.hasOwnProperty(key)) {
          let item = refsComponent[key];
          if (item && item.length) {
            item = item[0];
          }
          if (item.field && item.field.length) {
            parentKey += item.field;
            let refsChild = item.$refs;
            this.getRefByParent(refsChild, refList, parentKey);
          } else {
            let itemRef = {};
            key = parentKey + key;
            itemRef[key.toLowerCase()] = item;
            refList.push(itemRef);
          }
        }
      }
    },

    async afterCallErrorCustom() {},
    async handleCreateSubmit() {},
  },
};
</script>
