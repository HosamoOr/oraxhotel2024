class CCustomer {
    CidCu;
    CidMy;
    Cname;
    CType;
    CEmail;
    CNationality;
    CTypeWork;
    CLocWork;
    CPhoneWork;
    CTypeProof;
    CNumProof;
    CLocRelease;

    
    CReleaseDate;
    CEndDate;
    CPublicNote;
    CPrivateNote;
    CIdAccount;
    CIdSub;
    Cis_my = false;
    CVisitEndDate;
    CId_Area;
    CTypeOP = "ADD";
    CIdNationality;
    constructor(id1,id2) {
        this.CidCu = id1;
        this.CidMy = id2;

    }

     setDataa(id11, id22) {
        this.CidCu = id11;
       
        this.CidMy = id22;

    }

    #privateMethod() {
        return 'hello world';
    }

    getPrivateMessage() {
        return this.CidCu;
    }


}



$(document).ready(function () {

    Cuinstance.CidMy = $('#customerMyID').val();
   // Cuinstance.CidCu = $('#customerID').val(); 

    Cuinstance.CIdAccount = $('#customerIDacc').val();

    Cuinstance.CPrivateNote = $('#PrivateNoteID').val();
  
    Cuinstance.Cis_my = $('#isMyID').val();



    $('#typeCustomerid').on('change', function (event) {
        var form = $(event.target).parents('form');


        $('#table13').DataTable().ajax.url('/Customers/GetDataFarz1/' + $("#typeCustomerid").val()).load();

        // $('#table13').DataTable().destroy();
        //$('#table13').DataTable().ajax.reload();


    });
    $('#divMy').hide();


    $('#ReleaseDate').on('change', function (event) {
       

        var now = new Date($('#ReleaseDate').val());
        now.setFullYear(now.getFullYear() + 10);

        now.setDate(now.getDate() - 1);

        var dateOptionsAA = { day: '2-digit', month: '2-digit', year: 'numeric' };
        var currentDateAA = now.toLocaleDateString('ja-JP', dateOptionsAA).replace(/\//gi, '-');
        $('#EndDateCustomer').val(currentDateAA);


    });

    bindDatatablecust();
    
});

//var superCustomer = new customerModel(0, 0, 0, "Init");
var Cuinstance = new CCustomer(0,0);

function editCusModelShow()
{
    var typePagee = $('#titlePageType').val();
    //alert(typePagee);
    if (isloadCoutry == false) {

        isloadCoutry = true;
        _getcountry();
    }
    

    //if (typePagee == 1) {
        //$('#customerID').val(Cuinstance.CidCu);
        $('#customerMyID').val(Cuinstance.CidMy);
        $('#NameCustomer').val(Cuinstance.Cname);
        $('#NameCustomer').addClass("is-valid").removeClass("is-invalid");

        $('#TypeCustomer').val(Cuinstance.CType);
        $('#TypeCustomer').addClass("is-valid").removeClass("is-invalid");

        $('#EmailID').val(Cuinstance.CEmail);
        $('#Nationality').val(Cuinstance.CNationality);
        $('#Nationality').addClass("is-valid").removeClass("is-invalid");

        $('#TypeWork').val(Cuinstance.CTypeWork);
        $('#TypeWork').addClass("is-valid").removeClass("is-invalid");

        $('#LocWork').val(Cuinstance.CLocWork);
        $('#LocWork').addClass("is-valid").removeClass("is-invalid");

        $('#PhoneWork').val(Cuinstance.CPhoneWork);
        $('#PhoneWork').addClass("is-valid").removeClass("is-invalid");

        $('#TypeProof').val(Cuinstance.CTypeProof);
        $('#TypeProof').addClass("is-valid").removeClass("is-invalid");

        $('#NumProof').val(Cuinstance.CNumProof);
        $('#NumProof').addClass("is-valid").removeClass("is-invalid");

        $('#LocRelease').val(Cuinstance.CLocRelease);
        $('#LocRelease').addClass("is-valid").removeClass("is-invalid");

       /* $('#ReleaseDate').val(Cuinstance.CReleaseDate);*/
        $('#ReleaseDate').addClass("is-valid").removeClass("is-invalid");

       // $('#EndDateCustomer').val(Cuinstance.CEndDate);
        $('#EndDateCustomer').addClass("is-valid").removeClass("is-invalid");


        $('#PublicNote').val(Cuinstance.CPublicNote);
        $('#PrivateNoteCustomer').val(Cuinstance.CPrivateNote);

    $('#Id_Area').val(Cuinstance.CId_Area);
    

        
       // alert(Cuinstance.Cis_my);

        if (Cuinstance.Cis_my == true) {

            $('#VisitEndDate').val(Cuinstance.CVisitEndDate);
            $('#IdAccountCustomer').val(Cuinstance.CIdAccount);
            $('#customerIDacc').val(Cuinstance.CIdAccount);

            $('#divMy').show();
        }
        else {
            $('#divMy').hide();
        }

    //}
    //else {

    //}

    //Cuinstance.CIdSub = 1;
    $("#titlNewCustomer").html("تعديل العميل");

    Cuinstance.CTypeOP = "UPDATE";
    $('#newCustoModals').modal('show');

    
}

function doneSelect(id_, idmy_, name_, idacc_, privateNote_,ismy_) {

   // $("#customerID").val(id_);
    $("#customerMyID").val(idmy_);
    $("#customerName").val(name_);
    $("#customerIDacc").val(idacc_);
    $("#PrivateNoteID").val(privateNote_);
   
    Cuinstance.CidCu = id_;
    Cuinstance.CidMy = idmy_;
    Cuinstance.CIdAccount = idacc_;

    Cuinstance.Cis_my = ismy_;

   

}

function saveCustomer15() {
    //event.preventDefault();


    //if ($('#NameCustomer').val().lenght ===0) {
    //    
    //}

    var validation = true;

    if ($("#NameCustomer").val().length === 0) {
        validation = false;
        $('#NameCustomer').addClass("is-invalid").removeClass("is-valid"); 
       
    }
    else {
        $('#NameCustomer').addClass("is-valid").removeClass("is-invalid");
    }

    if ($("#TypeCustomer").val() === "0") {
        validation = false;
        $('#TypeCustomer').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#TypeCustomer').addClass("is-valid").removeClass("is-invalid");
    }

    if ($("#Nationality").val() === "0") {
        validation = false;
        $('#Nationality').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#Nationality').addClass("is-valid").removeClass("is-invalid");
    }


    //if ($("#TypeWork").val().length === 0) {
    //    validation = false;
    //    $('#TypeWork').addClass("is-invalid").removeClass("is-valid");
    //}
    //else {
    //    $('#TypeWork').addClass("is-valid").removeClass("is-invalid");
    //}

    //if ($("#LocWork").val().length === 0) {
    //    validation = false;
    //    $('#LocWork').addClass("is-invalid").removeClass("is-valid");
    //}
    //else {
    //    $('#LocWork').addClass("is-valid").removeClass("is-invalid");
    //}

    if ($("#PhoneWork").val().length === 0) {
        validation = false;
        $('#PhoneWork').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#PhoneWork').addClass("is-valid").removeClass("is-invalid");
    }


    if ($("#TypeProof").val() === "0") {
        validation = false;
        $('#TypeProof').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#TypeProof').addClass("is-valid").removeClass("is-invalid");
    }


    if ($("#NumProof").val().length === 0) {
        validation = false;
        $('#NumProof').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#NumProof').addClass("is-valid").removeClass("is-invalid");
    }

    if ($("#LocRelease").val().length === 0) {
        validation = false;
        $('#LocRelease').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#LocRelease').addClass("is-valid").removeClass("is-invalid");
    }

    if ($("#ReleaseDate").val().length === 0) {
        validation = false;
        $('#ReleaseDate').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#ReleaseDate').addClass("is-valid").removeClass("is-invalid");
    }


    if ($("#EndDateCustomer").val().length === 0) {
        validation = false;
        $('#EndDateCustomer').addClass("is-invalid").removeClass("is-valid");
    }
    else {
        $('#EndDateCustomer').addClass("is-valid").removeClass("is-invalid");
    }

    if (validation == true) {

        

        //Cuinstance.CidCu = $('#customerID').val();
        Cuinstance.CidMy = $('#customerMyID').val();
        Cuinstance.Cname = $('#NameCustomer').val();
        Cuinstance.CType = $('#TypeCustomer').val();
        Cuinstance.CEmail = $('#EmailID').val();
        Cuinstance.CNationality = $('#Nationality').val();
        Cuinstance.CTypeWork = $('#TypeWork').val();

        Cuinstance.CLocWork = $('#LocWork').val();
        Cuinstance.CPhoneWork = $('#PhoneWork').val();
        Cuinstance.CTypeProof = $('#TypeProof').val();
        Cuinstance.CNumProof = $('#NumProof').val();
        Cuinstance.CLocRelease = $('#LocRelease').val();
        Cuinstance.CReleaseDate = $('#ReleaseDate').val();
        Cuinstance.CEndDate = $('#EndDateCustomer').val();

        Cuinstance.CPublicNote = $('#PublicNote').val();
        Cuinstance.CPrivateNote = $('#PrivateNoteCustomer').val();
        Cuinstance.CIdAccount = $('#customerIDacc').val();

        Cuinstance.CId_Area=  $('#Id_Area').val();

        Cuinstance.CIdSub = 1;

        if (Cuinstance.CTypeOP == "ADD") {
            Cuinstance.CidCu = 0;
            Cuinstance.CidMy = 0;
            Cuinstance.CIdAccount = 0;

        }

        var nationa = $('#country').find('option:selected').text();

        var id_nationa = $('#country').val();

        Cuinstance.CIdNationality = id_nationa;

        var myModel =
        {
            "IdcumtomerAll": Cuinstance.CidCu,
            "IdmyCu": Cuinstance.CidMy,
            "Name": Cuinstance.Cname,
            "Type": Cuinstance.CType,
            "Email": Cuinstance.CEmail,//
            "Nationality": nationa,
            "TypeWork": Cuinstance.CTypeWork,
            "LocWork": Cuinstance.CLocWork,
            "PhoneWork": Cuinstance.CPhoneWork,
            "TypeProof": Cuinstance.CTypeProof,
            "NumProof": Cuinstance.CNumProof,
            "LocRelease": Cuinstance.CLocRelease,

            "ReleaseDate": Cuinstance.CReleaseDate,
            "EndDate": Cuinstance.CEndDate,
            "PublicNote": Cuinstance.CPublicNote,

            "PrivateNote": Cuinstance.CPrivateNote,

            "IdAccount": Cuinstance.CIdAccount,
            "is_my": Cuinstance.Cis_my,
            "Id_Area": Cuinstance.CId_Area,
            "IdNationality": Cuinstance.CIdNationality,

            "IdSub": Cuinstance.CIdSub
        };


        var url = '/Customers/CreateJson';  //@Url.Action("CreateJson", "Customers") //$(this).attr("action");
        //var formData = $(this).serialize();

       

        $.post(url, myModel, function (response) {
            $('#newCustoModals').modal('hide');

            if (response.status == 0) {

                //Swal.fire(
                //    'تم الحفظ!',
                //    'تم حفظ العميل بنجاح.',
                //    'success'
                //);

                executeExample('mixin');

               // $('#table').bootstrapTable('refresh');

                $('#table').DataTable().ajax.reload(null, false);

                $('#table13').DataTable().ajax.reload(null, false);

                

                $("#idareaHid").val("-1").change();
                $("#idcityHid").val("-1").change();


            }
            else if (response.status == -1)
            {

                Swal.fire({
                    title: 'بيانات مكررة?',
                    text: "بيانات العميل مضافة مسبقا" + " !",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'اختيار العميل!',
                }).then(function (result) {
                    if (result.isConfirmed) {

                        //doneSelect(response.id, response.idmy, response.name, response.idacc, response.privateNote, Cuinstance.Cis_my);
                       


                    }
                })
            }
            else if (response.status == -2) {

                Swal.fire({
                    title: 'بيانات مكررة?',
                    text: response.mess + " رقم الهوية تتبع اسم عميل اخر!",
                   
                    icon: 'info',
                    html:
                        'رقم الهوية تتبع اسم عميل اخر  <b>'  + '</b>' +
                        ' علي: ' + '<b style="font-size:22px;color:red">' + response.name + '</b> \n' + response.idacc + '' +
                        '',
                    showCloseButton: true,
                    showCancelButton: true,
                    focusConfirm: false,
                    confirmButtonText:
                        '<i class="fa fa-save" style="font-size:22px;color:blue"></i> اختيار العميل!',
                    confirmButtonAriaLabel: ' اختيار العميل!',
                    cancelButtonText:
                        '<i class="fa fa-close" style="font-size:22px;color:red"></i> الغاء ',
                    cancelButtonAriaLabel: 'الغاء'


                }).then(function (result) {
                    if (result.isConfirmed) {

                       // doneSelect(response.id, response.idmy, response.name, response.idacc, response.privateNote, Cuinstance.Cis_my);

                    }
                })


            }
            else if (response.status == -3) {
              


                Swal.fire({
                    text: response.mess + "اسم العميل مدخل مسبقا ولكن بهوية اخرى !",
                    title: 'بيانات مكررة?',
                    icon: 'info',
                    html:
                        'هل تريد تعديل بيانات العميل او اختيار العميل الموجود مسبقا بنفس الاسم  <b>'  + '</b>' +
                        ' : ' + '<b style="font-size:22px;color:red">' + response.name + '</b> \n' + response.idacc + '' +
                        '',
                    showCloseButton: true,
                    showCancelButton: true,
                    focusConfirm: false,
                    confirmButtonText: '<i class="fa fa-close" style="font-size:22px;color:red"></i>  تعديل بيانات العميل ',
                        
                    confirmButtonAriaLabel: 'تعديل بيانات العميل' ,
                    cancelButtonText:
                        '<i class="fa fa-save" style="font-size:22px;color:blue"></i> اختيار بيانات العميل السابقة !',
                      
                    cancelButtonAriaLabel: ' اختيار بيانات العميل السابقة!'
                }).then(function (result) {
                    if (result.isConfirmed) {
                        $("#customerName").val(response.name);

                        $("#customerID").val(response.idcumtomerAll);

                        $("#PrivateNoteID").val(response.model.privateNote);
                        $('#customerIDacc').val(response.idacc);



                        Cuinstance.CidCu = response.id;
                        Cuinstance.CidMy = response.idmy;
                        Cuinstance.Cname = response.name;
                        Cuinstance.CType = response.model.type;
                        Cuinstance.CEmail = response.model.email;
                        Cuinstance.CNationality = response.model.nationality;
                        Cuinstance.CTypeWork = response.model.typeWork;

                        Cuinstance.CLocWork = response.model.locWork;
                        Cuinstance.CPhoneWork = response.model.phoneWork;
                        //Cuinstance.CTypeProof = response.model.typeProof;
                        //Cuinstance.CNumProof = response.model.numProof;
                        Cuinstance.CLocRelease = response.model.locRelease;

                        var now = new Date(response.model.releaseDate);

                        var dateOptionsAA = { day: '2-digit', month: '2-digit', year: 'numeric' };
                        var currentDateAA = now.toLocaleDateString('ja-JP', dateOptionsAA).replace(/\//gi, '-');

                        $('#ReleaseDate').val(currentDateAA);


                        var now2 = new Date(response.model.endDate);


                        var dateOptionsB = { day: '2-digit', month: '2-digit', year: 'numeric' };
                        var currentDateB = now2.toLocaleDateString('ja-JP', dateOptionsB).replace(/\//gi, '-');

                        $('#EndDateCustomer').val(currentDateB);

                        Cuinstance.CReleaseDate = currentDateAA;
                        Cuinstance.CEndDate = currentDateB;

                        

                        Cuinstance.CPublicNote = response.model.publicNote;
                        Cuinstance.CPrivateNote = response.model.privateNote;
                        Cuinstance.CIdAccount = response.model.idAccount;
                        Cuinstance.Cis_my = true;
                        Cuinstance.CVisitEndDate = response.model.visitEndDate;
                        Cuinstance.CId_Area = response.model.Id_Area;

                        Cuinstance.CIdSub = 1;

                        
                      //  $('#titlePageType').val("1");
                        Cuinstance.CTypeOP = "UPDATE";

                        editCusModelShow();
                        //  doneSelect(row.idcumtomerAll, row.idmyCu, row.name, row.idAccount, row.privateNote, row.is_my);

                        //e.preventDefault();


                    }
                    else {

                        $('#table').DataTable().ajax.reload(null, false);

                        executeExample('mixin');

                        //Swal.fire(
                        //    'تم الحذف!',
                        //    'تم حفظ العميل بنجاح.',
                        //    'success'
                        //);
                       
                    }
                })
            }
            else {

               
            }
           
        });
    }

    


}


function newCusModelShow() {
  
    if (isloadCoutry == false) {

        isloadCoutry = true;
        _getcountry();
    }
  
    $('#NameCustomer').val('');
    // $('#TypeCustomer').val();
    $('#EmailID').val('');
    //$('#Nationality').val('');
    $('#TypeWork').val('');
    
    $('#LocWork').val('');
     $('#PhoneWork').val('');
     //$('#TypeProof').val('');
     $('#NumProof').val('');
    $('#LocRelease').val('');
    // $('#ReleaseDate').val();
   //$('#EndDateCustomer').val();

    $('#PublicNote').val('');
    $('#PrivateNoteCustomer').val('');
    // $('#customerIDacc').val();
    $('#titlNewCustomer').html('اضافة عميل');
   
  
        
    Cuinstance.CTypeOP = "ADD";
    $('#divMy').hide();



    $('#NameCustomer').removeClass("is-invalid").removeClass("is-valid");

    $('#TypeCustomer').removeClass("is-invalid").removeClass("is-valid");
   
    $('#Nationality').removeClass("is-invalid").removeClass("is-valid");
    $('#TypeWork').removeClass("is-invalid").removeClass("is-valid");
    $('#LocWork').removeClass("is-invalid").removeClass("is-valid");
    

    $('#PhoneWork').removeClass("is-invalid").removeClass("is-valid");
    
   
    $('#TypeProof').removeClass("is-invalid").removeClass("is-valid");
    
    $('#NumProof').removeClass("is-invalid").removeClass("is-valid");
    
    $('#LocRelease').removeClass("is-invalid").removeClass("is-valid");
    $('#ReleaseDate').removeClass("is-invalid").removeClass("is-valid");
    $('#EndDateCustomer').removeClass("is-invalid").removeClass("is-valid");

   


    
   
  //  _getCity();

    $('#newCustoModals').modal('show');


}

var isloadData = false;



function bindDatatablecust() {
   // $('#cuSearchModal').modal('show');

   // $('#titlNewCustomer')

    

    if (isloadData == false) {
        isloadData = true;

        var dt = $('#table13')
            .DataTable({

                "sAjaxSource": '/Customers/GetDataFarz1/' + $("#typeCustomerid").val(),
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
                        "data": "name",
                        "autoWidth": false,
                        "width": "35%",
                        "searchable": true
                    },
                    {

                        "render": function (data, type, row, meta) {

                            if (row.typeProof == '1') {
                                return '<a>الهوية</a>';

                            }
                            else if (row.typeProof == '2') {
                                return '<a> جواز سفر</a>';

                            }
                            else {
                                return '<a>...</a>';
                            }

                        }
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
                    },
                    {
                        "render": function (data, type, row, meta)  {

                            if (row.visitEndDate == null)

                                return '<p>لا زيارات </p>';
                            else
                                return row.visitEndDate;
                        }
                    },
                    
                     {
                         "data": "email",
                        "autoWidth": true,
                        "searchable": true
                    },

                    {
                        "data": "publicNote",
                        "autoWidth": true,
                        "searchable": true
                    },
                    {
                        "data": "nationality",
                        "autoWidth": true,
                        "searchable": true
                    },
                    {
                        "data": "idNationality",
                        "autoWidth": true,
                        "searchable": true
                    },
                    {
                        "render": function (data, type, row, meta) {
                            return '<a>' + row.area.name + '-' + row.area.nameCity + '</a>';

                        }
                    },

                    {
                        "render": function (data, type, row, meta) {

                            return '<a class="btn btn-default btn-sm"   onclick="selecteEdit(JSON.parse(\'' + JSON.stringify(row).replace(/'/g, '&apos;').replace(/"/g, '&quot;') + '\'))"><i class="fa fa-edit"></i></a><a class="btn  btn-sm" href="/Customers/Delete/' + row.idmyCu + '"><i class="fa fa-trash" style="color: #ea0606;"></i></a>';


                        }
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

    else {

        $('#table13').DataTable().ajax.reload(null, false);
    }

   

    }

var idCityforCu = -1;
var idAreaforCu = -1;


var isloadCoutry = false;

function selecteEdit(row) {

   

    $("#idcountryHid").val(row.idNationality);

    if (isloadCoutry == false) {


        _getcountry();
        isloadCoutry = true;


    }
    else {
       

        $("#idcountryHid").val(row.idNationality);

        $("#country").val(row.idNationality).change();
       
    }

   

    //var row = e.target.getAttribute('data-id');

    //var row2 = e.target.getAttribute('data-toggle');

    //var row3 = e.target.getAttribute('data-privateNote');

    idCityforCu = row.area.idCity;

    idAreaforCu = row.area.id;

    Cuinstance.CId_Area = idAreaforCu;

    $("#idcityHid").val(idCityforCu);

    $("#idareaHid").val(idAreaforCu);

    //if (row.area != null) {
    //    idCountryCu = row.area.idCountry;
    //}
    //else {
    //    idCountryCu = row.idNationality;
        
    //}
  


    $("#customerName").val(row.name);

    $("#customerID").val(row.idcumtomerAll);

  //  alert($("#customerID").val());

   

    $("#PrivateNoteID").val(row.privateNote);
    $('#customerIDacc').val(row.idAccount);

    

    Cuinstance.CidCu = row.idcumtomerAll;
    Cuinstance.CidMy = row.idmyCu;
    Cuinstance.Cname = row.name;
    Cuinstance.CType = row.type;
    Cuinstance.CEmail = row.email;
    Cuinstance.CNationality = row.nationality;
    Cuinstance.CTypeWork = row.typeWork;

    Cuinstance.CLocWork = row.locWork;
    Cuinstance.CPhoneWork = row.phoneWork;
    Cuinstance.CTypeProof = row.typeProof;
    Cuinstance.CNumProof = row.numProof;
    Cuinstance.CLocRelease = row.locRelease;

   
  //Cuinstance.CReleaseDate = new Date(row.releaseDate);
  //  Cuinstance.CEndDate = new Date(row.endDate);


    var now = new Date(row.releaseDate);
  

    var dateOptionsAA = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDateAA = now.toLocaleDateString('ja-JP', dateOptionsAA).replace(/\//gi, '-');

    $('#ReleaseDate').val(currentDateAA);


    var now2 = new Date(row.endDate);


    var dateOptionsB = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDateB = now2.toLocaleDateString('ja-JP', dateOptionsB).replace(/\//gi, '-');

    $('#EndDateCustomer').val(currentDateB);


    Cuinstance.CReleaseDate = currentDateAA;
    Cuinstance.CEndDate = currentDateB;

   
    Cuinstance.CPublicNote = row.publicNote;
    Cuinstance.CPrivateNote = row.privateNote;
    Cuinstance.CIdAccount = row.idAccount;
    Cuinstance.Cis_my = row.is_my;
    Cuinstance.CVisitEndDate = row.visitEndDate;

    Cuinstance.CIdSub = 1;

  
  
    editCusModelShow();

    


   
   // _getCity();

   // doneSelect(row.idcumtomerAll, row.idmyCu, row.name, row.idAccount, row.privateNote, row.is_my);

    //e.preventDefault();

    //$('#cuSearchModal').modal('hide');


}


function selectCustomerClick(row) {
  //  alert(idCountryCu);
  
    if (isloadCoutry == false) {

        isloadCoutry = true;
        _getcountry();
    }
    //var row = e.target.getAttribute('data-id');

    //var row2 = e.target.getAttribute('data-toggle');

    //var row3 = e.target.getAttribute('data-privateNote');


   
    $("#customerName").val(row.name);

    $("#customerID").val(row.idcumtomerAll);

    $("#PrivateNoteID").val(row.privateNote);
    $('#customerIDacc').val(row.idAccount);
    


    Cuinstance.CidCu = row.idcumtomerAll;
    Cuinstance.CidMy = row.idmyCu;
    Cuinstance.Cname = row.name;
    Cuinstance.CType = row.type;
    Cuinstance.CEmail = row.email;
    Cuinstance.CNationality = row.nationality;
    Cuinstance.CTypeWork = row.typeWork;
    
    Cuinstance.CLocWork = row.locWork;
    Cuinstance.CPhoneWork = row.phoneWork;
    Cuinstance.CTypeProof = row.typeProof;
    Cuinstance.CNumProof = row.numProof;
    Cuinstance.CLocRelease = row.locRelease;

    var startSplit = row.releaseDate.split("-");
    var f1 = new Date(startSplit[2], startSplit[1] - 1, startSplit[0]);

    Cuinstance.CReleaseDate = new Date(row.releaseDate);
    Cuinstance.CEndDate = new Date(row.endDate);

    Cuinstance.CPublicNote = row.publicNote;
    Cuinstance.CPrivateNote = row.privateNote;
    Cuinstance.CIdAccount = row.idAccount;
    Cuinstance.Cis_my = row.is_my;
    Cuinstance.CVisitEndDate = row.visitEndDate;

    Cuinstance.CIdSub = 1;
    Cuinstance.CIdNationality = row.idNationality;

    doneSelect(row.idcumtomerAll, row.idmyCu, row.name, row.idAccount, row.privateNote, row.is_my);

     //e.preventDefault();
    



    $('#cuSearchModal').modal('hide');


}



function showDataCo() {


    $('#cuSearchModal').modal('show');


    $('#table13').bootstrapTable('destroy');

}
