window.ShowToastr = (type, msg) => {
    if (type == 'success') {
        toastr.success(msg, 'Operation Successful', { timeOut: 5000 })
    }
    if (type == 'error') {
        toastr.error(msg, 'Operation Failure', { timeOut: 5000 })
    }
}
window.ShowSwal = (type, msg1, msg2) => {
    if (type == 'success') {
        Swal.fire(
            msg1,
            msg2,
            'success'
        )
    }
    if (type == 'error') {
        Swal.fire(
            msg1,
            msg2,
            'error'
        )
    }
}

//Swal.fire({
//    title: "Do you want to save the changes?",
//    showDenyButton: true,
//    showCancelButton: true,
//    confirmButtonText: "Save",
//    denyButtonText: `Don't save`
//}).then((result) => {
//    /* Read more about isConfirmed, isDenied below */
//    if (result.isConfirmed) {
//        Swal.fire("Saved!", "", "success");
//    } else if (result.isDenied) {
//        Swal.fire("Changes are not saved", "", "info");
//    }
//});

window.ShowPic = (msg1, msg2) => {
    Swal.fire({
        title: msg1,
        text: msg2,
        imageUrl: "https://unsplash.it/400/200",
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: "Custom image"
    })
}


function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}