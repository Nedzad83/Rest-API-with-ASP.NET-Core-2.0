var App;
(function (App) {
    var _root = this;
    this.TaskList = this.TaskList || {};
    _root.ContainerId = "activedeploy";
    var list = new App.UIComponents.List(ContainerId);
    var _service = new App.Providers.ObjectFactory()

    function LoadList() {
        var baseApiUrl = App.Routes.config.baseUrl;
        var envName =  $('#ENV').val();
        var Url =  baseApiUrl + App.Routes.config.productList;
        var Params ={};
        
        list.Url = Url;
        list.Params = Params;
        list.OnClick = function ($element) { };

        list.AfterLoad = function (response) {
            var $table = $('#' + _root.ContainerId + ' table');
            $table.attr({'border': '1', 'cellpadding': '6'});

            //add upd column to the table
            var $delCol = $("<th><span> Delete Attribute</span></th>");
            var $thead = $('#' + _root.ContainerId + ' table thead tr');
            $thead.append($delCol);
        }

        list.GetData();
    }

    list.OnCreatingBodyRow = ($tr)=>{
        var id = $('[data-prop="Id"]', $tr).text();
        var $DelAttrib = $('<td><a href ="javascript:void(0)"><img src="../../../delete.png" border="0" WIDTH=25 HEIGHT=25 ALIGN=center/> </a></td>');
        $DelAttrib.data('attributeId', id);
        $DelAttrib.on('click', DelAttrib);
        $tr.append($DelAttrib);

        return $tr;
    }

    $('#getData').on('click', () => {
        LoadList();
    })


    function DelAttrib(id){
        //_service.getDataService().DelAttrib(id, function(){
            //alert('success ' + id);
        //    LoadList();
        //});
    }
})(App || (App = {}));  




