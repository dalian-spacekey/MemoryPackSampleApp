import colors from 'vuetify/es5/util/colors';

export default {
  srcDir: 'src',
  ssr: false,
  env: {
    APP_NAME: require('./package.json').name,
    APP_TITLE: require('./package.json').description,
    VERSION: require('./package.json').version
  },
  head: {
    title: require('./package.json').description,
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },
  css: [],
  plugins: [
    { src: '~/plugins/gateway-accessor' }
  ],
  buildModules: [
    '@nuxt/typescript-build',
    '@nuxtjs/vuetify'
  ],
  modules: [
    '@nuxtjs/axios'
  ],
  axios: {
  },
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    treeShake: true,
    theme: {
      dark: false,
      themes: {
        light: {
          primary: '#045D56',
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3
        },
        dark: {
          primary: '#045D56',
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3
        }
      }
    }
  },
  build: {
    extend (config, ctx) {
      config.devtool = ctx.isClient ? 'source-map' : 'inline-source-map';
    }
  }
};
