<template>
  <div style="width: 400px; margin: 0 auto;">
    <el-form label-position="right" label-width="80px" :model="user">
      <el-form-item label="账号">
        <el-input v-model="user.account"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="user.password"></el-input>
      </el-form-item>
      <div>
        <div class="center">
          <el-button type="primary">注册</el-button>
        </div>
      </div>


    </el-form>
  </div>
</template>

<script>
// import axios from 'axios';
// import store from '@/store/index.js'

export default {
  name: 'RegisterView',
  components: {
  },
  data() {
    return {
      user: {
        account: '',
        password: '',
      },
      rules: {
      }
    }
  },

  mounted() {
  },

  methods: {
  },



}
</script>

<style scoped>
.center {
  display: flex;
  justify-content: center;
}
</style>