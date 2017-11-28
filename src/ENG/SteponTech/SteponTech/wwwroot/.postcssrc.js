// https://github.com/michael-ciniawsky/postcss-load-config

module.exports = {
  "plugins": {
    // to edit target browsers: use "browserlist" field in sidebar.json
    // "autoprefixer": {},
    "postcss-salad": {
      browsers: ['ie > 8', 'last 2 version'],
      features: {
        "bem": true//pass boolean false can disable the plugin
      }
    }
  }
}
