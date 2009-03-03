include(function() {
    var global = this;

    global.SparkViewRenderer = Class.extend({
        initialize: function(view) {
            this.view = view;
        },

        render: function(controller, model) {
            return eval("window.Spark." + this.view).RenderView(model);
        }
    });
});

