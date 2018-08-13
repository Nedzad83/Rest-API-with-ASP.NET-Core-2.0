var App;
(function (App) {
    this.App.Routes = this.App.Routes || {};
    this.App.Routes.config = {
        baseUrl: 'http://localhost:60620/api/',
        attributeList: 'Attribute/list/',
		delAttrib: 'Attribute/deleteAtts/',
        updAttrib: 'Attribute/updAttrib/',
        addAttrib: 'Attribute/addAttrib/',
        cloneAttrib: 'Attribute/cloneAttrib/',
        productList: 'Products',
    };
})(App || (App = {}));