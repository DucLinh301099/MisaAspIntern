<template>
  <transition name="fade">
    <div v-if="visible" class="alert-box" :class="type">
      <div class="alert-content">
        <span class="alert-message">{{ message }}</span>
        <button v-if="isClose" class="close-button" @click="$emit('close')">
          X
        </button>
      </div>
      <div v-if="isConfirm" class="confirm-buttons">
        <button class="confirm-button" @click="$emit('confirm')">
          Xác nhận
        </button>
        <button class="cancel-button" @click="$emit('cancel')">Hủy</button>
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
    isClose: {
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

.close-button,
.confirm-button,
.cancel-button {
  padding: 5px 10px;
  border: none;
  border-radius: 3px;
  cursor: pointer;
}

.close-button {
  background-color: #ccc;
  scale: 1.2;
}

.confirm-buttons {
  display: flex;
  justify-content: flex-end;
  margin-top: 10px;
}

.confirm-button {
  background-color: #28a745;
  color: white;
  margin-right: 10px;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
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
