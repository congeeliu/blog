<template>
  <div>

    <el-form label-width="80px">
      <el-form-item label="标题">
        <el-input v-model="blog.title"></el-input>
      </el-form-item>
      <el-form-item label="内容">
        <div id="editor"></div>
      </el-form-item>
      <el-button type="warning" icon="el-icon-refresh" @click="update(blog.id)">修改博客</el-button>
    </el-form>

  </div>
</template>

<script>
import axios from 'axios';
import Editor from '@toast-ui/editor';
import '@toast-ui/editor/dist/toastui-editor.css'; // Editor's Style

export default {
  name: 'UpdateBlogView',
  components: {
  },
  data() {
    return {
      blog: {
        id: 0,
        title: '标题11',
        content: '内容11',
        editor: {},
      },
    }
  },

  mounted() {
    this.getBlog();
    this.blog.editor = new Editor({
      el: document.querySelector('#editor'),
      height: '700px',
      initialEditType: 'markdown',
      previewStyle: 'vertical',
      initialValue: this.blog.content,
    });
  },

  methods: {
    getBlog() {
      const blogId = this.$route.params.id;
      axios.get('/api/Blogs/' + blogId
        , {
          id: blogId,
          headers: { Authorization: 'Bearer ' + this.$store.state.user.token },
        }
      ).then(res => {
        this.blog.id = res.data.id;
        this.blog.title = res.data.title;
        this.blog.content = res.data.content;
        // console.log(res);
      }).catch((error) => {
        console.log(error);
        // this.$message.error('获取列表失败');
      });
    },

    update(id) {
      this.blog.content = this.blog.editor.getMarkdown();
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
  },
}
</script>

<style scoped></style>