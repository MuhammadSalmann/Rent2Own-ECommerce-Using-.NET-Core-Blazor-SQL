window.ShowToastr = (type, msg) => {
    if (type == 'success') {
        toastr.success(msg, 'Operation Successful', { timeOut: 5000 })
    }
    if (type == 'error') {
        toastr.error(msg, 'Operation Failure', { timeOut: 5000 })
    }
}

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

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}