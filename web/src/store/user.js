import axios from "axios";
import router from "@/router/index";

export default ({
  state: {
    id: '',
    username: '',
    role: '',
    token: '',
    refreshToken: '',
    isLogin: false,
  },
  mutations: {
    updateToken(state, token) {
      state.token = token;
    },
    updateRefreshToken(state, refreshToken) {
      state.refreshToken = refreshToken;
    },
    updateUser(state, user) {
      state.id = user.id;
      state.username = user.username;
      state.role = user.role;
      state.isLogin = true;
    },
    logout(state) {
      state.id = '';
      state.username = '';
      state.token = '';
      state.isLogin = false;
    },
  },
  actions: {
    login(context, user) {
      axios.post('/api/Users/login', {
        username: user.username,
        password: user.password,
      }).then((res) => {
        // console.log(res.data);
        context.commit('updateToken', res.data);
        // context.commit('updateRefreshToken', res.data.refreshToken);
        localStorage.setItem('token', res.data);
        // localStorage.setItem('refreshToken', res.data.refreshToken);
        context.dispatch('getInfo');
        router.push({ name: 'Index' });
      }).catch((error) => {
        console.log(error);
      });
    },

    getInfo(context) {
      axios.get('/api/Users/info', {
        headers: { Authorization: 'Bearer ' + context.state.token },
      }).then((res) => {
        // console.log(res.data);
        context.commit('updateUser', res.data);
      }).catch((error) => {
        console.log(error);
      });
    }
  },
  modules: {
  }
})
