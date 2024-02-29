
var OutOrCancel_ = "1";

function donelogout() {
    var cuorCo1 = "cu";
    var idacc0 = Cuinstance.CIdAccount;// $('#customerID').val();

    if ($('#companyID').val().length > 0) {
        cuorCo1 = "co";
        idacc0 = $('#companyIDacc').val();
    }

    var url3 = '/LogoutCustomer/LogOut';
    var myModelLogout =
    {
        bondViewModel: null,
        IdReception: $('#idReception').val(),
        IdRoom: $('#roominputid').val(),
        IdAccount: idacc0,
        OutOrCancel: OutOrCancel_,
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


function showTalkDoadOut() {

   
    $('#addDoadPayModal').modal('show');

    $('#summaryLogoutModal').modal('hide');

   // $('#NoteT').val(' - قيمة ايجار الشقة' + $('#roominputid').val());

    $('#WhyT').val(' - قيمة ايجار الشقة' + $('#roominputid').val());

    $('#nameaccountP').val($("#customerName").val());


    // $('#id_accountToT').val('الصندوق الرئيسي');

    var ame = $('#balancerOut').val() * -1;

    $('#AmountT').val(ame);

    $("#AmountT").prop('disabled', true);


    $('#sourceBondT').val('fromLogout');
    
   
    var now = new Date();




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


  
}



function showPayDoadOut() {

    //event.preventDefault();
    //

    

   
  
    $('#nameaccountE').val($("#customerName").val());
    
    $('#sourceBondEX').val("fromLogout");
    
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
    $('#summaryLogoutModal').modal('hide');
    $('#addDoadExModal').modal('show');
    $('#summaryCancelModal').modal('show');
   
    //
}



function SummaryOutput(OutOrCancel) {
    OutOrCancel_ = OutOrCancel;
    var idRecc20 = $('#idReception').val();

    var idacc = $('#customerIDacc').val();

    if ($('#companyID').val().length > 0) {
        idacc = $('#companyIDacc').val();
    }

    var messagee = "لإكمال تصفية العقد اضغط على تصفية العقد";
    var MsgBackReception = "خروج العميل قبل موعد انتهاء العقد - " + "اجمالي المبلغ المتحصل من العميل اكبر من اجمالي مبلغ العقد  لإكمال تصفية العقد على تصفية العقد لإنشاء سند ايجار مسترد-بدل ايجار بمبلغ  " ;
    var MsgEx = "اجمالي المبلغ المتحصل من العميل اكبر من اجمالي مبلغ العقد  لإكمال تصفية العقد على تصفية العقد لإنشاء سند صرف بمبلغ  ";
    var MsgPay = "لم يتم تحصيل المبلغ الاجمالي للعقد لإكمال تصفية العقد على تصفية العقد لإنشاء سند قبض بمبلغ  ";

    if (OutOrCancel == 2) {
        $('.titleOutPage').text('الغاء العقد');

        $('#btnlogoutt').text('الغاء العقد');

        messagee = "لإكمال الغاء العقد اضغط على الغاء العقد";
        MsgBackReception = "خروج العميل بدون تسجيل دخول للشقة - " + "اجمالي المبلغ المتحصل من العميل اكبر من اجمالي مبلغ العقد  لإكمال الغاء العقد اضغط على تصفية العقد لإنشاء سند ايجار مسترد-بدل ايجار بمبلغ  ";
        MsgEx = "اجمالي المبلغ المتحصل من العميل اكبر من اجمالي مبلغ العقد  لإكمال الغاء العقد اضغط على تصفية العقد لإنشاء سند صرف بمبلغ  ";
        MsgPay = "لم يتم تحصيل المبلغ الاجمالي للعقد لإكمال الغاء العقد اضغط على تصفية العقد لإنشاء سند قبض بمبلغ  ";

    }

    var myModel =
    {
        "IdReception": idRecc20,
        "IdRoom": $('#roominputid').val(),
        "IdAccount": idacc,
        
       
        "IdSub": 1,
       
    };
    var url = '/LogoutCustomer/SummaryLogOut';

    $.post(url, myModel, function () {



    }).done(function (rec) {


       

        if (rec.id > 0) {


            var sumFromPrice_ = rec.sumFromPrice;
            var sumToPrice_ = rec.sumToPrice;
            var balance_ = rec.balance;

            var item_ = rec.item;

            alert(rec.itemDocument.id_document);

          

            //var noteBond_ = rec.noteBond;

              $('#balancerOut').val(balance_);

            $('#idReceptionOut').val(idRecc20);

          

            
            $('#itemss').text( item_);

            if (balance_ == 0) {

                $("#balancerOut").addClass("alert alert-success");

                $('#btnlogout').show();
                $('#btnBondE').hide();
                $('#btnBondP').hide();
                $('#noteOut').val(messagee);


            }
            else
                

                if (balance_ > 1) {
                    if (rec.itemDocument.id_document == '3') {
                        // سند ايجار مسترد
                        $('#noteOut').val(MsgBackReception+ "(" + balance_ + ")");

                        $('#btnnBounE').text('اضافة سند ايجار مسترد');
                        $('#titlEX').text('اضافة سند ايجار مسترد');

                        $("#TypeE option[value='2']").remove();
                        $("#TypeE option[value='3']").remove();
                        $('#TypeE').append("<option value=3 selected=''>سند بدل ايجار (ايجار مسترد)</option>");

                        $('#WhyE').val(' بدل ايجار الشقة' + $('#roominputid').val());
                       

                        $('#AmountE').val(balance_);
                        $("#AmountE").prop('disabled', true);
                        $("#WhyE").prop('disabled', true);
                    }
                    else {
                        $('#noteOut').val(MsgEx+ "(" + balance_ + ")");
                        $('#btnnBounE').text('اضافة سند صرف');
                        $('#titlEX').text('اضافة سند صرف');

                        $("#TypeE option[value='2']").remove();
                        $("#TypeE option[value='3']").remove();
                        $('#TypeE').append("<option value=2 selected=''>سند صرف</option>");

                        $('#WhyE').val(' سند صرف بدل ايجار الشقة' + $('#roominputid').val());
                        $('#AmountE').val(balance_);
                        $("#AmountE").prop('disabled', true);
                        $("#WhyE").prop('disabled', true);
                    }
                    $("#balancerOut").addClass("alert alert-success");

                    $('#btnlogout').hide();
                    $('#btnBondE').show();
                    $('#btnBondP').hide();
                   
            }
            else if (balance_ < 0) {

                    $('#noteOut').val(MsgPay + "(" + balance_ + ")");
                $("#balancerOut").addClass("alert alert-danger");

                $('#btnlogout').hide();
                $('#btnBondE').hide();
                $('#btnBondP').show();
                   

            }
            else {
                $('#btnlogout').hide();
                $('#btnBondE').hide();
                $('#btnBondP').hide();
                 $('#noteOut').val('');
                }


            if (OutOrCancel == 1) {
                if (rec.timTemp.isoverflow == true) {
                    $('#divTiminglog').show();

                }
                else {
                    $('#divTiminglog').hide();
                }
            }


            $("#result").addClass("alert alert-success");

            // $('#idReception').val(response.id);

            $('#summaryLogoutModal').modal('show');

           
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
$(document).ready(function () {
    $('#btnlogout').hide();
    $('#btnBondE').hide();
    $('#btnBondP').hide();
    $('#divTiminglog').hide();
    
});
