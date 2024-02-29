
var statuAccto = true;
var DataItem = [];


$(document).ready(function () {

   

    $('#amountDisS').hide();
    $('#rateDisS').hide();
    $('.loading').hide();
    $('#idacc_to_div').hide();

    //
    $("#compoboxService").change(function () {
        alert("Print me");
    });
    
    $("#QtyDiscountRateS").change(function () {
        var discrs = parseFloat($(this).val());
        var totals2 = parseFloat($('#TotalService').val());;

        if (discrs > 100 || discrs < 0) {
            alert("عفوا .. لابد ان تكون نسبة الخصم اقل او يساوي 100%");
            $('#QtyDiscountRateS').val(100);
            $("#QtyDiscountS").val(totals2);
        }

        updatePriceS(totals2, $("#QtyDiscountS").val());
       // updatePrice(parseFloat($('#txtPrice').val()), parseFloat($('#QtyTime').val()), parseFloat($('#QtyDiscount').val()));


    });

    $("#QtyDiscountS").change(function () {
        var disc2 = parseFloat($(this).val());

        var total2 = parseFloat($('#TotalService').val());;

        if (disc2 > total2) {
            alert("عفوا .. لابد ان تكون قيمة الخصم اقل او يساوي اجمالي الفاتورة");

            $("#QtyDiscountS").fadeOut(300, function () {
                $(this).val(total2).fadeIn(300);
            });

            disc2 = total2;
           //$('#QtyDiscountS').val(total2);
        }

       // updatePrice(parseFloat($('#txtPrice').val()), parseFloat($('#QtyTime').val()), parseFloat($('#QtyDiscount').val()));
        updatePriceS(total2, disc2);

    });
    

    $('#TypeDiscountS').on('change', function (event) {

        var targetSs = $('#TypeDiscountS option:selected').val();
        if (targetSs == "1") {
            $('#amountDisS').show();
            $('#rateDisS').hide();


        }
        else if (targetSs == "2") {
            $('#amountDisS').hide();
            $('#rateDisS').show();

        }
        else {
            $('#amountDisS').hide();
            $('#rateDisS').hide();

        }


    });


    $('#TypePayBillService').on('change', function (event) {

        var targetSs = $('#TypePayBillService option:selected').val();
        if (targetSs == "1") {
            $('#idacc_to_div').show();

            if (statuAccto == true) {
                _getBoxs();
            }

           
            
        }
        else if (targetSs == "2") {
            $('#idacc_to_div').hide();
          

        }
        else {
            $('#idacc_to_div').hide();
           
        }


    });








    var creditAmount = 0
   // $('#tableProduct').DataTable();

    $("#tableProduct").on('change', function () {


       // var parent0 = $(this).closest('tr');
        //var qty_s = $(this).find("td:eq(4) input[type='number']").val();

        //var price_s = $(this).find("td:eq(3)").text();

        //alert(price_s);


        

       // toastr["warning"]("Producto ya existe");

        //
        //var sum_ = parseFloat(qty_s * price_s);

        //$(this).closest("tr").find("td:eq(5)").text(sum_);


  
    
        //var choose = parseFloat($('.choose', parent).val());

       // $('.total', parent).text(choose * price);



        var checkedCount = $("#tableProduct input:checked").length;
        var creditAmount = 0;

        var totalVAt_ = 0;
        var totalBelad_ = 0;
        DataItem = [];

        var isTaxSerinc = $('.sincludetax').prop('checked');
       
      
        for (var i = 0; i < checkedCount; i++) {

            var qtyy = $("#tableProduct input:checked")[i].parentNode.parentNode.children[4].children[0].value;

            var price_ = $("#tableProduct input:checked")[i].parentNode.parentNode.children[3].innerHTML;
            var idPro = $("#tableProduct input:checked")[i].parentNode.parentNode.children[1].innerHTML;

            var price_tax_ = $("#tableProduct input:checked")[i].parentNode.parentNode.children[5].innerHTML;
            var price_baladi_ = $("#tableProduct input:checked")[i].parentNode.parentNode.children[6].innerHTML;


            


            var sumSub = parseFloat(qtyy * price_);

            if (isTaxSerinc === false) {
                sumSub = parseFloat(sumSub) + parseFloat(price_tax_) + parseFloat(price_baladi_);
                alert(sumSub);
            }

            $("#tableProduct input:checked")[i].parentNode.parentNode.children[7].innerHTML = sumSub

            if (qtyy != "") {
                creditAmount += parseFloat(sumSub);
            } else {
                creditAmount = 0;
            }
            totalVAt_ = totalVAt_ + parseFloat(price_tax_);
            totalBelad_ = totalBelad_ + parseFloat(price_baladi_);
           
            $('#TotalTaxSer').val(totalVAt_);
            $('#TotalBaladiSer').val(totalBelad_);


            DataItem.push({
               
                    'Id': 0,
                'Qty': qtyy,
                'PriceOne': price_,
                'Total': sumSub,
                'IdProduct': idPro,
                'IdBill': 0,
                'TaxPrice': price_tax_,
                'BaladiTaxPrice': price_baladi_

              
               

            });
        }
        $("#TotalService").fadeOut(500, function () {
            $(this).val(creditAmount).fadeIn(500);
        });

        
        var qyd = $("#QtyDiscountS").val();
        if (qyd > creditAmount) {
            alert("يجب ان يكون مبلغ الخصم اقل او يساوي اجمالي الفاتورة");

            $("#QtyDiscountS").val(creditAmount);
            qyd = creditAmount;
        }

        $("#FinalTotalS").fadeOut(500, function () {
            $(this).val(parseFloat(creditAmount - qyd)).fadeIn(500);
        });

        

    });


    //$('#tableProduct tbody input[name="name1"]').on('change', function () {
    //    var checkedValue = $(this).prop('checked');
    //    // uncheck other checkboxes (checkboxes on the same row)
    //    $('input[name="name1"]:checked').each(function () {
    //        $(this).prop('checked', false);
    //    });
    //    $(this).prop("checked", checkedValue);
    //});
  

});


function _getBoxs() {
    var url = '/_Accounts/GetBoxs/';
   // $('.loading').show();


     $.post(url, function () {
        
       // $('.loading').hide();

    })
        .done(function (data) {

            var response = jQuery.parseJSON(data);


            for (var i = 0; i < response.length; i++) {
                // alert(response[i].Name);

               
                $('#id_accountToServeice').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


            }
            statuAccto = false;


        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

}

function cleanFeild() {
    $("#QtyDiscountS").val(0);
    $("#FinalTotalS").val(0);
    $("#TotalService").val(0);

}

function updatePriceS(total00,discount00) {
    
    var total_finish = total00 - discount00;

   
    var targetT = $('#TypeDiscountS option:selected').val();
    if (targetT == "2") {
        var disc00 = parseFloat($('#QtyDiscountRateS').val());
        var qpric0 = total00 * disc00 / 100;

        $("#QtyDiscountS").fadeOut(300, function () {
            $(this).val(qpric0).fadeIn(300);
        });



        total_finish = total00 - qpric0;
    }
    else {
        // $('#totalDiscount').text(discountf);


        $("#QtyDiscountS").fadeOut(300, function () {
            $(this).val(discount00).fadeIn(300);
        });

    }


    $("#FinalTotalS").fadeOut(300, function () {
        $(this).val(parseFloat(total_finish)).fadeIn(300);
    });

    $("#TotalService").fadeOut(300, function () {
        $(this).val(parseFloat(total00)).fadeIn(300);
    });


}
function saveBondService() {

    var cuorCo1 = "cu";
    var idacc0 = Cuinstance.CIdAccount;//$('#customerID').val();

    var idaccFrom = idacc0;
    var idaccTo = 211002; //ايرادات ايجار

   


    if ($('#companyID').val().length > 0) {
        cuorCo1 = "co";
       // idacc0 = $('#companyIDacc').val();
        idaccFrom = $('#companyIDacc').val();
    }

    var methedPay = $('#TypePayBillService').val();
    if (methedPay == "1") {

        idaccFrom = $('#id_accountToServeice').val();//الصناديق
    }
   

    var tot = $('#TotalService').val();

    var isTaxSerinc_ = $('.sincludetax').prop('checked');

    var totaTaxRat_1 = parseFloat($('#TotalTaxSer').val()) / parseFloat($('#FinalTotalS').val()) * 100;

    var totalBeladiRat_1 = parseFloat($('#TotalBaladiSer').val()) / parseFloat($('#FinalTotalS').val()) * 100;


    var myModel =
    {
        "Id": 0,
        "Type": 7,
        "TypePay": $('#TypePayBillService').val(),
        //"NumReference": $('#NumReferenceT').val(),
        "Date": $('#DateS').val(),
        "Total": tot,
        "IsForRoom": true,

        "DeserveAmount": $('#FinalTotalS').val(),
        "TypeDiscount": $('#TypeDiscountS').val(),
        "QtyDiscount": $('#QtyDiscountS').val(),
        //"PayAmount": $('#QtyDiscount').val(),
       // "RestAmount": $('#QtyDiscount').val(),
        //"NumCheck": $('#QtyDiscount').val(),
        "Note": $('#noteS').val(),
        
        //"IdReception": $('#QtyDiscount').val(),
       // "IdBank": $('#QtyDiscount').val(),
        "IdCurrancy": 0,
      
        "id_accountForm": idaccFrom ,
        "id_accountTo": idaccTo,

        "IdReception": $('#idReception').val(),
        "IdAccount": idacc0,
        "customerOrCompany": cuorCo1,

        "IncludeTax": isTaxSerinc_,
        "TotalTaxPrice": $('#TotalTaxSer').val(),
        "TotalTaxRate": totaTaxRat_1,
        "TotalBaladiTaxPrice": $('#TotalBaladiSer').val(),
        "TotalBaladiTaxRate": totalBeladiRat_1,


        "Items": DataItem,


    };
    
    var url = '/Bill/Createjson';



    $.post(url, myModel, function () {

    

    }).done(function (data_) {

        alert("id  " + data_.id);

        $("#result").html(data_.mass + data_.id + "  (سند خدمات )");

        // $("#result").html('');

        if (data_.id > 0) {

            //adding class
            $("#result").addClass("alert alert-success");
            // $('#idReception').val(response.id);

            if (methedPay == "2") {



                var totalBill_ = parseFloat($('#totalB').text());
                var totalFinish_ = parseFloat($('#finishtotal').text());
                var balance1_ = parseFloat($('#balance').text());

                var sumServices_ = parseFloat($('#FinalTotalS').val());

                var totalsumServices_ = parseFloat($('#sumServices').text());


                if ($('#sumServices').text() === ".") {

                    $('#sumServices').text(sumServices_);
                }
                else {
                    alert($('#sumServices').text());


                    $('#sumServices').text(totalsumServices_ + sumServices_);
                }



               // $('#totalB').text(totalBill_ + sumServices_);
               // $('#finishtotal').text(totalFinish_ + sumServices_);

                $('#balance').text(balance1_ + (sumServices_ * -1));


               

            }

            var typeep = "نقدا";
            if ($('#TypePayBillService').val() == "2") {
                typeep = "كمبيالة";
            }

            $('#compoboxService').append("<option value='" + data_.id + "'>" + data_.id + " | " + tot + " ( " + typeep +" )"+"</option>");


        }
        else {

            $("#result").addClass("alert alert-danger");

        }

        $('#addServicesModal').modal('hide');




    })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });



        
}

function showServiceDoad1() {

   
 //event.preventDefault();

    if ($('#customerName').val() == "") {
        alert('الرجاء اختيار عميل اولا');
        return;
    }

    $('#addServicesModal').modal('show');

    var url = '/ReceptionService/GetInitBill/';

  

    $('.loading').show();
  

    var jqxhr = $.post(url, function () {
        //alert("success");
        $('.loading').hide();

    })
        .done(function (data) {
          
            var response = jQuery.parseJSON(data);

            var ddata = response['dataGroup'];
            var isServiceTax = response['ServicesIncludeTax'];
            var txtServiceTax = response['txtIncludeTax'];

          //  alert(isServiceTax);


            $('.sincludetax').prop('checked', isServiceTax);
         

            $('#txtIncudeSer').text(txtServiceTax);

          
            for (var i = 0; i < ddata.length; i++) {
               // alert(response[i].Name);

                var r1 = '<button  data-id="' + ddata[i].Id + '"  data-toggle="' + ddata[i].Name + '" onclick="selectgroupService(event)"  class="btn btn-secondary mb-2 mr-1" data-method="getData" data-option="" data-target="#putData">' + ddata[i].Name + '</button>';

            
                $("#groupContact").append(r1);

            }
        

        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

    

    //// Set another completion function for the request above
    //jqxhr.always(function () {
    //    alert("second finished");
    //});


    $('#noteS').val('سند خدمات  على الشقة' + $('#roominputid').val());

    $('#AmountS').val(0);

    


    var now = new Date();



    var dateOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    var currentDate = now.toLocaleDateString('ja-JP', dateOptions).replace(/\//gi, '-');
    //var timeOptions = { hour: '2-digit', minute: '2-digit' };
    //var currentTime = now.toLocaleTimeString('it-IT', timeOptions);


    $('#DateS').val(currentDate);


   // $('#NumCheckEForm').hide();
    //$('#NumCardEForm').hide();
    //$('#IdBankEForm').hide();


   
}

var stdt = false;

function selectgroupService(e) {

    var row = e.target.getAttribute('data-id');

    var row2 = e.target.getAttribute('data-toggle');


    if (stdt == false) {
        var dt = $('#tableProduct')
            .DataTable({

                "sAjaxSource": '/Products/GetDataFarz1/' + row,
                "bServerSide": true,
                "bProcessing": true,
                "bSearchable": true,
                "order": [[1, 'asc']],
                "language": {
                    //"emptyTable": "No record found.",
                    //"processing":
                    //    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">انتضر يتم تحميل البيانات...</span> '

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
                        "data": "id"
                        ,
                        "render": function (data, type, row, meta) {
                            //return '<a class='btn btn-default btn-sm' onclick=PopupForm('@Url.Action("AddOrEdit","Employee")/" + data + "')><i class='fa fa-pencil'></i> Edit</a>'

                            return '<input type="checkbox" name="name1" />'

                              //  < button data - id="' + row.id + '"  data - toggle="' + row.name + '" onclick = "selectProCLick(event)" > select</button > ';

                            //btn = '<a class="btn btn-success"  onclick="deleteThis( \'' + row.name + '\' )"/><i class="fa fa-edit"></i> Edit</a>';
                            //return btn;

                        }


                    }
                    ,
                    {
                        "data": "id",
                        "autoWidth": false,
                        "width": "5%",
                       
                        "searchable": true
                    },
                    {
                        "data": "name",
                        "autoWidth": false,
                        "searchable": true,
                        "width": "30%",
                    }
                    ,
                    {
                        "data": "price",
                        "autoWidth": true,
                        "searchable": true
                    },

                    {
                        "autoWidth": false,
                        "width": "30%",
                        'render': function (date) {


                            return '<input type="number" style="font-size: 12px;"  class=" form-control full-width text-lg-center" value="1" >';
                        }
                    },
                    {
                        "data": "tax_price",
                        "autoWidth": true,
                        "searchable": true
                    },
                    {
                        "data": "baladi_price",
                        "autoWidth": true,
                        "searchable": true
                    },

                    {
                        
                        "autoWidth": false,
                        "width": "2%",
                        "defaultContent": '0', //ms-2: modificar las distancia de separacion del icono

                        //'render': function (date) {


                        //    return '0';
                        //}
                    }



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
        stdt = true;
    }
    else {
        $('#tableProduct').DataTable().ajax.url('/Products/GetDataFarz1/' + row).load();
    }

    cleanFeild();
}
   
function selectProCLick(ed) {

    var row3 = ed.target.getAttribute('data-id');

    var row4 = ed.target.getAttribute('data-toggle');
    //alert(row3 + row4);
    }

function showInPopupInvServ() {

    var idInvService = $('#compoboxService').val();

    showInPopupRes("/ReceptionService/PreViewBillService?id="+idInvService, ' فاتورة خدمات');

}

function printInv() {

   

}



