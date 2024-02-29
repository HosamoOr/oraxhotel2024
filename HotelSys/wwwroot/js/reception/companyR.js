
$(document).ready(function () {



});


function saveComp() {
    var namcom11 = $('#NameCompany').val();

   
    var myModel =
    {
        "IdCo":0,
        "NameCo": namcom11,
        "IdAccountCo": 0,
        "IdSub": 0
    };
    // var formData = JSON.stringify(myModel);

    var url = '/_Company/Createjson';   //@Url.Action("Createjson", "_Company")
    //  var formData = $(this).serialize();

    $.post(url, myModel, function (response) {
        $('#addCompanyModal').modal('hide');
        alert(" New company sucess " + response.id);


        $("#companyName").val(response.name + "   " + response.id);

        $("#companyID").val(response.id);
        $("#companyIDacc").val(response.idacc);

        //

        //window.location.href = '@Url.Action("Index", "_Company")';
        //  if (intervalId)
        //clearInterval(intervalId);
    });
}





function selectComCLick(ee) {
    var rowcid = ee.target.getAttribute('data-id');

    var rowcname = ee.target.getAttribute('data-toggle');
    var rowcidacc = ee.target.getAttribute('data-idacc');

   // alert(rowc1);

    $("#companyName").val(rowcname + rowcid);

    $("#companyID").val(rowcid);

    $("#companyIDacc").val(rowcidacc);

    $('#CompanySearchModal').modal('hide');

    ee.preventDefault();



}

var isloadData = false;

function bindDatatableComp() {
    //

    $('#CompanySearchModal').modal('show');

    if (isloadData == false) {
        isloadData = true;

        var dt = $('#tableCompany')
            .DataTable({

                "sAjaxSource": '/_Company/GetDataFarz1/',
                "bServerSide": true,
                "bProcessing": true,
                "bSearchable": true,
                "order": [[1, 'asc']],
                "language": {
                    //"emptyTable": "No record found.",
                    //"processing":
                    //    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">انتضر يتم تحميل البيانات...</span> '

                    "sProcessing": '<i class="fa fa- spinner fa- spin fa - 3x fa - fw" style="color:#2a2b2b; "></i><span class="sr - only"> جاري تحميل البيانات...</span> ',
                    "sLengthMenu": "أظهر _MENU_ سجلات",
                    "sZeroRecords": "لم يعثر على أية سجلات",
                    "sInfo": "إظهار _START_ إلى _END_ من أصل _TOTAL_ سجل",
                    "sInfoEmpty": "يعرض 0 إلى 0 من أصل 0 سجل",
                    "sInfoFiltered": "(منتقاة من مجموع _MAX_ سجل)",
                    "sInfoPostFix": "",
                    "sSearch": "ابحث:",
                    "sUrl": "",
                    "oPaginate": {
                        "sFirst": "الأول",
                        "sPrevious": "السابق",
                        "sNext": "التالي",
                        "sLast": "الأخير"
                    }

                },
                "columns": [



                    {
                        "data": "idCo"
                        ,
                        "render": function (data, type, row, meta) {
                            //return '<a class='btn btn-default btn-sm' onclick=PopupForm('@Url.Action("AddOrEdit","Employee")/" + data + "')><i class='fa fa-pencil'></i> Edit</a>'

                            return '<button   data-idacc="' + row.idAccount + '"  data-id="' + row.id + '"  data-toggle="' + row.name + '" onclick="selectComCLick(event)">select</button>';

                            //btn = '<a class="btn btn-success"  onclick="deleteThis( \'' + row.name + '\' )"/><i class="fa fa-edit"></i> Edit</a>';
                            //return btn;

                        }


                    }
                    ,
                    {
                        "data": "idCo",
                        "autoWidth": false,
                        "width": "40%",
                        "searchable": true
                    },
                    {
                        "data": "nameCo",
                        "autoWidth": true,
                        "searchable": true
                    }
                    ,
                    {
                        "data": "idAccountCo",
                        "autoWidth": true,
                        "searchable": true
                    }
                    //{
                    //   'render': function (date) {

                    //        var date = new Date(parseInt(date.substr(6)));
                    //        var month = date.getMonth() + 1;
                    //        return date.getDate() + "/" + month + "/" + date.getFullYear();
                    //    }
                    //},



                ]
                //, "scrollX": true,
                ////"paging": false,
                //"info": false,
                ////"lengthMenu": false,
                //dom: 'lBfrtip',
                //,
                //buttons: [
                //    'copy', 'pdf', 'print'
                //]

            });
    }

    else {
        $('#tableCompany').DataTable().ajax.reload(null, false);
    }

    


}

