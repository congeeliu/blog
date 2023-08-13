// import axios from "axios";
// import router from "@/router/index";

export default ({
  state: {
    id: "",
    username: "",
    token: '',
    isLogin: false,
  },
  mutations: {
    updateToken(state, token) {
      state.token = token;
    },
    updateIsLogin(state, isLogin) {
      state.isLogin = isLogin;
    },
    logout(state) {
      state.id = "";
      state.username = "";
      state.token = "";
      state.isLogin = false;
    },
  },
  actions: {
  },
  modules: {
  }
})
