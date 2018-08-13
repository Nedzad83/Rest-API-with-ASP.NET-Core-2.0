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
            var $updCol = $("<th><span>Update</span></th>");
            var $thead = $('#' + _root.ContainerId + ' table thead tr');
            $thead.append($updCol);
        }

        list.GetData();
    }

    list.OnCreatingBodyRow = ($tr)=>{
        var id = $('[data-prop="id"]', $tr).text();
        var $UpdAttrib = $('<td><a href ="javascript:void(0)"><img src="./Styles/update.jpg" border="0" WIDTH=25 HEIGHT=25 ALIGN=center/> </a></td>');
        $UpdAttrib.data('Id', id);
        $UpdAttrib.on('click', UpdAttrib);
        $tr.append($UpdAttrib);

        return $tr;
    }

    $('#getProducts').on('click', () => {
        LoadList();
    })

    function UpdAttrib(){
         var id = $(this).data('Id');
         alert('Update Successfull for productId: ' + id);
        //  _service.getDataService().UpdateProduct(id, function(){
        //      alert('success');
		// 	 LoadList();
        //  });
    }

})(App || (App = {}));  




