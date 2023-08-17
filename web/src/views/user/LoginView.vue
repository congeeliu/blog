<template>
  <div style="width: 400px; margin: 0 auto;">
    <el-form label-position="right" label-width="80px" :model="user">
      <el-form-item label="用户名">
        <el-input v-model="user.username"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="user.password"></el-input>
      </el-form-item>
      <div>
        <div class="center">
          <el-button type="primary" @click="login()">登录</el-button>
        </div>
      </div>


    </el-form>
  </div>
</template>

<script>
// import axios from 'axios';

export default {
  name: 'LoginView',
  components: {
  },
  data() {
    return {
      user: {
        username: '',
        password: '',
      },
      rules: {
      }
    }
  },

  mounted() {
  },

  methods: {
    login() {
      this.$store.dispatch('login', this.user);
    },
  },



}
</script>

<style scoped>
.center {
  display: flex;
  justify-content: center;
}
</style>