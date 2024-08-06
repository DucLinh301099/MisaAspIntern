<script>
import { baseApi } from "../../api/baseApi";
import BaseForm from "./BaseForm.vue";

export default {
  extends: BaseForm,
  data() {
    return {
      isDisabled: false, // Thêm biến trạng thái
      isEditMode: false, // Trạng thái chế độ chỉnh sửa
    };
  },
  methods: {
    async handleSubmit(action) {
      if (action === "cancel" || action === "close") {
        // xử lý cho đóng hoặc hủy
        this.showConfirm("Bạn chắc chắn muốn hủy!", () => {
          if (action === "cancel") {
            this.$router.push("/admin");
          }
        });
      } else if (action === "edit" || action === "unsaveAndEdit") {
        this.isDisabled = false; // Cho phép chỉnh sửa
        this.isEditMode = false; // Chuyển lại về chế độ lưu
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

        if (action === "save" && responseData.isSuccess) {
          this.showAlert("Ghi sổ thành công", () => {
            this.isDisabled = true;
            this.isEditMode = true; // Chuyển sang chế độ chỉnh sửa
          });
        }

        if (action === "saveAndClose" && responseData.isSuccess) {
          this.showAlert("Ghi sổ thành công", () => {
            this.$router.push("/admin");
          });
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
