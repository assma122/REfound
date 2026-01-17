// ==========================================
// ReFound - Dashboard JavaScript
// ==========================================

document.addEventListener('DOMContentLoaded', function () {
    console.log('Dashboard loaded ✓');

    // تفعيل Filter Tabs
    initFilterTabs();

    // إخفاء رسالة النجاح بعد 5 ثواني
    hideAlertAfterDelay();
});

// === Filter Tabs ===
function initFilterTabs() {
    const filterTabs = document.querySelectorAll('.filter-tab');

    filterTabs.forEach(tab => {
        tab.addEventListener('click', function () {
            // إزالة active من جميع الـ tabs
            filterTabs.forEach(t => t.classList.remove('active'));

            // إضافة active للـ tab المحدد
            this.classList.add('active');

            // الحصول على نوع الفلتر
            const filter = this.dataset.filter;

            // تطبيق الفلتر (سيتم ربطه بالـ Backend لاحقاً)
            filterItems(filter);
        });
    });
}

// === Filter Items ===
function filterItems(filter) {
    console.log('Filtering by:', filter);

    // TODO: هنا ستقومين بإرسال طلب للـ Backend لجلب العناصر المفلترة
    // مثال:
    // fetch(`/Item/GetFiltered?status=${filter}`)
    //     .then(response => response.json())
    //     .then(data => displayItems(data));

    // مؤقتاً: عرض رسالة
    const itemsGrid = document.getElementById('itemsGrid');
    itemsGrid.innerHTML = `
        <div class="empty-state">
            <span class="material-symbols-outlined empty-icon">search_off</span>
            <h3>No ${filter === 'all' ? '' : filter} items found</h3>
            <p>Items will appear here when available.</p>
        </div>
    `;
}

// === Display Items (ستستخدمينها لاحقاً) ===
function displayItems(items) {
    const itemsGrid = document.getElementById('itemsGrid');

    if (items.length === 0) {
        itemsGrid.innerHTML = `
            <div class="empty-state">
                <span class="material-symbols-outlined empty-icon">search_off</span>
                <h3>No items found</h3>
                <p>Be the first to report a lost or found item!</p>
                <button class="btn-add" onclick="window.location.href='/Item/Create'">
                    <span class="material-symbols-outlined">add_circle</span>
                    <span>Add New Item</span>
                </button>
            </div>
        `;
        return;
    }

    let html = '';
    items.forEach(item => {
        html += `
            <div class="item-card">
                <div class="item-image">
                    <img src="${item.imageUrl || '/images/placeholder.jpg'}" alt="${item.name}">
                    <span class="status-badge ${item.status.toLowerCase()}">${item.status}</span>
                </div>
                <div class="item-content">
                    <h3 class="item-title">${item.name}</h3>
                    <p class="item-description">${item.description}</p>
                    <div class="item-meta">
                        <div class="meta-item">
                            <span class="material-symbols-outlined">location_on</span>
                            <span>${item.location}</span>
                        </div>
                        <div class="meta-item">
                            <span class="material-symbols-outlined">calendar_today</span>
                            <span>${formatDate(item.date)}</span>
                        </div>
                    </div>
                </div>
            </div>
        `;
    });

    itemsGrid.innerHTML = html;
}

// === Format Date ===
function formatDate(dateString) {
    const date = new Date(dateString);
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    return date.toLocaleDateString('en-US', options);
}

// === Hide Alert After Delay ===
function hideAlertAfterDelay() {
    const alerts = document.querySelectorAll('.alert');

    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            setTimeout(() => {
                alert.remove();
            }, 300);
        }, 5000);
    });
}

// === Search Functionality ===
const searchInput = document.querySelector('.search-input');
if (searchInput) {
    searchInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            const query = this.value.trim();
            if (query) {
                window.location.href = `/Item/Search?query=${encodeURIComponent(query)}`;
            }
        }
    });
}