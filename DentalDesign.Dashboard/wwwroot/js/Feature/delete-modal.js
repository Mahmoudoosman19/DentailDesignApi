// wwwroot/js/features/delete-modal.js

/**
 * Opens delete confirmation modal for a single item.
 * param {string} id - Item ID
 * param {Object} config - Configuration object
 * param {string} config.endpoint - Delete endpoint URL
 * param {string} [config.title='Delete Item'] - Modal title
 * param {string} [config.message='Are you sure...'] - Confirmation message
 */
function openDeleteSingle(id, config = {}) {
    const endpoint = config.endpoint || '';
    const title = config.title || 'Delete Item';
    const message = config.message || 'Are you sure you want to delete this item?';

    // ✅ اضبط الاسم ليكون 'id' (مطابق لمعلمة الـ Controller)
    const input = document.getElementById('deleteIds');
    input.name = 'id';      // ← هذا سيُرسل كـ id=...
    input.value = id;

    document.getElementById('deleteFormModal').action = endpoint;
    document.getElementById('deleteTitle').innerText = title;
    document.getElementById('deleteMessage').innerHTML = `
        <p class="mb-2">${message}</p>
        <p class="text-muted small">This action cannot be undone.</p>
    `;

    const modal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
    modal.show();
}

/**
 * Opens delete confirmation modal for selected items.
 * param {Object} config - Configuration object
 */
function openDeleteSelected(config = {}) {
    const endpoint = config.endpoint || '';
    const title = config.title || 'Delete Selected Items';
    const messagePrefix = config.messagePrefix || 'items';

    const selected = [...document.querySelectorAll('input[name="selectedIds"]:checked')].map(c => c.value);
    if (selected.length === 0) {
        alert('No items selected.');
        return;
    }

    // ✅ اضبط الاسم ليكون 'ids' (للحذف الجماعي)
    const input = document.getElementById('deleteIds');
    input.name = 'ids';     // ← هذا سيُرسل كـ ids=...
    input.value = selected.join(',');

    document.getElementById('deleteFormModal').action = endpoint;
    document.getElementById('deleteTitle').innerText = title;
    document.getElementById('deleteMessage').innerHTML = `
        <p class="mb-2">Are you sure you want to delete <strong>${selected.length}</strong> ${messagePrefix}?</p>
        <p class="text-muted small">This action cannot be undone.</p>
    `;

    const modal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
    modal.show();
}

/**
 * Handles form submission of delete modal via AJAX.
 * param {Event} event - Form submit event
 */
function submitGlobalDelete(event) {
    event.preventDefault();
    const form = document.getElementById('deleteFormModal');
    const url = form.action;
    const ids = document.getElementById('deleteIds').value;
    const modalElement = document.getElementById('deleteConfirmModal');
    const modal = bootstrap.Modal.getInstance(modalElement);

    // ✅ حدد نوع الحذف من endpoint
    const isBulk = url.includes('DeleteSelected');

    // ✅ استخدم الحقل الصحيح حسب النوع
    const body = isBulk
        ? `ids=${encodeURIComponent(ids)}`
        : `id=${encodeURIComponent(ids)}`; // ← هنا كان الخطأ: كنا نستخدم `ids` دومًا

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: body // ← استخدم المتغير `body` (ليس سلسلة ثابتة)
    })
        .then(async res => {
            if (res.ok) {
                if (modal) modal.hide();
                if (window.SpaLoader) {
                    window.SpaLoader.loadPage(location.href, false);
                } else {
                    location.reload();
                }
            } else {
                const errorMessage = await res.text().catch(() => 'Unknown error');
                throw new Error(errorMessage || 'Delete operation failed');
            }
        })
        .catch(err => {
            console.error("Delete failed:", err);
            alert(err.message || "An error occurred during deletion.");
        });
}