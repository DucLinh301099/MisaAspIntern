<template>
  <div class="attach-file">
    <div class="attach-header">
      <font-awesome-icon :icon="['fas', 'paperclip']" class="attach-icon" />
      <span class="attach-title">Đính kèm</span>
      <span class="attach-info">Dung lượng tối đa 5MB</span>
    </div>
    <div class="attach-dropzone" @click="triggerFileInput">
      <p>Kéo/thả tệp vào đây hoặc bấm vào đây</p>
      <input
        type="file"
        @change="handleFileUpload"
        multiple
        hidden
        ref="fileInput"
        :disabled="disabled"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "AttachFile",
  props: {
    disabled: {
      type: Boolean,
      default: false,
    },
  },
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
  font-size: 14px;
}

.attach-icon {
  font-size: 20px;
  margin-right: 8px;
  color: #555; /* Adjust color intensity here */
  opacity: 0.6; /* Adjust opacity for more subtle effect */
}

.attach-title {
  font-weight: bold;
  margin-right: 8px;
  color: #333; /* Adjust color intensity here */
}

.attach-info {
  color: #888;
}
p {
  font-size: 14px;
}
.attach-dropzone {
  border: 1px solid #ccc;
  border-radius: 2.5px;
  padding: 5px;
  text-align: center;
  cursor: pointer;
  color: #888;
  width: 350px;
  font-size: 16px;
}

/* .attach-dropzone:hover {
  border-color: #000;
  color: #000;
} */
</style>
