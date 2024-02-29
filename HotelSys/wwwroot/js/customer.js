$(document).ready(function () {
    bindDatatable();
});

function bindDatatable() {
    datatable = $('#tblPolo')
        .DataTable({

            "sAjaxSource": '/Customers/GetDataFarz1',
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "order": [[1, 'asc']],
            "language": {
                //"emptyTable": "No record found.",
                //"processing":
                //    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">انتضر يتم تحميل البيانات...</span> '
                //,

                "sProcessing": "جارٍ التحميل...",
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


                    "data": "numProof",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "typeProof",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "name",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "locRelease",
                    "autoWidth": true,
                    "searchable": true
                }, {
                    "data": "phoneWork",
                    "autoWidth": true,
                    "searchable": true
                }, {
                    "data": "email",
                    "autoWidth": true,
                    "searchable": true
                }



            ]
        });
}







