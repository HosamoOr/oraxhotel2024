
$(document).ready(function () {


    $('#TypePayT').on('change', function (event) {
       // var form = $(event.target).parents('form');

        var target = $('#TypePayT option:selected').val();
        if (target == "3") {
            $('#NumCheckTForm').show();
            $('#NumCardTForm').hide();
            $('#IdBankTForm').show();
        }
        else if (target == "4") {
            $('#NumCheckTForm').hide();
            $('#NumCardTForm').show();
            $('#IdBankTForm').hide();
        }
        else {
            $('#NumCheckTForm').hide();
            $('#NumCardTForm').hide();
            $('#IdBankTForm').hide();
        }
       

    });
    


});

var statuAccto1 = true;

function __getBoxs() {
    var url = '/_Accounts/GetBoxs/';
    // $('.loading').show();


    $.post(url, function () {

        // $('.loading').hide();

    })
        .done(function (data) {

            var response = jQuery.parseJSON(data);


            for (var i = 0; i < response.length; i++) {
                // alert(response[i].Name);


                $('#accountFormT_').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


            }
            statuAccto1 = false;


        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

}



function saveBondTalk() {

    var cuorCo1 = "cu";
    var idacc0 = Cuinstance.CIdAccount;// $('#customerID').val();

    if ($('#companyID').val().length > 0) {
        cuorCo1 = "co";
        idacc0 = $('#companyIDacc').val();
    }

    var priceEE = $('#AmountT').val();

    var myModel =
    {
        "Id": 0,
        "Type": $('#TypeT').val(),
        "TypePay": $('#TypePayT').val(),
        //"NumReference": $('#NumReferenceT').val(),
        "Date": $('#DateT').val(),
        "Amount": priceEE,
        "Why": $('#WhyT').val(),
        "NumCheck": $('#NumCheckT').val(),
        "NumCard": $('#NumCardT').val(),
        "Note": $('#NoteT').val(),
        "IdBank": $('#IdBankT').val(),
        //"IdCurrancy": $('#IdCurrancyT').val(),
        "id_accountForm": $('#accountFormT_').val(),
        "id_accountTo": idacc0,
        "Time": $('#TimeT').val(),

        "IdReception": $('#idReception').val(),
        "IdAccount": idacc0,

        "customerOrCompany": cuorCo1

    };



    var url = '/Bond/Createjson';

    if ($('#sourceBondT').val() == 'fromLogin') {


        $.post(url, myModel, function () {


        }).done(function (data_1) {
            alert("id  " + data_1.id);

            $("#result").html(data_1.mass + data_1.id + "  (سند قبض)");

            // $("#result").html('');

            if (data_1.id > 0) {

                //adding class
                $("#result").addClass("alert alert-success");
                // $('#idReception').val(response.id);


                var v1 = parseFloat($('#totalT').text());
                var amountT_ = parseFloat($('#AmountT').val());

                //$('#totalT').text(v1 + v3);

                var totalT_ = parseFloat(v1 + amountT_);

                $("#totalT").fadeOut(300, function () {
                    $(this).text(totalT_).fadeIn(100);
                });


                var v2 = parseFloat($('#balance').text());

                $('#balance').text(v2 + amountT_);

                var typepaye = "نقدا";
                if ($('#TypePayT').val() == "3") {
                    typepaye = "تحويل بنكي";
                } else if ($('#TypePayT').val() == "4") {
                    typepaye = "بطاقة";
                }

                $('#compoboxEx').append("<option value='" + data_1.id + "'>" + data_1.id + " | " + priceEE + " (" + typepaye + " )" + "</option>");


                //$("#balance").fadeOut(300, function () {
                //    $(this).text(v2 - v4).fadeIn(100);
                //});

            }
            else {

                $("#result").addClass("alert alert-danger");

            }



            $('#addDoadPayModal').modal('hide');



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


                //$('#summaryLogoutModal').modal('hide');

                //$("#result").html('تم تصفية العقد نبجاح' + 56);

                //$('#idstatusReception').val(3);
                //checkSttusReception();

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

function showTalkDoad() {

    //event.preventDefault();

    $('#addDoadPayModal').modal('show');

    $('#NoteT').val('سند قبض مقابل ايجار الشقة' + $('#roominputid').val());

    $('#WhyT').val(' مقابل ايجار الشقة' + $('#roominputid').val());

    $('#nameaccountP').val($("#customerName").val());


   // $('#id_accountToT').val('الصندوق الرئيسي');
    $('#AmountT').val(0);
    $("#AmountT").prop('disabled', false);

    var now = new Date();
    $('#sourceBondT').val('fromLogin');






    // var currentDate = now.toISOString().substring(0, 10);
    // var currentTime = now.toISOString().substring(11, 16);


    var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDate = now.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    var timeOptions = { hour: '2-digit', minute: '2-digit' };
    var currentTime = now.toLocaleTimeString('it-IT', timeOptions);


    $('#TimeT').val(currentTime);
    $('#DateT').val(currentDate);


    $('#NumCheckTForm').hide();
    $('#NumCardTForm').hide();
    $('#IdBankTForm').hide();

    if (statuAccto1 == true) {
        __getBoxs();
    }
   
    //NumCardT
    //
}


function showInPopupSandQ() {

    var idInvService = $('#compoboxService').val();

    showInPopupRes("/Bond/PreViewSanadReception?id=" + idInvService, ' سند قبض');

}
