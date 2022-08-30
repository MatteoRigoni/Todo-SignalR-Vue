import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap';
 import 'bootstrap/dist/css/bootstrap.min.css';
import { ConnectionServices } from './services/toDoService';

Vue.config.productionTip = false

Vue.use(ConnectionServices);

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
