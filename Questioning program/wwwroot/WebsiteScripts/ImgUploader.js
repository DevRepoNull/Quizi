function readURL(input, targetSelector) {
    if (!input.files || input.files.length === 0) return;

    const reader = new FileReader();
    reader.onload = function (e) {
        const target = document.querySelector(targetSelector);
        if (target) {
            target.src = e.target.result;
        }
    };
    reader.readAsDataURL(input.files[0]);
}

document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".custom-file-input").forEach(input => {
        input.addEventListener("change", function () {
            const targetSelector = this.getAttribute("data-target");
            if (targetSelector) {
                readURL(this, targetSelector);
            }
        });
    });
});
