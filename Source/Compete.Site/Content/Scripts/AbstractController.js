include("Content/Scripts/ViewRenderers/DefaultViewRenderer.js");
include("Content/Scripts/3p/jquery/jquery.bubble-sync.js");
include(function() {
  var global = this;
  var NonBubblingEvents = ["change", "focus"];

  global.AbstractController = Class.extend({

    initialize: function(data) {
      data = data ? data : {};
      this.element = null;
      this.controllerActions = [];
      this.isAttached = false;
      this.controllers = {};
      this.isRendered = false;
      this.registerActions();
      this.controllers = {};
      this.model = {};
      this.viewRenderClass = data.renderer ? data.renderer : "Default";
      this.renderer = null;

    },

    Action: Class.extend({
      initialize: function(event, matcher, callBack) {
        this.event = event;
        this.matcher = matcher;
        this.callBack = callBack;
      }
    }),

    registerActions: function() {
    },

    /* Attachment and Rendering Methods */
    _getRenderer: function() {
      if (this.renderer == null) {
        this.renderer = this._newRenderer();
      }
      return this.renderer;
    },

    _newRenderer: function() {
      var renderer = global[this.viewRenderClass + "ViewRenderer"];
      return new renderer(this.view);
    },

    renderTo: function(container) {
      var self = this;
      this.renderAndAttach(function() {
        $(container).html(self.element);
        return true;
      });
      return this;
    },

    appendTo: function(container) {
      var self = this;
      this.renderAndAttach(function() {
        $(container).append(self.element);
        return true;
      });
      return this;
    },

    renderIfNecessary: function() {
      if (!this.isRendered) {
        this.replace(this.element);
      }
    },

    replace: function(container) {
      var self = this;
      this.renderAndAttach(function() {
        $(container).replaceWith(self.element);
        return true;
      });
      return this;
    },

    newControllerFromType: function(type, params) {
      if (!params) {
        params = {};
      }

      var ctr = this[type + 'Controller'];
      if (!ctr) {
        ctr = window[type + 'Controller'];
      }
      return new ctr(params);
    },

    addController: function(type, data) {
      var controllerName = type;

      if (!data) {
        data = {};
      }

      if (data.name) {
        controllerName = data.name;
      }


      this.controllers[controllerName] = this.newControllerFromType(type, data);
      return "<div controller='" + controllerName + "'" + (data.autoRender ? " autorender='true' " : "") + "></div>";
    },

    render: function() {
      this.setElement(this._getRenderer().render(this, this.model));
      return true;
    },


    renderAndAttach: function(attachToPageFunction) {
      if (this.render() && attachToPageFunction() && this.attach()) {
        return true;
      }
      else {
        return false;
      }
    },

    attachToExistingElement: function(element) {
      this.setElement(element);
      this.attach();
    },

    attach: function() {
      this.attachActions();
      this.attachControllers();
      this.isRendered = true;
      this.onRenderFinished();
    },

    onRenderFinished: function() {
    },

    attachActions: function() {
      var self = this;
      this.element.unbind();
      this.element.find("*").unbind();
      for (var i = 0; i < this.controllerActions.length; i++) {
        var action = this.controllerActions[i];
        var element = this._getElementForBind(action);
        element.bind(action.event, { action: action }, function(event, customData) { self.onEvent(event, customData); });
      }
      this.isAttached = true;
    },

    _getElementForBind: function(action) {
      if (this._isNonBubbling(action.event)) {
        return this.element.find(action.matcher);
      }
      return this.element;
    },

    _isNonBubbling: function(eventName) {
      if ($.inArray(eventName, NonBubblingEvents) >= 0) {
        return true;
      }
      return false;
    },

    attachControllers: function() {
      var self = this;
      this.element.find('*[controller]').each(function() {
        self.attachController($(this));
      });
    },

    attachController: function(controllerDiv) {
      var controllerName = controllerDiv.attr('controller');
      var existingController = this.controllers[controllerName];
      if (!existingController) {
        existingController = this.newControllerFromType(controllerName);
        this.controllers[controllerName] = existingController;
      }

      existingController.setElement(controllerDiv);
      if (controllerDiv.attr("autoRender")) {
        existingController.replace(controllerDiv);
      }
    },

    hide: function() {
      if (this.element) {
        $(this.element).hide();
      }
    },

    setElement: function(element) {
      if ($(element).length > 1) {
        alert("Controller needs a single root node element. " + this.view + " Could mean the template wasn't found, check request.");
        return;
      }
      this.element = $(element);
    },

    wrapElement: function(element) {
      this.setElement(element);
      this.attachActions();
    },

    show: function() {
      if (this.element) {
        this.renderIfNecessary();
        $(this.element).show();
      }
    },

    addAction: function(eventName, matcher, callBack) {
      this.controllerActions.push(new this.Action(eventName, matcher ? matcher : '*', callBack));
    },


    raiseEvent: function(event, element, eventData) {
      var self = this;
      var actions = this.getActionsForEvent(event);
      $.each(actions, function(index, action) {
        self.onEvent({ target: element[0], data: { action: action} }, eventData);
      });
    },

    getActionsForEvent: function(event) {
      var actions = new Array();
      $.each(this.controllerActions, function(index, action) {
        if (event == action.event) {
          actions.push(action);
        }
      });
      return actions;
    },

    onEvent: function(event, eventData) {
      var action = event.data.action;
      var eventElement = event.target;
      var matched = false;
      var selectorTopElements = this.element.find(action.matcher);
      if ($.inArray(eventElement, selectorTopElements) >= 0) {
        event.actionTarget = event.target;
        matched = true;
      }
      else {
        $.each(selectorTopElements, function(i, descendantElement) {
          if ($.inArray(eventElement, $(descendantElement).find("*")) >= 0) {
            matched = true;
            event.actionTarget = descendantElement;
            return false;
          }
          return true;
        });
      }
      if (matched) {

        this[action.callBack](event, eventData);
      }
    },

    remove: function() {
      if (this.isRendered) {
        this.cascadeRemove();
        this.element.remove();
        this.controllerActions = [];
        this.isRendered = false;
      }
    },

    cascadeRemove: function() {
      $.each(this.controllers, function() {
        if (this) {
          this.remove();
        }
      });
    },

    update: function() {
      this.cascadeRemove();
      this.replace(this.element);
    },

    publishEvent: function(event, args) {
      this.publishEventFromElement(this.element, event, args);
    },

    publishEventFromElement: function(element, event, args) {
      if (typeof (args) == "undefined") {
        args = {};
      }
      args.sender = this;
      element.bubble(event, args);
    }
  });
});