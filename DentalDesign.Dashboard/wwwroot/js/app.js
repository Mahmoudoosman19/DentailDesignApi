// app.js
document.addEventListener("DOMContentLoaded", () => {
    // Init core
    window.SpaLoader.init();

    // Initial binding
    if (window.EventBinder) window.EventBinder.bindAll();
});