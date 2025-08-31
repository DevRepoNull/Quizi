const SwalHelper = {
    showSuccess: function (message) {
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: message,
            showConfirmButton: false,
            timer: 1500,
            customClass: {
                popup: 'swal2-rtl'
            }
        });
    },

    showError: function (message) {
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: message,
            showConfirmButton: false,
            timer: 1500,
            customClass: {
                popup: 'swal2-rtl'
            }
        });
    },

    showConfirm: function (title, text, confirmButtonText, cancelButtonText, callback) {
        Swal.fire({
            title: title,
            text: text,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: confirmButtonText,
            cancelButtonText: cancelButtonText,
            reverseButtons: true,
            customClass: {
                popup: 'swal2-rtl'
            }
        }).then((result) => {
            if (result.isConfirmed) {
                callback();
            }
        });
    }
};