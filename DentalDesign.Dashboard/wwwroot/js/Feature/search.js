// search.js
window.LiveSearch = (function () {
    function init() {
        const input = document.getElementById("searchInput");
        if (!input || input.dataset.bound) return;

        let timeout;
        input.addEventListener("input", () => {
            clearTimeout(timeout);
            const query = input.value.trim();
            const controller = location.pathname.split('/')[1] || 'Home';
            const url = `/${controller}/Index?Name=${encodeURIComponent(query)}`;
            timeout = setTimeout(() => window.SpaLoader.loadPage(url), 300);
        });
        input.dataset.bound = "true";
    }

    return { init };
})();