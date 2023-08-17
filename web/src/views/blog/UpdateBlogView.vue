<template>
  <div>

    <el-form label-width="80px">
      <el-form-item label="标题">
        <el-input v-model="blog.title"></el-input>
      </el-form-item>
      <el-form-item label="内容">
        <el-input v-model="blog.content" type="textarea" :autosize="{ minRows: 20, maxRows: 40 }"></el-input>
      </el-form-item>
      <el-button type="warning" icon="el-icon-refresh" @click="update(blog.id)">修改博客</el-button>
    </el-form>

  </div>
</template>

<script>
import axios from 'axios';
// import router from '../../router';
// import store from '@/store/index.js'

export default {
  name: 'BlogView',
  components: {
  },
  data() {
    return {
      blog: {
        id: 1,
        title: '标题11',
        content: '内容11',
        author: '张三',
        createTime: '2023-7-31',
      },
      tableData: [],
      tableDataBackup: [],
      recordsCount: 0,
      pageData: {
        pageSize: 10,
        pageSizes: [10, 20, 30, 40],
        currentPage: 1
      },
      findCondition: {
        title: '',
        content: '',
      },
      isFind: false,
      labelPosition: 'right',
      rules: {
      }
    }
  },

  mounted() {
    this.getBlog(true);
    // console.log(this.$route.params.id);
  },

  methods: {
    handleSizeChange(size) {
      this.pageData.pageSize = size;
      if (!this.isFind) {
        this.getList(false);
      } else {
        this.find(false);
      }
    },

    handleCurrentChange(page) {
      this.pageData.currentPage = page;
      if (!this.isFind) {
        this.getList(false);
      } else {
        this.find(false);
      }
    },

    getBlog(toFirstPage) {
      this.isFind = false;
      if (toFirstPage) {
        this.pageData.currentPage = 1;
      }
      // let params = {
      //   page: this.pageData.currentPage,
      //   size: this.pageData.pageSize,
      // };
      let blogId = this.$route.params.id;

      axios.get('/api/Blogs/' + blogId
        , {
          id: blogId,
          headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
        }
      ).then(res => {
        this.blog = res.data;
        // console.log(res);
        // this.tableData = res.data.scores;
        // this.recordsCount = res.data.recordsCount;
      }).catch((error) => {
        console.log(error);
        // this.$message.error('获取列表失败');
      });
    },

    update(id) {
      axios.put('/api/Blogs/' + id, {
        id: id,
        userId: this.$store.state.user.id,
        title: this.blog.title,
        content: this.blog.content,
      }, {
        headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
      }
      ).then(() => {
        // console.log(res);
        this.$message.success('修改成功');
        this.$router.push({ name: 'BlogList' });
      }).catch(error => {
        this.$message.error('修改失败');
        console.log(error);
      });
    },

    // find(toFirstPage) {
    //   this.isFind = true;
    //   this.tableData = this.tableDataBackup;
    //   if (toFirstPage) {
    //     this.pageData.currentPage = 1;
    //   }

    //   if (this.findCondition.courseName !== '') {
    //     this.tableData = this.tableData.filter(score => score.courseName.includes(this.findCondition.courseName));
    //   }
    //   if (this.findCondition.courseTerm !== '') {
    //     this.tableData = this.tableData.filter(score => score.courseTerm.includes(this.findCondition.courseTerm));
    //   }

    // },

    // openDialog(row, title) {
    //   this.dialogForm = { ...row };
    //   this.dialogForm.title = title;
    //   this.dialogFormVisible = true;
    // },

    // updateScore() {
    //   let legal = true;
    //   this.$refs['dialogForm'].validate((valid) => {
    //     if (!valid) legal = false;
    //   });
    //   if (!legal) {
    //     this.$message.error('成绩格式不合法');
    //     return;
    //   }

    //   let params = {
    //     id: this.dialogForm.id,
    //     score: this.dialogForm.score,
    //   };
    //   axios.post('/score/update', params, {
    //     // headers: { Authorization: 'Bearer ' + store.state.user.token },
    //   }).then(() => {
    //     this.getList(true);
    //     this.dialogFormVisible = false;
    //     this.$message.success(this.dialogForm.title + '成功');
    //   }).catch(() => {
    //     // console.log(error);
    //     this.$message.success(this.dialogForm.title + '失败');
    //   });
    // },

    reset() {
      this.findCondition.courseName = "";
      this.findCondition.courseTerm = "";
      this.getList(true);
    }
  },



}
</script>

<style scoped>
.card-content {
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 3;
  overflow: hidden;
}
</style>