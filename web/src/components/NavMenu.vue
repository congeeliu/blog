<template>
  <el-menu default-active="" class="el-menu" mode="horizontal" background-color="#545c64" text-color="#fff"
    active-text-color="#ffd04b" router>
    <el-menu-item index="/blogs">博客</el-menu-item>
    <el-submenu index="/user" v-if="this.$store.state.user.isLogin">
      <template slot="title">用户</template>
      <el-menu-item index="/user/info">个人中心</el-menu-item>
      <el-menu-item index="/user/myblog">我的博客</el-menu-item>
      <el-menu-item @click="logout()">退出</el-menu-item>
    </el-submenu>
    <el-menu-item index="/login" v-if="!this.$store.state.user.isLogin">登录</el-menu-item>
    <el-menu-item index="/register" v-if="!this.$store.state.user.isLogin">注册</el-menu-item>


  </el-menu>
</template>

<script>
// import router from '../router';

export default {
  name: 'NavMenu',
  data() {
    return {
      activeIndex: '1',
    };
  },
  methods: {
    logout() {
      this.$store.commit('logout');
      localStorage.removeItem('token');
      this.$router.push('Login');
    }
  }
}
</script>

<style scoped>
.el-menu {
  display: flex;
  justify-content: center;
}
</style>