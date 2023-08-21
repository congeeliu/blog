<template>
  <div style="width: 400px; margin: 0 auto;">
    <el-form label-position="right" label-width="80px" :model="user">
      <el-form-item label="账号">
        <el-input v-model="user.username"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="user.password"></el-input>
      </el-form-item>
      <div>
        <div class="center">
          <el-button type="primary" @click="register()">注册</el-button>
        </div>
      </div>


    </el-form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'RegisterView',
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
    register() {
      axios.post('/api/Users', {
        username: this.user.username,
        password: this.user.password,
      }
      ).then(res => {
        console.log(res);
        this.$router.push({ name: 'BlogList' });
      }).catch((error) => {
        console.log(error);
      });
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