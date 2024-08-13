<script>
import BaseForm from "./BaseForm.vue";

export default {
  extends: BaseForm,
  data() {
    return {
      isDisabled: false,
      mode: "view",
    };
  },
  methods: {
    async handleSubmit(action) {
      this.customValidate();

      let responseData = await this.customHandleLogic(action);

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

    async afterCallSuccess(responseData) {
      if (this.isEditMode) {
        this.setMode("view");
      }
    },

    /**
 * Hàm xử lý lỗi sau khi gọi API, tìm và hiển thị lỗi lên các form field tương ứng

 * @param responseData 
 */
    async afterCallError(responseData) {
      let refsForm = this.$refs;

      if (refsForm) {
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

    /**
     * Đệ quy tìm và lưu tất cả các refs từ component hiện tại và các component con
     * @param refsComponent
     * @param refList
     * @param parentKey
     */
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
