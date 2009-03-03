/**
 * jQuery.Bubble - Event Bubbling with jQuery 
 * Copyright (c) 2007 Ariel Flesler (flesler AT hotmail DOT com)
 * Licensed under GPL (http://www.opensource.org/licenses/gpl-license.php) license.
 * Date: 09/22/2007
 *
 * @projectDescription Alternate function for jQuery.fn.trigger, that will cause bubbling.
 *
 * @author Ariel Flesler
 * @version 1.2
 *
 * @id jQuery.fn.bubble
 * @method
 * @param {String} type Event name.
 * @param {Anything} data Optional: array of arguments to add to the handler, or a single argument (not an array).
 * @returns {jQuery} Returns the same jQuery object, for chaining.
 *
 * @see http://catcode.com/domcontent/events/capture.html
 * @see http://www.webreference.com/js/column10/eventbubbling.html
 *
 * Notes:
 *    - Tested along with jQuery 1.2.1, on Firefox 2.0.0.6 and IE 6. Both on Windows.
 *    - For now the call to node[eventName] ( f.e: node.click() ) is avoided, only jquery events will be triggered.
 * TO-DO:
 *    - Trigger native bindings ?
 **/

(function( $ ){
  
  $.fn.bubble = function(type, data){
    data = jQuery.makeArray(data || []);//ensure an array of data
    var parts = type.split('.');
    type = parts[0];
    return this.each(function(){
      var node = this, event = new Event( type, node ), args = [ event ].concat( data );
      var evs, ev;
      while( node != document && !event.cancelBubble ){//keep going up till you hit the document or propagation is stopped.
        if( evs = jQuery.data(node,'events') && jQuery.data(node,'events')[type] ){//does this node have events attached?
          for( ev in evs ) {//iterate through the stored event handlers for this event, in this element.
            if( (handler = evs[ev]) && !parts[1] || handler.type == parts[1] ){//only go ahead if the namespace matches
              event.handler = handler;//store the handler in the event object (imitating jquery's native behavior)
              event.data = handler.data;//copy the stored data to our custom event object
              if ( handler.apply( node, args ) === false ) {
                event.preventDefault();
                event.stopPropagation();//stop bubbling.
              }
            }
          }
        }
        node = node.parentNode;//jump to the next ancestor.     
      }
      event.target = null; //memory leak ?
    });
  };
  
  function Event( type, target ){//fake event constructor
    this.type = type;//event name ( f.e: click )
    this.target = target;//DOM element that triggered the event
  };
  $.extend( Event.prototype,{
    returnValue: true,//use preventDefault to skip the call to the native event hanlder (wont work for now)
    cancelBubble: false,//use stopPropagation to stop the bubbling
    stopPropagation:function(){
      this.cancelBubble = true; 
    },
    preventDefault:function(){
      this.returnValue = false; 
    }
  });
  
})(jQuery);