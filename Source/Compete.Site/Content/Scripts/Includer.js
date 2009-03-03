(function() {
  var global = this;
  if (!global.console) {
    global.console = { log: function() { } };
  }
  global.Templates = {};

  global.setDefaultIncluder = function(includer) {
    global.include = function(path, options) {
      includer.include(path, options);
    }

    global.includeTemplate = function(path, options) {
      includer.includeTemplate(path, options);
    }

    global.includeBundle = function(bundleFunction) {
      includer.bundle(bundleFunction);
    }

    global.loadIncludes = function() {
      includer.processQueueIfNotAlreadyProcessing("KickStarting Queue");
    }
  };

  global.Includer = Class.extend({
    initialize: function(options) {
      this.basePath = options.basePath;
      this.templateBasePath = options.templateBasePath;
      this.suffix = options.suffix ? options.suffix : "";

      this.bundlePath = options.bundlePath ? options.bundlePath : "bundles";
      if (!this.bundlePath.match(/\/$/)) {
        this.bundlePath += "/";
      }
      if (!this.basePath.match(/\/$/)) {
        this.basePath += "/";
      }
      if (this.tempalteBasePath && !this.templateBasePath.match(/\/$/)) {
        this.templateBasePath += "/";
      }
      this.included = {};
      this.includeQueue = new Array();
      this.includeStack = new Array();
      this.queueStopped = true;
      this.bundling = false;
      this.scriptText = "";
      this.bundled = new Array();
      this.bundleName = "nonamebundle";
      this.useBundles = options.useBundles ? options.useBundles : false;
      this.cache = false;
      if (window._rhino) {
        this.usingRhino = true;
      }
      else {
        this.usingRhino = false;
      }
    },


    getUnloadedScripts: function(paths) {
      var unloadedPaths = new Array();
      for (var i = 0; i < paths.length; i++) {
        var path = paths[i];
        if (!this._isIncluded(path)) {
          unloadedPaths.push(path);
        }
      }
      return unloadedPaths;
    },

    include: function(path) {
      if (typeof (path) == "object") {
        if (path.bundle) {
          this.bundle(path.bundle, path.name);
          return;
        }
        if (path.included) {
          this.included[path.included] = true;
          return;
        }
      }
      /* Functions are just added to the queue*/
      if (typeof (path) == "function") {
        if (this.useBundles && this.bundling) {
          path.apply(window);
          console.log("Live Executing callback");
          return;
        }
        this.includeQueue.push({ callback: path });
        return;
      }
      path = path.replace(/\s/g, '');
      var self = this;
      var paths = path.split(',');
      var unloadedPaths = this.getUnloadedScripts(paths);
      var pathToQueue = null;
      while (pathToQueue = unloadedPaths.shift()) {
        // console.log("Including: " + pathToQueue);
        this.included[pathToQueue] = true;
        this.includeQueue.push({ path: pathToQueue });
      }
    },

    bundle: function(bundlingFunction, bundleName) {
      this.bundleName = bundleName;
      this.includeQueue.push({ compress: "start" });
      if (this.useBundles) {
        this.include(this.bundlePath + bundleName + ".js");
        return;
      }
      else {
        bundlingFunction();
      }
      this.includeQueue.push({ compress: "stop" });
    },

    processQueueIfNotAlreadyProcessing: function(callname) {
      if (this.queueStopped) {
        this.processQueue(callname);
      }
    },

    processQueue: function(callname) {
      this.queueStopped = false;
      //console.log("Processing Queue: " + callname);
      var self = this;
      if (this.includeQueue.length > 0) {
        var entry = this.includeQueue.shift();
        if (entry.compress && entry.compress == "start") {
          this._startBundling();
        }
        else if (entry.compress && entry.compress == "stop") {
          this._stopBundling();
        }
        else if (entry.callback) {
          //console.log("Calling Callback ");
          entry.callback.apply(window);
        }
        else if (entry.path) {
          this.includeStack.push(this.includeQueue);
          this.includeQueue = new Array();
          this.dynamicInclude(entry.path, function() { self.processQueue("Called after " + entry.path); });
          return;
        }
      }
      if (this.includeQueue.length > 0) {
        this.processQueue("Called Because There Are still Items");
      }
      else if (this.includeStack.length > 0) {
        this.includeQueue = this.includeStack.pop();
        this.processQueue("Called after poping stack");
      }
      else {
        this.queueStopped = true;
      }

    },

    dynamicInclude: function(path, callback) {
      if (path.match(/\.ejs$/)) {
        this.dynamicIncludeEjs(path, callback);
      }
      else {
        this.dynamicIncludeJs(path, callback);
      }
    },

    dynamicIncludeEjs: function(path, callback) {
      $.ajax({
        async: false,
        chache: true,
        dataType: 'text',
        type: 'get',
        url: this.templateBasePath + path + this.suffix,
        error: function() {
          throw ("Error getting template");
        },
        success: function(data) {
          global.Templates[path] = new EJS({ text: data });
          callback();
        }
      });

    },

    dynamicIncludeJs: function(path, callback) {
      console.log("Loading " + path);
      var self = this;
      if (this.usingRhino) {
        load(path);
        if (this.bundling) {
          this.include(function() {
            var script = readFile(path);
            self.scriptText += "\n\n//" + path + "\n;\n" + script;
            self.bundled.push(path);
          });
        }
        callback();
        return;
      }
      var head = document.getElementsByTagName("head")[0];
      var script = document.createElement('script');
      script.type = 'text/javascript';
      script.src = this.basePath + path + this.suffix;

      if (callback) {
        script.onload = callback;
        script.onreadystatechange = function() {
          if (this.readyState == 'complete' || this.readyState == 'loaded') {
            script.onreadystatechange = null;
            callback();
          }
        };
        script.onError = function() { alert("ERROR LOADING: " + path); };
      }
      head.appendChild(script);
    },

    _startBundling: function() {
      /*Only compress if we are running in rhino*/
      this.bundling = true;
      if (this.usingRhino) {
        console.log("Start compression");
      }
    },

    _stopBundling: function() {
      if (this.bundling) {
        this.bundling = false;
        if (this.usingRhino) {
          console.log("End Compression");
          var out = new java.io.FileWriter(new java.io.File("bundles/" + this.bundleName + ".js"));
          var input = new java.lang.String(this._bundledPrefix() + this.scriptText);
          out.write(input, 0, input.length());
          out.flush();
          out.close();
          this.scriptText = "";
        }
      }
    },

    _bundledPrefix: function() {
      var bundledPrefix = "";
      while (this.bundled.length > 0) {
        var path = this.bundled.shift();
        bundledPrefix += "include({included:'" + path + "'});\n";
      }
      return bundledPrefix;
    },

    _isIncluded: function(path) {
      return this.included[path];
    }
  });
})();