
var statuAccto2 = true;

$(document).ready(function () {


    $('#TypePayE').on('change', function (event) {
       // var form = $(event.target).parents('form');

        var target = $('#TypePayE option:selected').val();
        if (target == "3") {
            $('#NumCheckEForm').show();
            $('#NumCardEForm').hide();
            $('#IdBankEForm').show();
        }
        else if (target == "4") {
            $('#NumCheckEForm').hide();
            $('#NumCardEForm').show();
            $('#IdBankEForm').hide();
        }
        else {
            $('#NumCheckEForm').hide();
            $('#NumCardEForm').hide();
            $('#IdBankEForm').hide();
        }
       

    });
    


});


function saveBondPay() {

    var cuorCo1 = "cu";
    var idacc0 = Cuinstance.CIdAccount;//$('#customerID').val();

    if ($('#companyID').val().length > 0) {
        cuorCo1 = "co";
        idacc0 = $('#companyIDacc').val();
    }

    var pricePP = $('#AmountE').val();

    var myModel =
    {
        "Id": 0,
        "Type": $('#TypeE').val(),
        "TypePay": $('#TypePayE').val(),
        //"NumReference": $('#NumReferenceT').val(),
        "Date": $('#DateE').val(),
        "Amount": $('#AmountE').val(),
        "Why": $('#WhyE').val(),
        "NumCheck": $('#NumCheckE').val(),
        "NumCard": $('#NumCardE').val(),
        "Note": $('#NoteE').val(),
        "IdBank": $('#IdBankE').val(),
        //"IdCurrancy": $('#IdCurrancyT').val(),
        "id_accountForm": idacc0 ,
        "id_accountTo": $('#id_accountToE').val(),

        "IdReception": $('#idReception').val(),
        "IdAccount": idacc0,
        "Time": $('#TimeE').val(),
        "customerOrCompany": cuorCo1

    };

    var url = '/Bond/Createjson';

    //alert($('#sourceBondEX').val());

    if ($('#sourceBondEX').val() == 'fromLogin') {
        $.post(url, myModel, function () {



        }).done(function (data_2_) {
            alert("id  " + data_2_.id);

            $("#result").html(data_2_.mass + data_2_.id + "  (سند صرف)");

            // $("#result").html('');

            if (data_2_.id > 0) {

                //adding class
                $("#result").addClass("alert alert-success");
                // $('#idReception').val(response.id);


                var v1 = parseFloat($('#totalE').text());
                var aomoutpp = parseFloat($('#AmountE').val());

                $('#totalE').text(v1 + aomoutpp);


                var v2 = parseFloat($('#balance').text());
                //var v4 = parseFloat($('#totalE').text());

                var pm = parseFloat(v2 - aomoutpp);

                $('#balance').text(pm);

                $('#addDoadExModal').modal('hide');

                var typepayp = "نقدا";
                if ($('#TypePayE').val() == "3") {
                    typepayp = "تحويل بنكي";
                } else if ($('#TypePayE').val() == "4") {
                    typepayp = "بطاقة";
                }

                $('#compoboxT').append("<option value='" + data_2_.id + "'>" + data_2_.id + " | " + pricePP + " ( " + typepayp + " )" + "</option>");


            }
            else {

                $("#result").addClass("alert alert-danger");

            }



        }).fail(function () {
            alert("error");
        })
            .always(function () {
                //alert("finished");
            });
    }
    else {
        var url3 = '/LogoutCustomer/LogOut';
        var myModelLogout =
        {
            bondViewModel: myModel,
            IdReception: $('#idReception').val(),
            IdRoom: $('#roominputid').val(),
            IdAccount: idacc0,
            IdSub: 1

        };


        $.post(url3, myModelLogout, function () {


        }).done(function (data_02) {

            if (data_02.status == true) {
                alert(data_02.mass);

                // عرض مودال لملخلص الفاتورة -حقول المستندات من ملخص العقد+ هذا السند
                $(location).prop('href', '/HomeReception/Index')
            }



        }).fail(function () {
            alert("error");
        })
            .always(function () {
                //alert("finished");
            });

    }

   
}

function ___getBoxs() {
    var url = '/_Accounts/GetBoxs/';
    // $('.loading').show();


    $.post(url, function () {

        // $('.loading').hide();

    })
        .done(function (data) {

            var response = jQuery.parseJSON(data);


            for (var i = 0; i < response.length; i++) {
                // alert(response[i].Name);


                $('#id_accountToE').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


            }
            statuAccto2 = false;


        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

}


function showPayDoad() {

    //event.preventDefault();
   
    
    $('#addDoadExModal').modal('show');

    //$('#NoteE').val('سند صرف ل ايجار  الشقة' + $('#roominputid').val());
    $('#WhyE').val('سند صرف للعميل - الشقة' + $('#roominputid').val());

   
    $('#nameaccountE').val($("#customerName").val());
   // $('#id_accountToE').val('الصندوق الرئيسي');
    $('#AmountE').val(0);
    $('#sourceBondEX').val('fromLogin');


    $("#AmountE").prop('disabled', false);
    $("#WhyE").prop('disabled', false);





    // var currentDate = now.toISOString().substring(0, 10);
    // var currentTime = now.toISOString().substring(11, 16);

    var now = new Date();

    var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDate = now.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    var timeOptions = { hour: '2-digit', minute: '2-digit' };
    var currentTime = now.toLocaleTimeString('it-IT', timeOptions);


    $('#TimeE').val(currentTime);
    $('#DateE').val(currentDate);


    $('#NumCheckEForm').hide();
    $('#NumCardEForm').hide();
    $('#IdBankEForm').hide();

    if (statuAccto2 == true) {
        ___getBoxs();
    }

    //NumCardT
    //
}

