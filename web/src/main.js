import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';

Vue.use(ElementUI);
axios.defaults.baseURL = "http://localhost:5056";

Vue.config.productionTip = false

let vue = new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')


// 添加响应拦截器
axios.interceptors.response.use(function (response) {
  // 2xx 范围内的状态码都会触发该函数。
  // 对响应数据做点什么
  return response;
}, function (error) {
  // 超出 2xx 范围的状态码都会触发该函数。
  // 对响应错误做点什么
  if (error.response.statusText === 'Unauthorized') {
    console.log('access token过期, 拦截成功！');
    // message.error('access token过期');
    if (vue.$route.name !== 'Login') {
      store.commit('logout');
      router.push({ name: 'Login' });
    }
  }
  return Promise.reject(error);
});
