<template>
  <div class="attach-file">
    <div class="attach-header">
      <span class="attach-icon">📎</span>
      <span class="attach-title">Đính kèm</span>
      <span class="attach-info">Dung lượng tối đa 5MB</span>
    </div>
    <div class="attach-dropzone" @click="triggerFileInput">
      Kéo/thả tệp vào đây hoặc bấm vào đây
      <input
        type="file"
        @change="handleFileUpload"
        multiple
        hidden
        ref="fileInput"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "AttachFile",
  methods: {
    triggerFileInput() {
      this.$refs.fileInput.click();
    },
    handleFileUpload(event) {
      const files = event.target.files;
      console.log(files);
    },
  },
};
</script>

<style scoped>
.attach-file {
  padding: 20px;
}

.attach-header {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.attach-icon {
  font-size: 24px;
  margin-right: 8px;
}

.attach-title {
  font-weight: bold;
  margin-right: 8px;
}

.attach-info {
  color: #888;
}

.attach-dropzone {
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 20px;
  text-align: center;
  cursor: pointer;
  color: #888;
  width: 300px;
}

.attach-dropzone:hover {
  border-color: #000;
  color: #000;
}
</style>
