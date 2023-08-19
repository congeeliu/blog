<template>
  <div>
    <el-form label-width="80px">
      <el-form-item label="标题">
        <el-input v-model="blog.title"></el-input>
      </el-form-item>
      <el-form-item label="内容">
        <div id="editor"></div>
      </el-form-item>
      <el-button type="primary" icon="el-icon-refresh" @click="create()">发布博客</el-button>
    </el-form>
  </div>
</template>

<script>
import axios from 'axios';
import Editor from '@toast-ui/editor';
import '@toast-ui/editor/dist/toastui-editor.css'; // Editor's Style

export default {
  name: 'CreateBlogView',
  components: {
  },
  data() {
    return {
      blog: {
        title: '',
        content: '',
        editor: {}
      },
    }
  },

  mounted() {
    this.blog.editor = new Editor({
      el: document.querySelector('#editor'),
      height: '700px',
      initialEditType: 'markdown',
      previewStyle: 'vertical'
    });
  },

  methods: {
    create() {
      const titleLength = this.blog.title.length;
      const contentLength = this.blog.editor.getMarkdown().length;
      if (titleLength < 5 || titleLength > 100) {
        this.$message.error('标题长度应在5~100个字之间');
        return;
      }
      if (contentLength === 0) {
        this.$message.error('博客内容不能为空');
        return;
      }

      this.blog.content = this.blog.editor.getMarkdown();
      axios.post('/api/Blogs', {
        userId: this.$store.state.user.id,
        title: this.blog.title,
        content: this.blog.content,
      }, {
        headers: { Authorization: 'Bearer ' + this.$store.state.user.token }
      }).then((res) => {
        console.log(res);
        this.$router.push({ name: 'BlogList' });
      }).catch(() => {
      });
    },
  },
}
</script>

<style scoped></style>