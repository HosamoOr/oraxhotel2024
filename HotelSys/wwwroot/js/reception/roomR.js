$(document).ready(function () {


    $('#roominputid').on('change', function (event) {
        var form = $(event.target).parents('form');


        var url = '/Reception/getPriceRoom';   //$(this).attr("action");
        var comid = $(":selected", $("#roominputid")).val();
        var comname = $(":selected", $("#roominputid")).text();



        var myModel =
        {
            "id": comid,
            "name": comname
        };



        $.ajax(
            {
                type: 'POST',
                dataType: 'JSON',
                url: url,
                data: myModel,
                success:
                    function (response) {

                        var priccc = parseFloat(response.price);

                        var rate = parseFloat(response.taxGroup.rate);

                        var priceTax = parseFloat(response.taxGroup.priceTax);


                        $('#txtPrice').val(priccc);

                        $('#TotalTaxRate').val(rate);
                        $('#TotalTaxPrice').val(priceTax);

                        var isbal = response.taxGroup.isBaladiTax;


                        $('#IsBaladiTax').val(isbal);

                        $('#TotalBaladiTaxRate').val(response.taxGroup.baladiRate);

                       
                     

                        if (isbal == true) {
                            $('.divbaladiN').show();
                        } else {
                            $('.divbaladiN').hide();
                        }

                       // $('#priceB').val(response.price);
 
                        updatePrice(priccc, parseFloat($('#QtyTime').val()), parseFloat($('#QtyDiscount').val()));

                        
                       
                    },
                error:
                    function (response) {
                        alert("Error: " + response);
                    }
            });


        //$.post(url, myModel, function (response) {

        //        alert("success");


        //      //  if (intervalId)
        //            //clearInterval(intervalId);
        //    });

        //$('#table').bootstrapTable('refresh');

    });



});