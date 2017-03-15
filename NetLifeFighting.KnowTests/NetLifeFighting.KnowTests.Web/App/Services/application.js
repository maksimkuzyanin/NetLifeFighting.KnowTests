angular.module('app')
	.service('application', function application() {
		var ready = false, registeredListeners = [];

		var callListeners = function() {
			for (var i = registeredListeners.length - 1; i >= 0; i--) {
				registeredListeners[i]();
			}
		}

		return {
			isReady: function() {
				return ready;
			},

			makeReady: function() {
				ready = true;

				callListeners();
			},

			registerListener: function(callback) {
				if (ready) {
					callback();
				}

				registeredListeners.push(callback);
			}
		}
});