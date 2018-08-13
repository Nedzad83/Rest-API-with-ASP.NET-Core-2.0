var App;
(function (App) {
    var Providers;
    (function (Providers) {
        var DataService = (function () {
            var _root = this;
            function DataService() {
            }
            
            DataService.prototype.DelAttrib = (id, $success, $error) => {
                $.ajax({
                    url: getBaseUrl() + App.Routes.config.delAttrib + id,
                    type: 'DELETE',
                    success: $success,
                    error: $error
                });
            };

            DataService.prototype.updAttrib = (id, $success, $error) => {
                $.ajax({
                    url: getBaseUrl() + App.Routes.config.updAttrib + id,
                    type: 'POST',
                    success: $success,
                    error: $error
                });
            };

            DataService.prototype.addAttrib = (env, an, av, $success, $error) => {
                var url = `${getBaseUrl() + App.Routes.config.addAttrib}${env}/${an}/${av}`;
                $.ajax({
                    url: url,
                    type: 'POST',
                    success: $success,
                    error: $error
                });
            };

            function getBaseUrl() {
                return App.Routes.config.baseUrl;
            }
            return DataService;
        }());
        Providers.DataService = DataService;
    })(Providers = App.Providers || (App.Providers = {}));
})(App || (App = {}));