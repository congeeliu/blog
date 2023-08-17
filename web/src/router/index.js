import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store/index'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Index',
    component: () => import('@/views/blog/BlogListView.vue'),
    meta: {
      requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的
    },
  },
  {
    path: '/blogs',
    name: 'BlogList',
    component: () => import('@/views/blog/BlogListView.vue'),
    meta: {
      requireAuth: true,
    },
  },
  {
    path: '/blog/content/:id',
    name: 'Blog',
    component: () => import('@/views/blog/BlogView.vue'),
    meta: {
      requireAuth: true,
    },
  },
  {
    path: '/blog/create',
    name: 'CreateBlog',
    component: () => import('@/views/blog/CreateBlogView.vue'),
    meta: {
      requireAuth: true,
    },
  },
  {
    path: '/blog/update/:id',
    name: 'UpdateBlog',
    component: () => import('@/views/blog/UpdateBlogView.vue'),
    meta: {
      requireAuth: true,
    },
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/user/LoginView.vue'),
    meta: {
      requireAuth: false,
    },
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('@/views/user/RegisterView.vue'),
    meta: {
      requireAuth: false,
    },
  },
]

const router = new VueRouter({
  mode: 'history',
  routes
});

router.beforeEach((to, from, next) => {
  if (to.path === '/login') next();
  else {
    const jwtToken = localStorage.getItem('token');
    if (jwtToken) {
      store.commit('updateToken', jwtToken);
      store.dispatch('getInfo');
      next();
    } else if (to.meta.requireAuth) {  // 判断该路由是否需要登录权限
      if (store.state.user.token) {  // 通过vuex state获取当前的token是否存在
        next();
      } else {
        next({ name: 'Login' });
      }
    } else {
      next();
    }
  }

});

export default router
