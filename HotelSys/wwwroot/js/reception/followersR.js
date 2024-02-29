







function saveFollower() {
    //event.preventDefault();



    var myModel =
    {
        "Name": $('#NameF').val(),
        "Type": $('#TypeF').val(),
        "Email": $('#EmailF').val(),//
        "Nationality": $('#NationalityF').val(),
        "LocWork": $('#LocWorkF').val(),
        "PhoneWork": $('#PhoneWorkF').val(),
        "TypeProof": $('#TypeProofF').val(),
        "NumProof": $('#NumProofF').val(),
        "LocRelease": $('#LocReleaseF').val(),

        "ReleaseDate": $('#ReleaseDateF').val(),
        "EndDate": $('#EndDateF').val(),
        "PublicNote": $('#PublicNoteF').val(),

        "PrivateNote": $('#PrivateNoteF').val(),

        "IdAccount": null

        //,"IdSub": 22
    };
    //var jsonToPost = JSON.stringify(myModel);

    var url = '/Followers/CreateJson'; //$(this).attr("action"); @Url.Action("CreateJson", "Customers")
    //var formData = $(this).serialize();

    $.post(url, myModel, function (response) {

       // alert("id New Follower " + response.id);





        var naff = response.name;

        var idff = response.id;
        var reff = $("#RelationF").val();

        $('#tableFollowSelect').append('<tr><td>' + naff + '</td><td> <span class="badge badge-soft-success">' + reff + '</span></td><td> <a href="#"><i class="las la-pen text-secondary font-18"></i></a><button  class="btnDelete text-dark"> <i class="mdi mdi-close-circle-outline font-18"></i></button></td> <td >' + idff + '</td></tr>');
        $('#addFollowerModal').modal('hide');

        //

        //




        //window.location.href = '@Url.Action("Index", "_Company")';
        //  if (intervalId)
        //clearInterval(intervalId);
    });


}



$("#tableFollowSelect").on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
});




function bindDatatablefollow() {
    $('#FollowSearchModal').modal('show');



    $('#tableFollow')
        .DataTable({

            "sAjaxSource": '/Customers/GetDataFarz1/' + $("#typeFollowid").val(),
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

                //        return '<input type="button" class="btn btn-info" onclick="selectCustomer(' + full.name + ')" value="Save"/>';



                {
                    "data": "idcumtomerAll"
                    ,
                    "render": function (data, type, row, meta) {
                        //return '<a class='btn btn-default btn-sm' onclick=PopupForm('@Url.Action("AddOrEdit","Employee")/" + data + "')><i class='fa fa-pencil'></i> Edit</a>'

                        return '<button  data-id="' + row.name + '"  data-toggle="' + row.idcumtomerAll + '" onclick="selectFollowClick(event)">select</button>';

                        //btn = '<a class="btn btn-success"  onclick="deleteThis( \'' + row.name + '\' )"/><i class="fa fa-edit"></i> Edit</a>';
                        //return btn;

                    }


                }
                ,
                {
                    "data": "name",
                    "autoWidth": false,
                    "width": "40%",
                    "searchable": true
                },
                {
                    "data": "typeProof",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "numProof",
                    "autoWidth": true,
                    "searchable": true
                },

                {
                    "data": "phoneWork",
                    "autoWidth": true,
                    "searchable": true
                }, {
                    "data": "publicNote",
                    "autoWidth": true,
                    "searchable": true
                },
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


$('#typeFollowid').on('change', function (event) {
    var form = $(event.target).parents('form');


    $('#tableFollow').DataTable().ajax.url('/Customers/GetDataFarz1/' + $("#typeFollowid").val()).load();

    // $('#table13').DataTable().destroy();
    //$('#table13').DataTable().ajax.reload();


});



function selectFollowClick(e2) {
    //console.log(e.target.getAttribute('data-id'));
    //e.preventDefault();
    //console.log(JSON.stringify(row));
    //console.log(row);
    //console.log(row.id);
    //console.log(row.name);

    var row = e2.target.getAttribute('data-id');

    var row2 = e2.target.getAttribute('data-toggle');

    $("#nameFollowtxt").val(row);


    $("#followSelectid").val(row2);



    $("#okFollow").attr("disabled", false);

    //  var iu = JSON.stringify(row);



}

function selectFollowClickOk() {

    if ($("#relationtxt").val().length === 0) {
        $("#relationtxt").parents('p').addClass('warning');
        alert('الرجاء ادخال صلة القرابة');
    }
    else {
        var naff = $("#nameFollowtxt").val();

        var idff = $("#followSelectid").val();
        var reff = $("#relationtxt").val();

        $('#tableFollowSelect').append('<tr><td>' + naff + '</td><td> <span class="badge badge-soft-success">' + reff + '</span></td><td> <button  class="btnDelete"> <i class="mdi mdi-close-circle-outline font-18"></i></button></td> <td>' + idff + '</td></tr>');
               $('#FollowSearchModal').modal('hide');
    }
}


