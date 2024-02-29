

$(document).ready(function () {

    $('#city').on('change', function (event) {
        var form = $(event.target).parents('form');

        var comid_ = $(":selected", $("#city")).val();
        var url = '/H_Area/GetByCity/' + comid_;   //$(this).attr("action");



       

       
        $.ajax(
            {
                type: 'GET',
                url: url,
                success:
                    function (data) {

                        var response = jQuery.parseJSON(data);

                        $('#Id_Area option').each(function () {
                            if ($(this).val() != '-1') {
                                $(this).remove();
                            }
                        });

                    
                        for (var i = 0; i < response.length; i++) {
                            // alert(response[i].Name);

                            


                            $('#Id_Area').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


                        }
                        var idarea_ = $("#idareaHid").val();

                        //alert(idarea_);

                        if ($("#idcityHid").val() == comid_) {
                            if (idarea_ != -1) {
                                $("#Id_Area").val(idarea_).change();

                            }

                        }

                        

                    },
                error:
                    function (response) {
                        alert("Error: " + response);
                    }
            });

    });
    

    $('#country').on('change', function (event) {
        var form = $(event.target).parents('form');

        var comid_2 = $(":selected", $("#country")).val();

        var url = '/H_City/GetBycountry/' + comid_2;   //$(this).attr("action");


       

        $.ajax(
            {
                type: 'GET',
                url: url,
                success:
                    function (data) {

                        var response = jQuery.parseJSON(data);

                        $('#city option').each(function () {
                            if ($(this).val() != '-1') {
                                $(this).remove();
                            }
                        });

                        $('#Id_Area option').each(function () {
                            if ($(this).val() != '-1') {
                                $(this).remove();
                            }
                        });

                        for (var i = 0; i < response.length; i++) {
                            // alert(response[i].Name);




                            $('#city').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


                        }
                        var icity_ = $("#idcityHid").val();

                       // alert(icity_);

                        if ($("#idcountryHid").val() == comid_2) {
                            if (icity_ != -1) {
                                $("#city").val(icity_).change();

                            }

                        }



                    },
                error:
                    function (response) {
                       // alert("Error: " + response);
                    }
            });

    });



});




function _getcountry() {
    var url = '/H_Country/GetCountrys/';

    //alert("fff");
    // $('.loading').show();


    $('#country option').each(function () {
        if ($(this).val() != '-1') {
            $(this).remove();
        }
    });


    $.post(url, function () {

        // $('.loading').hide();

    })
        .done(function (data) {

            var response = jQuery.parseJSON(data);


            for (var i = 0; i < response.length; i++) {
                // alert(response[i].Name);


                $('#country').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>"); //response[i].Id 


            }
            var idcitt = $("#idcityHid").val();

            var idconunt = $("#idcountryHid").val();

            

           

            if (idcitt != -1) {
                $("#city").val(idcitt).change();
            }

            if (idcitt != -1) {
                $("#country").val(idconunt).change();
            }




            //


        })
        .fail(function () {
          //  alert("error");
        })
        .always(function () {
            //alert("finished");
        });

}







function _getCity() {
    var url = '/H_City/GetCitys/';

   // alert("fff");
    // $('.loading').show();


    $('#city option').each(function () {
        if ($(this).val() != '-1') {
            $(this).remove();
        }
    });


    $.post(url, function () {

        // $('.loading').hide();

    })
        .done(function (data) {

            var response = jQuery.parseJSON(data);


            for (var i = 0; i < response.length; i++) {
                // alert(response[i].Name);


                $('#city').append("<option value='" + response[i].Id + "'>" + response[i].Name + "</option>");


            }
            var idcitt = $("#idcityHid").val();

            if (idcitt != -1) {
                $("#city").val(idcitt).change();
            }

           

          

           //
           

        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            //alert("finished");
        });

}
