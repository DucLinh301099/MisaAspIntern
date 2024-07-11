import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import './assets/styles.css';
import '@fortawesome/fontawesome-free/css/all.css';

const app = createApp(App);

// Sử dụng router trong ứng dụng
app.use(router);

// Mount ứng dụng vào phần tử với id 'app'
app.mount('#app');
