include(function() {
  var global = this;
  global.Repository = Class.extend({
    initialize: function() {
    },

    getJson: function(url, callback) {
      jQuery.getJSON(this._root + url, callback);
    },

    setRoot: function(value) {
      this._root = value;
      if (!this._root.endsWith('/')) {
        this._root += '/';
      }
    }
  });
  global._repository = new Repository();
  global.getRepository = function() {
    return global._repository;
  };
});