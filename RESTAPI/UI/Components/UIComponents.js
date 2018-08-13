(function () {
    this.App = this.App || {};
    this.App.UIComponents = this.App.UIComponents || {};

    var ns = this.App.UIComponents;

    /**
     * Create an html table from api request
     */
    ns.List = (function () {

        //Constructor
        function List(containerId) {
            this.Url = "";
            this.Params = "";
            this.Data = null;
            this.check = false;
            this.ContainerId = containerId;
            this.totalRecords = 0;
            this.LoadingContainer = null;
            this.defaults = {
                cssHeader: "header",
                cssAsc: "headerSortUp",
                cssDesc: "headerSortDown",
                cssChildRow: "expand-child",
                sortInitialOrder: "asc",
            };
        }

        List.prototype.GetData = function () {
            var _root = this;
            $.get(this.Url, this.Params, function (response) {
                _root.Data = response;
                _root.Response = response;
                _root.Render();
            });
        };

        List.prototype.Render = function () {
            var _root = this;

            if (this.Data && this.Response.length > 0) {
                var $table = $("<table data-prop='data'></table>")

                if (typeof RenderHeader === "undefined" || RenderHeader) {
                    var $head = $("<thead></thead>");
                    var $trh = $("<tr></tr>");

                    var $thCheck = $("<td><input class='selectAll' type=\"checkbox\" name=\"allcheck\" ></td>").css({ 'text-align': 'center', 'max-width': '2px' });

                    //get Fields from json response
                    if (this.Response.length > 0) {
                        var columnsIn = this.Response[0];
                        this.Response.Fields = [];

                        for (var key in columnsIn) {
                            this.Response.Fields.push(key);
                        }
                    } else {
                        console.log("No columns");
                    }

                    if (this.Response.Fields.indexOf('Edit') > -1) {
                        var $thEdit = $("<td class='edit'></td>").css({ 'text-align': 'center', 'max-width': '2px' });
                        $trh.append($thEdit);
                    }

                    if (_root.check) {
                        $trh.append($thCheck);
                    }

                    for (var field in this.Response.Fields) {
                        if (field == 'clear') break;
                        var $thTemp = $("<th align='center' data-prop='" + ((field.toString().match(/^[-]?\d*\.?\d*$/) == null) ? field : this.Response.Fields[field]) + "'><span>" + this.Response.Fields[field] + "</span></th>");
                        
                        var $th = this.OnCreatingHeaderColumn($thTemp, this.Response.Fields[field]);

                        if (_root.Sorted) {
                            $table.addClass('tablesorter');
                            $th.addClass(_root.defaults.cssHeader);

                            $th.on('click', function () {
                                if ($(this).hasClass(_root.defaults.cssAsc)) {
                                    $(this).parent().find('th').removeClass(_root.defaults.cssAsc).removeClass(_root.defaults.cssDesc);
                                    $(this).removeClass(_root.defaults.cssAsc).addClass(_root.defaults.cssDesc);
                                    _root.Params.sortColumn = $(this).data("prop") + ' ASC';
                                    _root.BuscarDatos(false);
                                }
                                else {
                                    $(this).parent().find('th').removeClass(_root.defaults.cssAsc).removeClass(_root.defaults.cssDesc);
                                    $(this).removeClass(_root.defaults.cssDesc).addClass(_root.defaults.cssAsc);
                                    _root.Params.sortColumn = $(this).data("prop") + ' DESC';
                                    _root.BuscarDatos(false);
                                }
                            })
                        }
                        $trh.append($th);
                    }

                    $head.append($trh);
                    $table.append($head);
                }

                $tbody = $("<tbody></tbody>");

                $.each(this.Data, function (idx, element) {
                    var $tr = $("<tr></tr>");
                    var $tdCheck = $("<td><input class='chkDetails' type=\"checkbox\" name=\"allcheck\"></td>").css({ 'text-align': 'center' });

                    if (_root.Response.Fields.indexOf('Edit') > -1) {
                        var $tdEdit = $("<td><a class='edit'>Edit</a></td>").css({ 'text-align': 'center', 'max-width': '25px' });
                        $tr.append($tdEdit);
                    }

                    if (_root.check) {
                        $tr.append($tdCheck);
                    }

                    for (var prop in element) {
                        var $tdTemp = $("<td data-prop='" + prop + "'>" + element[prop] + "</td>").css({ 'text-align': 'left' });
                        var $td = _root.OnCreatingBodyCell($tdTemp, element[prop], element);
                        $tr.append($td);
                    }

                    var $trTemp = _root.OnCreatingBodyRow($tr);
                    $tbody.append($trTemp);

                });

                $table.append($tbody);

                $("#" + this.ContainerId).html($table);

                if (_root.Data.length == 0 && $("#" + this.ContainerId + ' .NotHasRows').length == 0) {
                    var $noRows = $("<div class='NotHasRows'>No records found...</div>");
                    $("#" + this.ContainerId).append($noRows);
                }
                else {
                    $("#" + this.ContainerId + '.NotHasRows').remove();
                }
            } else {
                if ($("#" + this.ContainerId + ' .NotHasRows').length == 0) {
                    var $noRows = $("<div class='NotHasRows'>No records found...</div>");
                    $("#" + this.ContainerId).append($noRows);
                }

                $("#" + this.ContainerId).find("tbody").html("");
            }


            /**
             * Build the pagination
             *
             * @param {*} list
             * @param {*} TotalPages
             * @returns 
             */
            List.prototype.SetPagination = function (list, TotalPages) {

                // pagination
                var totalNumberOfPages = TotalPages;


                var $ul = $("<ul></ul>");
                var num = 1;
                var numberOfPagesToShow = 5;
                var $totalRecords = $("<label class='total-registro'>" + "Total: " + _root.totalRecords + "</label>");

                if (totalNumberOfPages < numberOfPagesToShow) {
                    numberOfPagesToShow = totalNumberOfPages;
                }

                if (totalNumberOfPages > numberOfPagesToShow) {
                    num = list.Params.start || 1;

                    if (num <= 3) {
                        num = 1;
                    }
                    else {
                        num -= 2;
                    }

                    numberOfPagesToShow = parseInt(num) + 4;
                }

                if (parseInt(num) > (totalNumberOfPages - 5)) {
                    //if (parseInt(num) > (totalNumberOfPages - 5)) {
                    num = totalNumberOfPages - 4;
                    //}
                    numberOfPagesToShow = totalNumberOfPages;
                }

                if (totalNumberOfPages < 5) {
                    num = 1;
                    var numberOfPagesToShow = totalNumberOfPages;
                }

                while (numberOfPagesToShow >= num) {

                    if (parseInt(TotalPages) < 2) {
                        break;
                    }

                    $li = $("<li class='pagination-item'>" + num + "</li>");

                    if (num == (list.Params.start || 1)) {
                        $li.addClass("current");
                    }

                    $li.on('click', function () {
                        list.Params.start = $(this).text().trim();
                        list.GetData();
                    });

                    $ul.append($li);
                    num++;
                }
                //Add total number of records 

                $ul.append($totalRecords);
                return $ul;
            }

            this.AfterLoad(this.Response)
        };

        List.prototype.OnCreatingHeaderColumn = ($th, field) => {
            return $th;
        }

        List.prototype.OnCreatingBodyCell = ($td, field, row) => {
            return $td;
        }

        List.prototype.OnCreatingBodyRow = ($tr) => {
            return $tr;
        }

        List.prototype.OnClick = () => { };

        List.prototype.AfterLoad = () => { };

        return List;
    }());
}());