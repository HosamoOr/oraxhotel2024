$(document).ready(function () {
    bindDatatable();
});

function bindDatatable() {
    datatable = $('#tblReception')
        .DataTable({

            "sAjaxSource": '/HomeCancelReception/GetDataFarz1',
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "order": [[1, 'asc']],
            "language": {
                //"emptyTable": "No record found.",
                //"processing":
                //    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">انتضر يتم تحميل البيانات...</span> '

                //,
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


                    "data": "idReception",
                    "autoWidth": true,
                    "searchable": true,
                    //"width": "160px"
                },
                {
                    "data": "room.nameRoom",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "customer.name",
                    //"autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "startDate",
                    "autoWidth": false,
                    "searchable": true
                }, {
                    "data": "endDate",
                    "autoWidth": false,
                    "searchable": true
                }, {
                    "data": "qtyTime",
                    "autoWidth": true,
                    "searchable": true
                },
                 {
                    "data": "price",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "status",
                    "autoWidth": true,
                    "searchable": true,
                    "render": function (data, type, row, meta) {
                        if (row.status == '1') {
                            return "<div>حجز موقت</div>";

                        }
                        else if (row.status == '2') {
                            return "<div>تسجيل دخول</div>";
                        }
                        else if (row.status == '3') {
                            return "<div>تسجيل خروج</div>";
                        }
                        else if (row.status == '4') {
                            return "<div> ملغي</div>";
                        }
                        else {
                            return "<div> محذوف</div>";
                        }


                    }
                },
                {
                    "data": "total",
                    "autoWidth": true,
                    "searchable": true
                }
                , {
                    "data": "price",
                    "render": function (data) {
                        return "<a class='btn btn-default btn-sm' href='/Reception/Edit'><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ")><i class='fa fa-trash'></i> </a>";
                    },
                    "orderable": false,
                    "searchable": false,
                    "width": "150px"
                }


            ]
        });
}



