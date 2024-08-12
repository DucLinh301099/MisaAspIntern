<script>
import { baseApi } from "../../api/baseApi";
import BaseForm from "./BaseForm.vue";

export default {
  extends: BaseForm,
  data() {
    return {
      isDisabled: false, // Thêm biến trạng thái
      mode: "view",
    };
  },
  methods: {
    async handleSubmit(action) {
      this.customValidate();

      let responseData;

      try {
        if (this.isAddMode) {
          // Gọi API POST để tạo mới
          responseData = await baseApi.postAuthenApi(
            this.createApiUrl,
            this.currentItem
          );
        } else if (this.isEditMode) {
          // Gọi API PUT để cập nhật

          responseData = await baseApi.putAuthenApi(
            this.updateApiUrl,
            this.currentItem
          );
          if (responseData.isSuccess) {
            this.showAlert("Cập nhật thành công", () => {});
          }
        }
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
    },
    setMode(newMode) {
      this.mode = newMode;
      this.isDisabled = newMode === "view";
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
