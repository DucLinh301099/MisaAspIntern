<template>
  <Header />
  <div id="app" class="app-container">
    <div class="main-content">
      <Section />
    </div>
  </div>

  <ContactComponent />

  <Footer />
</template>

<script>
import Header from "../components/HomePage/Header.vue";

import ContactComponent from "../components/HomePage/ContactComponent.vue";
import Footer from "../components/HomePage/Footer.vue";
import Section from "../components/HomePage/Section.vue";

export default {
  name: "UserAccount",
  components: {
    Header,
    Section,
    ContactComponent,
    Footer,
  },
};
</script>

<style scoped>
.app-container {
  padding-top: 80px;
  /* Adjust this value based on the height of your header */
}
.main-contain {
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}

/* Global styles for the Home view */
</style>
