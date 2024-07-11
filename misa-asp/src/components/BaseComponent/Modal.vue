<template>
  <div class="modal" v-if="visible">
    <div class="modal-overlay" @click="close"></div>
    <div class="modal-content">
      <button class="modal-close" @click="close">&times;</button>
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "Modal",
  props: {
    visible: {
      type: Boolean,
      required: true,
    },
  },
  methods: {
    close() {
      this.$emit("close");
    },
  },
};
</script>

<style scoped>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  background: rgba(0, 0, 0, 0.5);
  animation: fadeIn 0.3s ease;
}

.modal-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.modal-content {
  position: relative;
  background: #ffffff;
  padding: 0px 5px 25px 5px;
  max-width: 600px;
  max-height: 450px;
  width: 100%;

  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  animation: slideIn-fc2a14a2 0.3s ease;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.modal-close {
  position: absolute;
  top: 15px;
  right: 15px;
  background: none;
  border: none;
  font-size: 24px;
  cursor: pointer;
  scale: 1.7;
}

/* Animation keyframes */
@keyframes fadeIn {
  from {
    background: rgba(0, 0, 0, 0);
  }
  to {
    background: rgba(0, 0, 0, 0.5);
  }
}

@keyframes slideIn {
  from {
    transform: translateY(-20%);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}
</style>
