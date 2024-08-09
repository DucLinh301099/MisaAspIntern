import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import MSInput from "./components/Base/MSInput.vue";
import './assets/styles.css';
import '@fortawesome/fontawesome-free/css/all.css';

import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { faPaperclip } from '@fortawesome/free-solid-svg-icons';
library.add(faPaperclip);

const app = createApp(App);

// Sử dụng router trong ứng dụng
app.use(router);

app.component('MSInput', MSInput);
app.component('font-awesome-icon', FontAwesomeIcon);

// Mount ứng dụng vào phần tử với id 'app'
app.mount('#app');
