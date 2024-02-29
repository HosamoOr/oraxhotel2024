
$(document).ready(function () {

    var targetT = $('#TypeDiscount option:selected').val();
    if (targetT == "-1") {
        $('#amountDis').hide();
        $('#rateDis').hide();
    }
    else if (targetT == "1") {
        $('#amountDis').show();
        $('#rateDis').hide();
    }
    else {
        $('#amountDis').hide();
        $('#rateDis').show();
    }

    $("#QtyDiscountRate").change(function () {
        var disc0 = parseFloat($(this).val());
        var total2 = parseFloat($('#totalB').text());;

        if (disc0 > 100 || disc0 < 0) {
            alert("عفوا .. لابد ان تكون نسبة الخصم اقل او يساوي 100%");
            $('#QtyDiscountRate').val(100);
            $("#QtyDiscount").val(total2);

            //$("#QtyDiscountRate").fadeOut(500, function () {
            //    $(this).val(100).fadeIn(500);
            //});

            //$("#QtyDiscount").fadeOut(500, function () {
            //    $(this).val(100).fadeIn(total2);
            //});

        }
       

        //var qpric = total2 * disc0 / 100;
        //$("#QtyDiscount").val(qpric);

        updatePrice(parseFloat($('#txtPrice').val()), parseFloat($('#QtyTime').val()), parseFloat($('#QtyDiscount').val()));

        
    });

    $("#QtyDiscount").change(function () {
        var disc = parseFloat( $(this).val());

        var total0 = parseFloat($('#totalB').text());;

        if (disc > total0) {
            alert("عفوا .. لابد ان تكون قيمة الخصم اقل او يساوي اجمالي الفاتورة");
            $('#QtyDiscount').val(total0);

            //$("#QtyDiscount").fadeOut(300, function () {
            //    $(this).val(total0).fadeIn(500);
            //});
        }
       
        updatePrice(parseFloat($('#txtPrice').val()), parseFloat($('#QtyTime').val()), parseFloat($('#QtyDiscount').val()));

        
    });


    $('#TypeDiscount').on('change', function (event) {
      
        var targetS = $('#TypeDiscount option:selected').val();
        if (targetS == "1") {
            $('#amountDis').show();
            $('#rateDis').hide();

           
        }
        else if (targetS == "2") {
            $('#amountDis').hide();
            $('#rateDis').show();

        }
        else {
            $('#amountDis').hide();
            $('#rateDis').hide();

        }
       

    });
    


});



function saveBill() {

    var cuorCo1 = "cu";
    var idacc = Cuinstance.CIdAccount;

      //  $('#customerIDacc').val();



    if ($('#companyID').val().length > 0) {
        cuorCo1 = "co";
        idacc = $('#companyIDacc').val();
    }

   
    var total_ = parseFloat($('#totalB').text());
    var finishtotal_ = parseFloat($('#finishtotal').text());


    var typeDis = $('#TypeDiscount').val();
    var qtyDis = $('#QtyDiscount').val();

    //if (typeDis == "1") {
    //    qtyDis = $('#QtyDiscount').val();
    //}
    //else if (typeDis == "2") {

    //    qtyDis = $('#QtyDiscountRate').val();
    //}


    var myModel =
    {
        "Id": $('#idBillID').val(),
        "Total": total_,
        "DeserveAmount": finishtotal_,
        "IdAccount": idacc,
        "CustomerOrCompany": cuorCo1,
        "IdCurrancy": 1,
        "TypeDiscount": typeDis,
        "QtyDiscount": qtyDis,
        "CustomerOrCompany": cuorCo1,
        "Type": 5

    };
    var url = '/Bill/updateBillReceptionjson';
    $.post(url, myModel, function (response) {

        //alert("id  " + response.id);

        $("#result").html(response.mass + response.id);

        if (response.id == true) {


            $("#result").addClass("alert alert-success");

            $('#btnenterRoom').prop('disabled', false);

            $('#privew_REbtn').prop('disabled', false);
            

            $('#btnRebortBill').prop('disabled', false);


        }
        else {

            $("#result").addClass("alert alert-danger");

        }


    });
}



