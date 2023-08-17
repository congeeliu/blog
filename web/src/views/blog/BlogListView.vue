<template>
  <div>

    <el-form :inline="true" class="demo-form-inline">
      <el-form-item label="标题">
        <el-input v-model="findCondition.title" placeholder="请输入标题关键字" size="small"></el-input>
      </el-form-item>
      <el-form-item label="内容">
        <el-input v-model="findCondition.content" placeholder="请输入内容关键字" size="small"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-search" size="mini" @click="find(true)">搜索</el-button>
        <el-button icon="el-icon-refresh" size="mini" @click="reset()">重置</el-button>

      </el-form-item>
    </el-form>

    <div style="margin-bottom: 20px;">
      <router-link to="/blog/create">
        <el-button type="primary" icon="el-icon-refresh" size="mini">新增</el-button>
      </router-link>
    </div>


    <div v-for="blog in blogs" :key="blog.id" style="margin-bottom: 10px;">
      <el-card>
        <div slot="header" class="clearfix">
          <div style="font-size: large; font-weight: bold;">
            <router-link :to="'/blog/content/' + blog.id">{{ blog.title }}</router-link>
            <div style="float: right;">
              <el-button type="danger" icon="el-icon-search" size="mini" @click="remove(blog.id)">删除</el-button>
            </div>
          </div>
          <div style="color: gray;">作者id: {{ blog.userId }}</div>
        </div>

        <div>
          <div class="card-content">{{ blog.content }}</div>
          <div style="color: gray; margin-top: 10px;">发布时间: {{ blog.createTime }}</div>
        </div>
      </el-card>
    </div>

    <div class="block" style="float: right; margin-top: 20px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
        :current-page="pageData.currentPage" :page-sizes="pageData.pageSizes" :page-size="pageData.pageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="recordsCount" background>
      </el-pagination>
    </div>

  </div>
</template>

<script>
import axios from 'axios';
// import store from '@/store/index.js'

export default {
  name: 'BlogListView',
  components: {
  },
  data() {
    return {
      blogs: [
        {
          id: 1,
          title: '标题1',
          content: '内容1',
          author: '张三',
          createTime: '2023-7-31',
        },
        {
          id: 2,
          title: '标题2',
          content: '内容2',
          author: '李四',
          createTime: '2023-7-31',
        },
      ],
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
    this.getList(true);
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

    getList(toFirstPage) {
      this.isFind = false;
      if (toFirstPage) {
        this.pageData.currentPage = 1;
      }
      // let params = {
      //   page: this.pageData.currentPage,
      //   size: this.pageData.pageSize,
      // };

      // console.log(this.$store.state.user.role);

      axios.get('/api/Blogs'
        , {
          // params: params,
          headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
        }
      ).then(res => {
        this.blogs = res.data;
        // this.tableData = res.data.scores;
        // this.recordsCount = res.data.recordsCount;
      }).catch(() => {
        // console.log(error);
        // if (error.response.statusText === 'Unauthorized') {
        //   console.log('access token过期');
        //   this.$message.error('access token过期');
        // }
      });
    },

    remove(id) {
      axios.delete('/api/Blogs/' + id
        , {
          // params: params,
          headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
        }
      ).then(() => {
        // console.log(res);
        this.$message.success('删除成功');
        this.getList(true);
      }).catch(error => {
        this.$message.error('删除失败');
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