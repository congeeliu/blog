<template>
  <div>
    <el-card style="margin-top: 20px;">
      <div slot="header" class="clearfix">基本信息
      </div>

      <div class="card-content">
        <el-form label-width="80px">
          <el-form-item label="账号">
            <el-input v-model="info.username"></el-input>
          </el-form-item>
          <el-form-item label="性别">
            <el-input v-model="info.sex"></el-input>
          </el-form-item>
          <el-form-item label="邮箱">
            <el-input v-model="info.email"></el-input>
          </el-form-item>
          <el-form-item label="电话">
            <el-input v-model="info.telephone"></el-input>
          </el-form-item>
          <el-form-item label="所在地">
            <el-input v-model="info.address"></el-input>
          </el-form-item>
        </el-form>
      </div>
    </el-card>

  </div>
</template>

<script>
// import axios from 'axios';

export default {
  name: 'BlogView',
  components: {
  },
  data() {
    return {
      info: {
        username: '',
        sex: '武装直升机',
        email: 'email@163.com',
        telephone: '13205181111',
        address: '中国',
      },
      comments: [],
      comment: {
        content: '',
      }
    }
  },

  mounted() {
    this.getInfo();
  },

  methods: {
    getInfo() {
      this.info.username = this.$store.state.user.username;
      // const blogId = this.$route.params.id;
      // axios.get('/api/Blogs/' + blogId, {
      //   id: blogId,
      //   headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
      // }
      // ).then(res => {
      //   this.blog.id = res.data.id;
      //   // console.log(this.blog.content);
      //   // console.log(res);
      // }).catch((error) => {
      //   console.log(error);
      //   // this.$message.error('获取列表失败');
      // });
    },
  },
}
</script>

<style scoped></style>