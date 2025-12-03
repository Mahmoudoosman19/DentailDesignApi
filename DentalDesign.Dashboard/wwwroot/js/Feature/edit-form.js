// edit-form.js
window.EditForm = (function () {
    function init() {
        document.querySelectorAll('form[id^="edit"][method="post"]').forEach(form => {
            if (form.dataset.bound) return;
            form.addEventListener("submit", handleSubmit);
            form.dataset.bound = "true";
        });
    }

    function handleSubmit(e) {
        e.preventDefault();
        // ... نفس منطق الحفظ السابق ...
        // استخدم window.SpaLoader.loadPage للعودة
    }

    return { init };
})();