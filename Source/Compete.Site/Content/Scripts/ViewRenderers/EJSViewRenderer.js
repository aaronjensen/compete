include(function() {
   var global = this;

   global.EJSViewRenderer = Class.extend({
      initialize: function(template) {
         this.ejsTemplate = typeof (template) == 'string' ? new EJS({ url: template}) : template;
      },

      render: function(controller, model) {
         model.component = function(type, data) { return controller.addController(type, data) };
         return this.ejsTemplate.render(model);
      }

   });
});

