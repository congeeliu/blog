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
          <div class="card-content">{{ markdownSummary(blog.content) }}</div>
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

      axios.get('/api/Blogs', {
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
      axios.delete('/api/Blogs/' + id, {
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

    markdownSummary(content) {
      var marked = require('marked-ast');
      // var ast = marked.parse(str);
      // return ast;
      const len = content.length
      if (content) {
        content = content.split('\n')
        const textArr = []
        for (let i = 0; i < content.length; i++) {
          const text = content[i].trim()
          if (text) {
            textArr.push(text)
          }
        }
        content = textArr.join('\n')
        const ast = marked.parse(content.trim())
        const str = this.parseMarkAst(ast).slice(0, len)
        return str
      }
      return ''
    },

    parseMarkAst(ast) {
      let str = ''
      for (let i = 0; i < ast.length; i++) {
        const curAst = ast[i]
        if (curAst.type === 'heading' || curAst.type === 'paragraph' || curAst.type === 'strong' || curAst.type === 'em') {
          if (curAst.text.length === 1) {
            if (curAst.text !== '/n') {
              str += curAst.text[0]
            }
          } else {
            for (let y = 0; y < curAst.text.length; y++) {
              const yAst = curAst.text[y]
              if (typeof yAst === 'object') {
                str += yAst.text[0].trim()
              } else {
                str += yAst.trim()
              }
            }
          }
        }
      }
      return str
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

    reset() {
      this.findCondition.title = "";
      this.findCondition.content = "";
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