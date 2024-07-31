<template>
  <div>
    <slot name="content"></slot>
  </div>
</template>
<script>
export default {
  name: "BaseForm",
  methods: {
    /**
     * Hàm chính để xử lý quá trình gửi form
     */
    async handleSubmit() {
      this.customValidate(); // Bước 1: Tùy chỉnh Validate theo ý người dùng muốn

      let responseData = await this.customHandleLogic(); // Bước 2: Thực hiện logic tùy chỉnh và lưu kết quả phản hồi

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

    // Sẽ gọi hàm này trong tk kế thừa base này, sau đó tùy chỉnh tùy theo yêu cầu
    customValidate() {},

    async customHandleLogic() {},

    async afterCallSuccess() {},

    /**
     * hàm xử lý sau khi bị lỗi
     * @param responseData
     */
    async afterCallError(responseData) {
      // Lấy các tham chiếu đến các phần tử form
      let refsForm = this.$refs;
      if (refsForm) {
        if (responseData.code && responseData.code.length) {
          // Lặp qua các mã lỗi và hiển thị lỗi tương ứng trên các trường form
          for (let i = 0; i < responseData.code.length; i++) {
            var item = responseData.code[i];
            if (refsForm[item.FieldName]) {
              refsForm[item.FieldName].errors.push(item.ErrorText);
            }
          }
        }
      }
    },
    // gọi lại hàm này trong tk con và xử lý lỗi theo yêu cầu
    async afterCallErrorCustom() {},
    async handleCreateSubmit() {},

    /**
     * 2 function thực hiện đóng mở alert
     * @param message
     * @param action
     */
    showConfirm(message, action) {
      this.$root.alertMessage = message;
      this.$root.confirmAction = action;
      this.$root.alertVisible = true;
      this.$root.alertIsConfirm = true;
      this.$root.alertIsShow = true;
    },
    showAlert(message, action) {
      this.$root.alertMessage = message;
      this.$root.confirmAction = action;
      this.$root.alertVisible = true;
      this.$root.alertIsConfirm = true;
      this.$root.alertIsShow = false;
    },
    handleConfirm() {
      if (this.confirmAction) {
        this.confirmAction();
      }
      this.alertVisible = false;
    },
  },
};
</script>

<style scoped></style>
