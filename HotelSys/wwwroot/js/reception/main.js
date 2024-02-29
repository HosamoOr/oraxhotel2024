
function saveReception() {

   
    var validation_ = true;

    if ($("#customerName").val().length === 0) {
        validation_ = false;
        $('#customerName').addClass("is-invalid").removeClass("is-valid");

    }
    if ($("#roominputid").val() == '-1' || $("#roominputid").val() == '0') {
        validation_ = false;
        $('#roominputid').addClass("is-invalid").removeClass("is-valid");

    }

    if (validation_ == true) {

        var Data = [];
        $("#step2-tab").removeClass('disabled');

        $("#tableFollowSelect tbody tr").each(function () {
            var cnts = $(this).find('td:eq(0)').text();
            var crelat = $(this).find('td:eq(1)').text();
            var cidf = $(this).find('td:eq(3)').text();

            Data.push({
                'follwerCusomer': {
                    'IdcumtomerAll': cidf,
                    'Name': cnts
                },
                'Relation': crelat,

            });



        });

        var cuorCo1 = "cu";
        var idacc = $('#customerIDacc').val();



        if ($('#companyID').val().length > 0 || $('#companyID').val() != 0) {
            cuorCo1 = "co";
            idacc = $('#companyIDacc').val();
        }
        var total_ = parseFloat($('#totalB').text());
        var finishtotal_ = parseFloat($('#finishtotal').text());

        var dicount_ = $("#QtyDiscount").val();;
        var typeDis_ = $('#TypeDiscount').val();

       

        var totaltaxprice_ = parseFloat($('#TotalTaxPrice').val());

      
        var totaltaxRate_ = $('#TotalTaxRate').val();

        var isIncludTax_ = $('#IncludeTax').val();

        var totalBaladiPrice_ = parseFloat($('#totalBaladiTax').text());

        var totalBaladiRate_ = $('#TotalBaladiTaxRate').val();


        var isBaladiTax_ = $('#IsBaladiTax').val();


        

        var bill_ =
        {
            "Id": $('#idBillID').val(),
            "Total": total_,
            "DeserveAmount": finishtotal_,
            "IdAccount": idacc,
            "CustomerOrCompany": cuorCo1,
            "IdCurrancy": 1,

            "TypeDiscount": typeDis_,
            "QtyDiscount": dicount_,
            "Type": 5,
            "TotalTaxPrice": totaltaxprice_,
            "TotalTaxRate": totaltaxRate_,
            "IncludeTax": isIncludTax_,
            "IsBaladiTax": isBaladiTax_,
            "TotalBaladiTaxPrice": totalBaladiPrice_,
            "TotalBaladiTaxRate": totalBaladiRate_


        };

        var customer_ =
        {
            "IdmyCu": Cuinstance.CidMy,
            "IdcumtomerAll": Cuinstance.CidCu,
            "IdAccount": Cuinstance.CIdAccount,
            "PrivateNote": Cuinstance.CPrivateNote,
            "IdSub": Cuinstance.CIdSub,
            "is_my": Cuinstance.Cis_my

        };

        //alert($('#roominputid').val());
       
        if ($('#idstatusReception').val() == "0") {

            $('#idstatusReception').val("1");
        }

        

        var myModel =
        {
            "IdReception": $('#idReception').val(),
            "Source": $('#Source').val(),
            "Price": $('#txtPrice').val(),
            "QtyTime": $('#QtyTime').val(),
            "Unit": $('#Unit').val(),
            "StartDate": $('#StartDate').val(),
            "EndDate": $('#EndDate').val(),
            "TypeDate": $('#TypeDate').val(),
            "IdCo": $('#companyID').val(),
            "IdCustomer": $('#customerID').val(),
            "IdRoom": $('#roominputid').val(),
            "NameCustomer": $('#customerName').val(),
            // "PrivateNote": $('#PrivateNoteID').val(),
            //"cuIsMY": Cuinstance.Cis_my,

            "IDAccountReception": idacc,
            "titlePageType": $('#titlePageType').val(),

            "status": $('#idstatusReception').val(),
            "AreaFrom": $('#AreaFrom').val(),
            "WhyVisit": $('#WhyVisit').val(),

            "bill": bill_,
            "customer": customer_,
            "followers": Data
        };

        var url = '/Reception/Create'; //$(this).attr("action"); @Url.Action("Create", "Reception")
        // var formData = $(this).serialize();

        $.post(url, myModel, function (response) {

            //alert("id  " + response.id);

            $("#result").html(response.mass + response.id);

            if (response.id > 0) {

                //adding class
                $("#result").addClass("alert alert-success");

                $('#idReception').val(response.id);
                $('#customerIDacc').val(response.idCustomerAccount);
                Cuinstance.CIdAccount = response.idCustomerAccount;

                if ($('#titlePageType').val() == "1") {
                    $('#idBillID').val(response.idBill);

                    $('#step1-tab1').removeClass("active");
                    $('#step2-tab2').addClass("active");

                    $('#step1').removeClass("active");
                    $('#step2').addClass("active");

                    $('#customerName').removeClass("is-invalid");
                    $('#roominputid').removeClass("is-invalid");
                    
                }
               
                checkSttusReception();

            }
            else {

                $("#result").addClass("alert alert-danger");


            }


        });
    }
}


function EnterRoom() {
    $('#idstatusReception').val(2);
    
    var myModel =
    {
        "IdReception": $('#idReception').val(),
        "IdBill": $('#idBillID').val(),
        "IdRoom": $('#roominputid').val(),
        "StartDate": $('#StartDate').val(),
        "EndDate": $('#EndDate').val()
       
    };
    var url = '/Reception/EnterRoom';
    $.post(url, myModel, function (response) {

        //alert("id  " + response.id);

        $("#result").html(response.mass + response.id);

        if (response.id > 0) {


            $("#result").addClass("alert alert-success");

           

            checkSttusReception();


          

        }
        else {

            $("#result").addClass("alert alert-danger");

        }


    });
}



$(document).ready(function () {

    init();


});
function checkSttusReception() {
    var statusss=  $('#idstatusReception').val();
    $('#divCencel').hide();
    $('#divOut').hide();
    
    if (statusss == "1") {
        $('#divEnter').show();
        $('#divOut').hide();
        $('#divCencel').show();
        $('#txtstatusReception').val("حجز مؤقت");
        $('#btnenterRoom').prop('disabled', false);
        $('#privew_REbtn').prop('disabled', true);

        

    }
    else if (statusss == "2") {
        $('#divEnter').hide();
        $('#divOut').show();
        $('#txtstatusReception').val("تسجيل الدخول");

        $('#btnenterRoom').prop('disabled', true);
        $('#privew_REbtn').prop('disabled', false);


    }
    else if (statusss == "3") {
        $('#divEnter').hide();
        $('#divOut').hide();
        $('#txtstatusReception').val("تسجيل الخروج");
    }
    else if (statusss == "4") {
        $('#divEnter').hide();
        $('#divOut').hide();
        $('#txtstatusReception').val("ملغي");
    }
    else {
        $('#txtstatusReception').val("حجز جديد");
    }
}
function addQtyDay(dayn) {

    var now = new Date($('#StartDate').val());
    now.setDate(now.getDate() + dayn);

    var day = ("1" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var dayp = parseInt(day);//+ days1;




    var today = now.getFullYear() + "-" + (month) + "-" + (dayp);
    $('#EndDate').val(today);

    // return today;
}


function addOneDay() {

    

    var now = new Date($('#EndDate').val());
    now.setDate(now.getDate() + 1);

    var dateOptionsAA = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDateAA = now.toLocaleDateString('ja-JP', dateOptionsAA).replace(/\//gi, '-');
    $('#EndDate').val(currentDateAA);


    //var day = ("1" + now.getDate()).slice(-2);
    //var month = ("0" + (now.getMonth() + 1)).slice(-2);
    //var dayp = parseInt(day);//+ days1;
   
    //var today = now.getFullYear() + "-" + (month) + "-" + (dayp);
    //$('#EndDate').val(today);

   // return today;
}

function intitDate() {

    //var nowTT = new Date();

    //var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    //var currentDateTT = nowTT.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    //$('#StartDate').val(currentDateTT);

    //var now = new Date();
    //now.setDate(now.getDate() + 1);

    //var dateOptionsEE = { day: '2-digit', month: '2-digit', year: 'numeric' };
    //var currentDateEE = now.toLocaleDateString('ja-JP', dateOptionsEE).replace(/\//gi, '-');
    //$('#EndDate').val(currentDateEE);

   
   
  
    //var qtyDefoult = 1;

    //$('#QtyTime').val(qtyDefoult);

   var qtyDefoult = $('#QtyTime').val();
    var pric = parseFloat($('#txtPrice').val());

    var dicount = parseFloat($('#QtyDiscount').val());

    var sumServicc = parseFloat($('#sumServicesHidden').val());
    $('#sumServices').text(sumServicc);

    var sumServicc = parseFloat($('#totalEHidden').val());
    $('#totalE').text(sumServicc);


    var sumServicc = parseFloat($('#totalTHidden').val());
    $('#totalT').text(sumServicc);

    

    updatePrice(pric, qtyDefoult, dicount);

    if ($('#titlePageType').val() == '2') {
            $('#btnRebortBill').prop('disabled', false);
    }


    checkSttusReception();
     

    //$('#divOut').hide();
    //$('#divEnter').show();
    
    

}





function SUBOneDay() {

    // var now = new Date();

    var now1 = new Date($('#EndDate').val());//new Date();
    now1.setDate(now1.getDate() - 1);


    var dateOptionsAA = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDateAA = now1.toLocaleDateString('ja-JP', dateOptionsAA).replace(/\//gi, '-');
    $('#EndDate').val(currentDateAA);


    //var day = ("1" + now1.getDate()).slice(-2);
    //var month = ("0" + (now1.getMonth() + 1)).slice(-2);
    //var dayp = parseInt(day);//+ days1;
   

    //var today = now1.getFullYear() + "-" + (month) + "-" + (dayp);
    //$('#EndDate').val(today);
}


function init() {
    intitDate();
   
}


function showcancelReceModal() {
    $('#summaryCancelModal').modal('show');
}


function showReportReception() {


    

    $('.IdReceptionRep').text($('#IdReception').val());

    $('.TotalTaxPriceRep').text($('#TotalTaxPrice').val());

    $('#customerNameRep').text($('#customerName').val());

    

    //alert($('#roominputid').val());


    $('.roominputidRep').text($('#roominputid').val());
    $('.QtyTimeRep').text($('#QtyTime').val());
    $('.totalDiscountRep').text($('#totalDiscount').text());


    $('.StartDateRep').text($('#StartDate').val());

    $('.EndDateRep').text($('#EndDate').val());
    $('.UnitRep').text($('#Unit').val());

    $('.txtPriceRep').text($('#txtPrice').val());

    $('.finishtotalbeforVATRep').text($('#finishtotalbeforVAT').text());

    

    
    $('#reportReceptionModal').modal('show');
    
}

function showReportInvReception() {

   

    $('#reportInvReceptionModal').modal('show');

}

//



showInPopupRes = (url, title) => {

    //alert(url);

    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#print-modal .modal-body').html(res);
            $('#print-modal .modal-title').html(title);
            $('#print-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

