// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {

    //alert(url);

    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}


showInPopupSmail = (url, title) => {

    //alert(url);

    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

showInPopupBig = (url, title) => {
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
    });
}

jQueryAjaxPostBig = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    //$('#view-all').html(res.html)

                    $('#tableServicesGruop').DataTable().ajax.reload(null, false);
                    executeExample('mixin');

                    $('#print-modal .modal-body').html('');
                    $('#print-modal .modal-title').html('');
                    $('#print-modal').modal('hide');
                }
                else
                    $('#print-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


jQueryAjaxPostWithotList = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    //$('#view-all').html(res.html)

                    $('#tableServicesGruop').DataTable().ajax.reload(null, false);
                    executeExample('mixin');

                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html);
                    executeExample('mixin');
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');

                    $('#tableServicesGruop').DataTable().ajax.reload(null, false);
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {


    Swal.fire({
        title: 'Are you sure?',
        text: "هل انت متأكد من حذف السجل"  + " !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'نعم, حذف السجل!',
    }).then(function (result) {
        if (result.isConfirmed) {


            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {

                        if (res.isValid) {
                            Swal.fire(
                                'تم الحذف!',
                                'تم حذف السجل بنجاح.',
                                'success'
                            );

                        }
                        else {
                            Swal.fire(
                                'لم يتم الحذف!',
                                'حدث خطا اثناء الحذف.',
                                'error'
                            );
                        }

                       
                        $('#view-all').html(res.html);
                        
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }



        }
    });


    //if (confirm('Are you sure to delete this record ?')) {
    //    try {
    //        $.ajax({
    //            type: 'POST',
    //            url: form.action,
    //            data: new FormData(form),
    //            contentType: false,
    //            processData: false,
    //            success: function (res) {
    //                $('#view-all').html(res.html);
    //            },
    //            error: function (err) {
    //                console.log(err)
    //            }
    //        })
    //    } catch (ex) {
    //        console.log(ex)
    //    }
    //}



    //prevent default form submit event
    return false;
}


showDelPop = (url, title) => {
   // alert(url);


  

    Swal.fire({
        title: 'Are you sure?',
        text: "هل انت متأكد من حذف" + title +" !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'نعم, حذف السجل!',
    }).then(function (result) {
        if (result.isConfirmed) {


            $.ajax({
                type: 'GET',
                url: url,
                success: function (res) {

                    $('#tableServicesGruop').DataTable().ajax.reload(null, false);

                    Swal.fire(
                        'تم الحذف!',
                        'تم حذف السجل بنجاح.',
                        'success'
                    );

                    //$('#print-modal .modal-body').html(res);
                    //$('#print-modal .modal-title').html(title);
                    //$('#print-modal').modal('show');
                    //// to make popup draggable
                    //$('.modal-dialog').draggable({
                    //    handle: ".modal-header"
                    //});
                }
            });



        }
    })

}