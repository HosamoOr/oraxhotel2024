
var isloadData1 = false;

////for Roooom
function bindDatatableRoomLogin() {

    $('#receptionRoomModal').modal('show');

    if (isloadData1 == false) {
        isloadData1 = true;

        var dt = $('#tablerecptionroom')
            .DataTable({

                "sAjaxSource": '/RoomLoginCurrent/GetDataFarz1/',
                "bServerSide": true,
                "bProcessing": true,
                "bSearchable": true,
                "order": [[1, 'asc']],
                "language": {
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

                        "render": function (data, type, row, meta) {
                            return '<button class="btn btn-success" onclick="selectCustomerClick(JSON.parse(\'' + JSON.stringify(row).replace(/'/g, '&apos;').replace(/"/g, '&quot;') + '\'))">' + 'اختر' + '</button>';


                        }


                    }
                    ,
                    {
                        "data": "idReception",
                        "autoWidth": true,

                        "searchable": true
                    },
                    {
                        "data": "roomModel.nameR",
                        "autoWidth": true,
                        "searchable": true
                    },


                    {
                        "data": "nameCuOrCo",
                        "autoWidth": false,
                        "searchable": true,
                        "width": "100%",
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

        $('#tablerecptionroom').DataTable().ajax.reload(null, false);
    }



}

function showReceMpdal() {

    var toolr = $("#TotalService").val();
   

    if (toolr <= 0) {
        Swal.fire({
            title: 'يجب اختيار منتج واحد ع الاقل '
        })
       // alert("يجب اختيار منتج واحد ع الاقل")
    }
    else {
        bindDatatableRoomLogin();
    }
}


function selectCustomerClick(roomDCu) {

    
 
        saveBondService(roomDCu.idAccount, roomDCu.customerOrCompany, true, roomDCu.idReception, roomDCu.nameCuOrCo, roomDCu.roomModel.nameR)

   
   }

