import Vue from 'vue'
import Vuex from 'vuex'
import ModuleUser from './user'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    user: ModuleUser
  }
})
