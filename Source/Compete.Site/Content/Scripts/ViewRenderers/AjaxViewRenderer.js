include(function(){
   var global = this;

   global.AjaxViewRenderer = Class.extend({
      initialize: function(url){
          this.url = url;
      },

      render: function(){
         return $.ajax({
            async: false,
            cache: false,
            dataType: 'html',
            url: this.url}).responseText;
      }
   });
});

