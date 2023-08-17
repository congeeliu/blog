<template>
  <el-menu :default-active="activeIndex" class="el-menu" mode="horizontal" background-color="#545c64" text-color="#fff"
    active-text-color="#ffd04b" router>
    <el-menu-item index="/blogs">博客</el-menu-item>
    <el-submenu index="/user">
      <template slot="title">用户</template>
      <el-menu-item index="">个人中心</el-menu-item>
      <el-menu-item index="" @click="logout()">退出</el-menu-item>
      <!-- <el-menu-item index="2-3">选项3</el-menu-item> -->
      <!-- <el-submenu index="2-4">
        <template slot="title">选项4</template>
        <el-menu-item index="2-4-1">选项1</el-menu-item>
        <el-menu-item index="2-4-2">选项2</el-menu-item>
        <el-menu-item index="2-4-3">选项3</el-menu-item>
      </el-submenu> -->
    </el-submenu>
    <!-- <router-link :to="{ name: 'Login' }"> -->
    <el-menu-item index="/login">登录</el-menu-item>
    <!-- </router-link> -->
    <el-menu-item index="/register" route>注册</el-menu-item>

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