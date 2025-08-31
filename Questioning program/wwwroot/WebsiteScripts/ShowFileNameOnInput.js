$(document).ready(function () {
    $('.custom-file-input').on('change', function (e) {
        const fileName = e.target.files[0]?.name || 'انتخاب فایل';
        $(this).next('.custom-file-label').html(fileName);
    });
});