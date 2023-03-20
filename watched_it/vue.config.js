const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  pwa: {
    name: 'Watched It',
    themeColor: '#B29056',
    msTileColor: '#000000',
    appleMobileWebAppCapable: 'yes',
    appleMobileWebAppStatusBarStyle: 'black',

    // configure the workbox plugin
    workboxPluginMode: 'InjectManifest',
    workboxOptions: {
      // swSrc is required in InjectManifest mode.
      swSrc: './src/service-worker.js',
      exclude: [
        ".htaccess"
      ]
      // ...other Workbox options...
    },
    manifestOptions: {
      icons: [
        {
          "src": "img/icons/manifest-icon-192.maskable.png",
          "sizes": "192x192",
          "type": "image/png",
          "purpose": "any"
        },
        {
          "src": "img/icons/manifest-icon-192.maskable.png",
          "sizes": "192x192",
          "type": "image/png",
          "purpose": "maskable"
        },
        {
          "src": "img/icons/manifest-icon-512.maskable.png",
          "sizes": "512x512",
          "type": "image/png",
          "purpose": "any"
        },
        {
          "src": "img/icons/manifest-icon-512.maskable.png",
          "sizes": "512x512",
          "type": "image/png",
          "purpose": "maskable"
        }
      ]
    }
  }
})
