<template>
  <div>

    <el-card>
      <div slot="header" class="clearfix">
        <div style="font-size: large; font-weight: bold;">{{ blog.title }}</div>
        <div style="float: right;">
          <router-link :to="'/blog/update/' + blog.id">
            <el-button type="warning" icon="el-icon-search" size="mini">修改</el-button>
          </router-link>
        </div>
        <div style="color: gray;">作者id: {{ blog.userId }}</div>
      </div>

      <div>
        <div class="card-content">
          <!-- {{ blog.content }} -->
          <div id="viewer"></div>
        </div>
        <div style="color: gray; margin-top: 10px;">发布时间: {{ blog.createTime }}</div>
      </div>
    </el-card>

    <el-card style="margin-top: 20px;">
      <div slot="header" class="clearfix">评论区
        <div style="float: right;">

        </div>
      </div>

      <div class="card-content">
        <el-form label-width="40px">
          <el-form-item label="评论">
            <el-input v-model="comment.content" type="textarea" :rows="4"></el-input>
            <el-button type="primary" icon="el-icon-search" size="mini" @click="writeComment">发布</el-button>
          </el-form-item>
        </el-form>
        <div v-for="comment in comments" :key="comment.id">
          {{ comment.author + ": " + comment.content }}
        </div>
      </div>
    </el-card>

  </div>
</template>

<script>
import axios from 'axios';
// import Editor from '@toast-ui/editor';
import '@toast-ui/editor/dist/toastui-editor.css'; // Editor's Style
import Viewer from '@toast-ui/editor/dist/toastui-editor-viewer';

export default {
  name: 'BlogView',
  components: {
  },
  data() {
    return {
      blog: {
        id: 1,
        title: '标题11',
        content: 'use `getHtml()` and `getMarkdown()` of the [Editor](https://github.com/nhn/tui.editor).',
        author: '张三',
        createTime: '2023-7-31',
        userId: 1,
        viewer: {},
      },
      comments: [],
      comment: {
        content: '',
      }
    }
  },

  mounted() {
    this.getBlog();
    this.getComments();
    this.blog.viewer = new Viewer({
      el: document.querySelector('#viewer'),
      height: '700px',
    });
    this.blog.viewer.setMarkdown(this.blog.content);
    // console.log(this.blog.content);
  },

  methods: {
    getBlog() {
      const blogId = this.$route.params.id;
      axios.get('/api/Blogs/' + blogId, {
        id: blogId,
        // headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
      }
      ).then(res => {
        this.blog.id = res.data.id;
        this.blog.title = res.data.title;
        this.blog.content = res.data.content;
        this.blog.userId = res.data.userId;
        this.blog.createTime = res.data.createTime;
        this.blog.viewer.setMarkdown(this.blog.content);
        // console.log(this.blog.content);
        // console.log(res);
      }).catch((error) => {
        console.log(error);
        // this.$message.error('获取列表失败');
      });
    },

    getComments() {
      const blogId = this.$route.params.id;
      axios.get('/api/Comments/blog/' + blogId, {
        headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
      }
      ).then(res => {
        this.comments = res.data;
        // console.log(res);
      }).catch((error) => {
        console.log(error);
        // this.$message.error('获取列表失败');
      });
    },

    writeComment() {
      axios.post('/api/Comments', {
        blogId: this.$route.params.id,
        content: this.comment.content,
        author: this.$store.state.user.username,
        parentId: -1,
        headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
      }
      ).then(res => {
        this.getComments();
        console.log(res);
      }).catch((error) => {
        console.log(error);
        // this.$message.error('获取列表失败');
      });
    },
  },
}
</script>

<style scoped></style>