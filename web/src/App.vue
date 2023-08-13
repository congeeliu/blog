<template>
  <div>
    <NavMenu v-if="this.$store.state.user.token" />
    <el-container>
      <el-main>
        <div style="margin: 0 15vw;">
          <el-card>
            <router-view />
          </el-card>
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import NavMenu from './components/NavMenu.vue';

export default {
  name: 'App',
  components: {
    NavMenu,
  }
}
</script>

<style>
body {
  margin: 0;
}

/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
} */
</style>
