<template>
  <div>

    <el-card>
      <div slot="header" class="clearfix">
        <div style="font-size: large; font-weight: bold;">{{ blog.title }}</div>
        <div style="color: gray;">作者id: {{ blog.userId }}</div>
      </div>

      <div>
        <div class="card-content">{{ blog.content }}</div>
        <div style="color: gray; margin-top: 10px;">发布时间: {{ blog.createTime }}</div>
      </div>
    </el-card>

  </div>
</template>

<script>
import axios from 'axios';
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
        content: '内容1',
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
          //   // headers: { Authorization: 'Bearer ' + store.state.user.token },
        }
      ).then(res => {
        this.blog = res.data;
        // console.log(res);
        // this.tableData = res.data.scores;
        // this.tableDataBackup = res.data.scores;
        // this.recordsCount = res.data.recordsCount;
      }).catch(() => {
        // console.log(error);
        // this.$message.error('获取列表失败');
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