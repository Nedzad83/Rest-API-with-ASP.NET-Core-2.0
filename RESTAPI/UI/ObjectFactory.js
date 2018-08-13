var App;
(function (App) {
    var Providers;
    (function (Providers) {
        var ObjectFactory = (function () {
            function ObjectFactory() {
            }
            ObjectFactory.prototype.getDataService = function () {
                return new App.Providers.DataService();
            };
            return ObjectFactory;
        }());
        Providers.ObjectFactory = ObjectFactory;
    })(Providers = App.Providers || (App.Providers = {}));
})(App || (App = {}));