// wwwroot/js/core/event-binder.js

/**
 * Binds event handlers to dynamically loaded content.
 * Call this after every AJAX content update.
 */
window.EventBinder = (function () {
    'use strict';

    // === Public API ===
    function bindAll() {
        bindAjaxLinks();
        bindSearch();
        bindPagination();
        bindEditForms();
        bindSelectAll();
    }

    // === Private Helpers ===
    function bindAjaxLinks() {
        document.querySelectorAll(".ajax-link").forEach(link => {
            if (!link.dataset.bound) {
                link.addEventListener("click", function (e) {
                    e.preventDefault();
                    if (window.SpaLoader) {
                        window.SpaLoader.loadPage(this.href);
                    }
                });
                link.dataset.bound = "true";
            }
        });
    }

    function bindSearch() {
        // سيتم تفعيله تلقائيًا عبر search.js عند وجود #searchInput
        // لا حاجة لربط يدوي هنا
    }

    function bindPagination() {
        document.querySelectorAll(".pagination .page-link").forEach(link => {
            if (!link.dataset.bound) {
                link.addEventListener("click", function (e) {
                    e.preventDefault();
                    if (window.SpaLoader) {
                        window.SpaLoader.loadPage(this.href);
                    }
                });
                link.dataset.bound = "true";
            }
        });
    }

    function bindEditForms() {
        document.querySelectorAll('form[id^="edit"][method="post"]').forEach(form => {
            if (form.dataset.bound) return;

            form.addEventListener("submit", function (e) {
                e.preventDefault();
                const formData = new FormData(this);
                const url = this.action;
                const token = this.querySelector('input[name="__RequestVerificationToken"]');
                if (token) formData.set("__RequestVerificationToken", token.value);

                fetch(url, {
                    method: "POST",
                    headers: { "X-Requested-With": "XMLHttpRequest" },
                    body: formData
                })
                    .then(async res => {
                        if (res.ok) {
                            const contentType = res.headers.get("content-type");
                            if (contentType && contentType.includes("application/json")) {
                                const result = await res.json();
                                if (result?.isSuccess) {
                                    const controller = window.location.pathname.split('/')[1] || 'Home';
                                    if (window.SpaLoader) {
                                        window.SpaLoader.loadPage(`/${controller}/Index`);
                                    }
                                }
                            } else {
                                // Validation errors: replace content
                                const html = await res.text();
                                if (window.SpaLoader && window.SpaLoader.getMainContent) {
                                    window.SpaLoader.getMainContent().innerHTML = html;
                                    bindAll(); // Re-bind
                                }
                            }
                        } else {
                            const html = await res.text();
                            if (window.SpaLoader && window.SpaLoader.getMainContent) {
                                window.SpaLoader.getMainContent().innerHTML = html;
                                bindAll();
                            }
                        }
                    })
                    .catch(err => {
                        console.error("Edit failed:", err);
                        alert("Failed to save changes.");
                    });
            });
            form.dataset.bound = "true";
        });
    }

    function bindSelectAll() {
        const selectAll = document.getElementById("selectAll");
        if (selectAll && !selectAll.dataset.bound) {
            selectAll.addEventListener("change", function () {
                document.querySelectorAll('input[name="selectedIds"]').forEach(cb => {
                    cb.checked = this.checked;
                });
            });
            selectAll.dataset.bound = "true";
        }
    }

    // === Expose Public Methods ===
    return {
        bindAll: bindAll
    };
})();