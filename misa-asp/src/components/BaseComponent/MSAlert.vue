<template>
  <transition name="fade">
    <div v-if="visible" class="alert-box" :class="type">
      <div class="alert-content">
        <span class="alert-message">{{ message }}</span>
      </div>
      <div v-if="isConfirm" class="comfirm-buttons">
        <button class="comfirm-button-ms" @click="$emit('confirm')">
          Xác nhận
        </button>
        <button v-if="isShow" class="cancel-button-ms" @click="$emit('cancel')">
          Hủy
        </button>
      </div>
    </div>
  </transition>
</template>

<script>
export default {
  name: "MSAlert",
  props: {
    message: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      required: true,
    },
    visible: {
      type: Boolean,
      required: true,
    },
    isConfirm: {
      type: Boolean,
      required: true,
    },
    isShow: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
.cancel-button-ms,
.comfirm-button-ms {
  font-size: 14px;
  font-weight: bold;
  text-align: center;
  width: 100px;
}
.alert-box {
  position: fixed;
  top: 50px;
  left: 50%;
  transform: translateX(-50%);
  width: 300px;
  border-left: 5px solid;
  border-radius: 5px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  padding: 20px;
  background-color: white;
  z-index: 1000;
  font-size: 18px; /* Larger font size */
}

.alert-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.alert-message {
  flex: 1;
  margin-right: 10px;
}

.comfirm-button-ms,
.cancel-button-ms {
  padding: 5px 10px;
  border: none;
  border-radius: 3px;
  cursor: pointer;
}

.close-button {
  background-color: #ccc;
  scale: 1.2;
}

.comfirm-buttons {
  display: flex;
  justify-content: flex-end;
  margin-top: 10px;
}

.comfirm-button-ms {
  background-color: #28a745;
  color: white;
  margin-right: 10px;
}

.cancel-button-ms {
  background-color: #dc3545;
  color: white;
}
.cancel-button-ms:hover {
  background-color: rgb(175, 14, 14);
}
.comfirm-button-ms:hover {
  background-color: green;
}
.alert-box.info {
  border-color: #28a745;
}

.alert-box.success {
  border-color: #28a745;
}

.alert-box.error {
  border-color: #dc3545;
}
</style>
