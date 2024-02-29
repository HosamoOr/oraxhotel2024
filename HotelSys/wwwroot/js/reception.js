var $table = $('#table');



$(function () {



    $('#toolbar').find('select').change(function () {



        $table.bootstrapTable('destroy').bootstrapTable({
            exportDataType: $(this).val(),
            //exportTypes: ['xlsx', 'xlsx', 'txt', 'csv'], //Optional export file type
            exportTypes: ['xlsx', 'pdf', 'doc', 'csv', 'excel', 'png', 'txt', 'powerpoint'],
            //exportFooter: true,
            //  export: 'glyphicon-export icon-share',
            // showExport: true,


            url: '/Customers/GetDataFarz',
            //height: 790,                    //ارتفاع الجدول
            search: true,
            searchAlign: "left",
            trimOnSearch: true,         //حذف الفراغات عند البحث
            searchOnEnterKey: true, //设置为 true时，按回车触发搜索方法，否则自动触发搜索方法

            /* icons: {   //تغيير الايقونات الافتراضية
                 refresh: "glyphicon-repeat",
                 toggle: "glyphicon-list-alt",
                 columns: "glyphicon-list",
                 export: 'glyphicon-export'
             },*/


            exportOptions: {

                // ignoreColumn: [0, 1], //استخراج اعمدة محددة    $(":selected", $("#userinputid")).text();
                fileName: function () {
                    // return $("#userinputid option").filter(":selected").text();
                }

                , //file name setting
                worksheetName: 'nameUser', //table workspace name
                tableName: 'nameUser',
                excelstyles: ['background-color', 'color', 'font-size', 'font-weight']
            },
            queryParams: queryParams,       //查询函数
            //pagination: true,                       //显示分页栏
            showRefresh: true,               //显示刷新按钮
            showColumns: true,              //是否显示内容列下拉框
            // striped: true,                      //设置为 true 会有隔行变色效果
            //sortName: "plateNumber",                //定义排序字段
            //sortOrder: "desc",                  //定义排序方式，'asc' 或者 'desc'
            // silentSort: false,                      //设置为 false 将在点击分页按钮时，自动记住排序项
            //maintainSelected: true,         //设置为 true 在点击分页按钮或搜索按钮时，将记住checkbox的选择项
            //toolbar: 'toolbar',          //工具栏id
            // sidePagination: "server",                               //设置在哪里进行分页，可选值为 'client' 或者 'server'，默认'client'
            //pageNumber: 1,                                             //如果设置了分页，首页页码
            // pageSize: 20,                                               //每页每页显示行数
            //  pageList: [20, 50, 100, 200, 'All'],                         //设置可供选择的页面数据条数,如果数据量大于50小于100，则只会显示20，50，100三项
            showFullscreen: true,                                    //是否显示全屏按钮
            responseHandler: function (data) {                  //如果后台返回的数据格式与bootstrapTable的数据格式不同在这里调整
                return {
                    "page": data.pages,
                    "total": data.total,
                    "rows": data.list
                };
            },
            columns: [
                //{ field: 'state'},

                { field: 'numProof', title: 'رقم الاثبات' },
                { field: 'typeProof', title: 'نوع الاثبات' },
                { field: 'name', title: 'Name', formatter: function (value, row, index) { return '<a href=' + value + '  target="_blank"  rel="noopener noreferrer" >' + value + '</a>'; } },


                { field: 'locRelease', title: 'مكان الاصدار' },

                { field: 'phoneWork', title: 'Mobile No' },
                { field: 'email', title: 'البريد الالكتروني' },
            ]

        })
    }).trigger('change')


     


});

//$('#userinputid').on('change', function (event) {
//    var form = $(event.target).parents('form');

//    $('#table').bootstrapTable('refresh');

//});

function queryParams(params) {
    //params = {
    //    search: "", sort: undefined, order: undefined, limit: 10,
    //    offset: 0,
    //    username: "hsam"
    //};//，可通过上面的配置修改
    var temp = {            //这里的键的名字和控制器的变量名必须一致
        limit: params.limit,
        offset: params.offset,
        pageSize: this.pageSize,
        pageNumber: this.pageNumber,
        userid: 100
    };
    return temp;
}






//$(document).ready(function () {
//    bindDatatable();
//});
//function bindDatatable() {

//    var $table = $('#table');


//    $(function () {



//        $('#toolbar').find('select').change(function () {


//            $table.bootstrapTable('destroy').bootstrapTable({
//                exportDataType: $(this).val(),
//                //exportTypes: ['xlsx', 'xlsx', 'txt', 'csv'], //Optional export file type
//                //exportTypes: ['xlsx', 'pdf', 'doc', 'csv', 'excel', 'png', 'txt', 'powerpoint'],
//                //exportFooter: false,
//                //  export: 'glyphicon-export icon-share',
//                showExport: true,


//                url: '/Customers/GetDataFarz',
//                //height: 790,                    //ارتفاع الجدول
//                search: true,
//                searchAlign: "left",
//                trimOnSearch: true,         //حذف الفراغات عند البحث
//                searchOnEnterKey: true, //设置为 true时，按回车触发搜索方法，否则自动触发搜索方法

//                /* icons: {   //تغيير الايقونات الافتراضية
//                     refresh: "glyphicon-repeat",
//                     toggle: "glyphicon-list-alt",
//                     columns: "glyphicon-list",
//                     export: 'glyphicon-export'
//                 },*/



//                queryParams: queryParams,       //查询函数
//                //pagination: true,                       //显示分页栏
//                showRefresh: true,               //显示刷新按钮
//                showColumns: true,              //是否显示内容列下拉框
//                // striped: true,                      //设置为 true 会有隔行变色效果
//                //sortName: "plateNumber",                //定义排序字段
//                //sortOrder: "desc",                  //定义排序方式，'asc' 或者 'desc'
//                // silentSort: false,                      //设置为 false 将在点击分页按钮时，自动记住排序项
//                //maintainSelected: true,         //设置为 true 在点击分页按钮或搜索按钮时，将记住checkbox的选择项
//                //toolbar: 'toolbar',          //工具栏id
//                // sidePagination: "server",                               //设置在哪里进行分页，可选值为 'client' 或者 'server'，默认'client'
//                //pageNumber: 1,                                             //如果设置了分页，首页页码
//                // pageSize: 20,                                               //每页每页显示行数
//                //  pageList: [20, 50, 100, 200, 'All'],                         //设置可供选择的页面数据条数,如果数据量大于50小于100，则只会显示20，50，100三项
//                showFullscreen: true,                                    //是否显示全屏按钮
//                responseHandler: function (data) {                  //如果后台返回的数据格式与bootstrapTable的数据格式不同在这里调整
//                    return {
//                        "page": data.pages,
//                        "total": data.total,
//                        "rows": data.list
//                    };
//                },
//                columns: [
//                    { field: 'state' },
//                    { field: 'name', title: 'اسم العميل', formatter: function (value, row, index) { return '<a href=' + value + '  target="_blank"  rel="noopener noreferrer" >' + value + '</a>'; } },

//                    { field: 'numProof', title: 'رقم الاثبات' },
//                    { field: 'typeProof', title: 'نوع الاثبات' },


//                    { field: 'phoneWork', title: 'رقم التلفون' },

//                ]

//            })


//        });
//            }
//    );
//}


//function queryParams(params) {
//    //params = {
//    //    search: "", sort: undefined, order: undefined, limit: 10,
//    //    offset: 0,
//    //    username: "hsam"
//    //};//，可通过上面的配置修改
//    var temp = {            //这里的键的名字和控制器的变量名必须一致
//        limit: params.limit,
//        offset: params.offset,
//        pageSize: this.pageSize,
//        pageNumber: this.pageNumber,
//        userid: 100
//    };
//    return temp;
//}




