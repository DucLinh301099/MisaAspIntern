<template>
  <div id="app">
    <MSAlert
      :message="alertMessage"
      :type="alertType"
      :visible="alertVisible"
      :isConfirm="alertIsConfirm"
      :isShow="alertIsShow"
      @confirm="handleConfirm"
      @cancel="alertVisible = false"
    />
    <router-view />
  </div>
</template>

<script>
import MSAlert from "./components/Base/MSAlert.vue";
export default {
  name: "App",
  components: {
    MSAlert,
  },
  data() {
    return {
      alertMessage: "",
      alertVisible: false,
      alertIsConfirm: false,
      alertIsShow: false,
      confirmAction: null,
      alertType: "",
    };
  },
  methods: {
    handleConfirm() {
      if (this.confirmAction) {
        this.confirmAction();
      }
      this.alertVisible = false;
    },
  },
};
</script>

<style>
/* Add your global styles here */
</style>
