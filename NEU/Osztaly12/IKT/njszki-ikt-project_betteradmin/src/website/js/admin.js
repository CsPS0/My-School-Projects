// Admin Panel Logic
let currentViewData = [];
let currentViewType = '';

async function loadAdminInterface() {
    console.log('loadAdminInterface started');
    try {
        const adminPanel = document.getElementById('admin-panel');
        if (!adminPanel) {
            console.error('Admin panel container not found!');
            return;
        }

        // Get user name safely
        let userName = 'Admin';
        try {
            if (typeof getCurrentUser === 'function') {
                const u = getCurrentUser();
                if (u) userName = u.fullName || u.username || 'Admin';
            }
        } catch (e) { console.warn('User name fetch failed', e); }

        console.log('Rendering admin shell...');
        adminPanel.innerHTML = `
        <div class="admin-container">
            <!-- Sidebar Navigation -->
            <aside class="sidebar">
                <div class="sidebar-header">
                    <h2>Admin Panel</h2>
                    <div class="user-info">
                        <p class="user-name">${userName}</p>
                    </div>
                </div>

                <nav class="nav-menu">
                    <button class="nav-button active" onclick="loadAdminView('bookings')">
                        <span class="nav-icon">📅</span>
                        <span class="nav-text">Foglalások</span>
                    </button>
                    <button class="nav-button" onclick="loadAdminView('menu')">
                        <span class="nav-icon">🍽️</span>
                        <span class="nav-text">Menü</span>
                    </button>
                    <button class="nav-button" onclick="loadAdminView('tables')">
                        <span class="nav-icon">🪑</span>
                        <span class="nav-text">Asztalok</span>
                    </button>
                    <button class="nav-button" onclick="loadAdminView('users')">
                        <span class="nav-icon">👥</span>
                        <span class="nav-text">Felhasználók</span>
                    </button>
                    <button class="nav-button" onclick="loadAdminView('stats')">
                        <span class="nav-icon">📊</span>
                        <span class="nav-text">Statisztikák</span>
                    </button>
                    <button class="nav-button" onclick="loadAdminView('settings')">
                        <span class="nav-icon">⚙️</span>
                        <span class="nav-text">Beállítások</span>
                    </button>
                </nav>

                <button class="logout-button" onclick="handleLogout()">
                    <span class="nav-icon">🚪</span>
                    <span class="nav-text">Kijelentkezés</span>
                </button>
            </aside>

            <!-- Main Content Area -->
            <main class="main-content">
                <header class="top-bar">
                    <h1 id="admin-page-title">Foglalások kezelése</h1>
                </header>

                <div class="content-area" id="admin-content">
                    <p style="padding:20px;">Adatok betöltése folyamatban...</p>
                </div>
            </main>
        </div>

        <!-- Admin Modal (Generic) -->
        <div id="admin-modal" class="modal">
            <div class="modal-content">
                <span class="modal-close" onclick="closeAdminModal()">&times;</span>
                <h2 id="admin-modal-title"></h2>
                <div id="admin-modal-body"></div>
            </div>
        </div>
        `;
        
        // Load default view
        loadAdminView('bookings');

    } catch (error) {
        console.error('Error loading admin interface:', error);
        const p = document.getElementById('admin-panel');
        if(p) p.innerHTML = `<div style="color:red;padding:20px;">CRITICAL ERROR: ${error.message}</div>`;
    }
}

async function loadAdminView(view) {
    console.log('Loading view:', view);
    const content = document.getElementById('admin-content');
    const title = document.getElementById('admin-page-title');
    currentViewType = view;
    
    if (!content || !title) return;

    // Update active button
    document.querySelectorAll('.nav-button').forEach(btn => btn.classList.remove('active'));
    const buttons = document.querySelectorAll('.nav-button');
    buttons.forEach(btn => {
        const text = btn.innerText.toLowerCase();
        // Simplified mapping
        const viewMap = {
            'bookings': 'foglalások',
            'menu': 'menü',
            'tables': 'asztalok',
            'users': 'felhasználók',
            'stats': 'statisztikák',
            'settings': 'beállítások'
        };
        if (text.includes(viewMap[view])) {
            btn.classList.add('active');
        }
    });
    
    try {
        content.innerHTML = '<p style="text-align:center; padding: 20px;">Adatok betöltése...</p>';

        switch(view) {
            case 'bookings':
                title.textContent = 'Foglalások kezelése';
                currentViewData = await DataService.getBookings();
                content.innerHTML = renderAdminBookings(currentViewData);
                break;
            case 'menu':
                title.textContent = 'Menü kezelése';
                currentViewData = await DataService.getMenu();
                content.innerHTML = renderAdminMenu(currentViewData);
                break;
            case 'tables':
                title.textContent = 'Asztalok kezelése';
                currentViewData = await DataService.getTables();
                content.innerHTML = renderAdminTables(currentViewData);
                break;
            case 'users':
                title.textContent = 'Felhasználók kezelése';
                currentViewData = await DataService.getUsers();
                content.innerHTML = renderAdminUsers(currentViewData);
                break;
            case 'stats':
                title.textContent = 'Statisztikák';
                const sBookings = await DataService.getBookings();
                const sMenu = await DataService.getMenu();
                const sTables = await DataService.getTables();
                content.innerHTML = renderAdminStats(sBookings, sMenu, sTables);
                break;
            case 'settings':
                title.textContent = 'Beállítások';
                const settings = await DataService.getSettings();
                content.innerHTML = renderAdminSettings(settings);
                break;
        }
    } catch (error) {
        console.error(`Error loading view ${view}:`, error);
        content.innerHTML = `
            <div style="color: red; text-align: center; padding: 20px;">
                <p>Hiba történt az adatok betöltésekor.</p>
                <p>${error.message}</p>
                <button class="btn btn-primary" onclick="loadAdminView('${view}')">Újrapróbálás</button>
            </div>`;
    }
}

// --- Render Functions with Toolbar & Search ---

function renderToolbar(placeholder, buttonText, buttonAction) {
    return `
    <div class="toolbar" style="display: flex; justify-content: space-between; margin-bottom: 20px;">
        <div class="search-wrapper" style="flex: 1; max-width: 300px;">
            <input type="text" 
                   placeholder="${placeholder}" 
                   onkeyup="filterAdminTable(this.value)"
                   style="width: 100%; padding: 10px; border: 1px solid var(--border); border-radius: 6px;">
        </div>
        ${buttonText ? `<button class="btn btn-primary" onclick="${buttonAction}">${buttonText}</button>` : ''}
    </div>`;
}

function renderTableStructure(headers, rowsHtml) {
    return `
    <div class="card">
        <div class="table-container">
            <table class="data-table" id="admin-data-table">
                <thead>
                    <tr>${headers.map(h => `<th>${h}</th>`).join('')}</tr>
                </thead>
                <tbody>
                    ${rowsHtml}
                </tbody>
            </table>
        </div>
    </div>`;
}

function renderAdminBookings(bookings) {
    const toolbar = renderToolbar('Keresés név vagy email alapján...', '➕ Új foglalás', 'openNewBookingModal()');
    
    if (!bookings || bookings.length === 0) {
        return toolbar + '<p style="text-align:center; padding: 20px;">Nincs megjeleníthető foglalás.</p>';
    }

    const rows = bookings.map(b => `
        <tr>
            <td>${b.name}</td>
            <td>${b.date}</td>
            <td>${b.time}</td>
            <td>${b.guests}</td>
            <td>${b.email || '-'}</td>
            <td>${b.phone}</td>
            <td>${b.status || '-'}</td>
            <td>
                <button class="btn btn-small btn-danger" onclick="deleteBooking('${b.id}')">Törlés</button>
            </td>
        </tr>
    `).join('');

    return toolbar + renderTableStructure(['Név', 'Dátum', 'Idő', 'Vendégek', 'Email', 'Telefon', 'Státusz', 'Műveletek'], rows);
}

function renderAdminMenu(menu) {
    const toolbar = renderToolbar('Keresés étel neve alapján...', '➕ Új elem', "openNewMenuItemModal()");

    const rows = menu.map(item => `
        <tr>
            <td>${item.name}</td>
            <td>${item.category}</td>
            <td>${item.price} Ft</td>
            <td>${item.available ? 'Igen' : 'Nem'}</td>
            <td>${item.description || '-'}</td>
            <td>
                <button class="btn btn-small btn-danger" onclick="deleteMenuItem('${item.id}')">Törlés</button>
            </td>
        </tr>
    `).join('');

    return toolbar + renderTableStructure(['Név', 'Kategória', 'Ár', 'Elérhető', 'Leírás', 'Műveletek'], rows);
}

function renderAdminTables(tables) {
    const toolbar = renderToolbar('Keresés...', '➕ Új asztal', "openNewTableModal()");

    const rows = tables.map(t => `
        <tr>
            <td style="font-weight: bold; font-size: 1.1em;">${t.number}.</td>
            <td>${t.capacity} fő</td>
            <td>${t.location}</td>
            <td style="color: ${t.available ? 'green' : 'red'}; font-weight: 500;">
                ${t.available ? 'Szabad' : 'Foglalt'}
            </td>
            <td>
                <button class="btn btn-small btn-danger" onclick="deleteTable('${t.id}')">Törlés</button>
            </td>
        </tr>
    `).join('');

    return toolbar + renderTableStructure(['Asztalszám', 'Kapacitás', 'Helyszín', 'Státusz', 'Műveletek'], rows);
}

function renderAdminUsers(users) {
    const toolbar = renderToolbar('Keresés név vagy felhasználónév alapján...', '', '');

    const rows = users.map(u => `
        <tr>
            <td>${u.fullName || '-'}</td>
            <td>${u.username}</td>
            <td>${u.email}</td>
            <td>${u.role}</td>
            <td>
                ${u.username === 'admin' 
                    ? '<span style="color: #999; font-style: italic;">Rendszergazda</span>' 
                    : `<button class="btn btn-small btn-danger" onclick="deleteUser('${u.id}')">Törlés</button>`
                }
            </td>
        </tr>
    `).join('');

    return toolbar + renderTableStructure(['Név', 'Felhasználónév', 'Email', 'Szerepkör', 'Műveletek'], rows);
}

// --- Features ---

function filterAdminTable(searchTerm) {
    searchTerm = searchTerm.toLowerCase();
    const table = document.getElementById('admin-data-table');
    if (!table) return;
    
    const rows = table.getElementsByTagName('tr');
    
    // Skip header row (index 0)
    for (let i = 1; i < rows.length; i++) {
        let text = rows[i].innerText.toLowerCase();
        rows[i].style.display = text.includes(searchTerm) ? '' : 'none';
    }
}

// Modal functions
function closeAdminModal() {
    document.getElementById('admin-modal').classList.remove('active');
}

// New Booking Modal
function openNewBookingModal() {
    const modal = document.getElementById('admin-modal');
    const title = document.getElementById('admin-modal-title');
    const body = document.getElementById('admin-modal-body');
    
    title.innerText = 'Új Foglalás Létrehozása';
    body.innerHTML = `
        <form onsubmit="saveNewBooking(event)" class="booking-form" style="box-shadow: none; padding: 0;">
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Név</label>
                <input type="text" name="name" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Email</label>
                <input type="email" name="email" style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Telefonszám</label>
                <input type="text" name="phone" required style="width: 100%;">
            </div>
            <div class="form-row" style="display: flex; gap: 12px; margin-bottom: 12px;">
                <div class="form-group" style="flex: 1;">
                    <label>Dátum</label>
                    <input type="date" name="date" required style="width: 100%;">
                </div>
                <div class="form-group" style="flex: 1;">
                    <label>Idő</label>
                    <select name="time" required style="width: 100%; padding: 10px;">
                        <option value="12:00">12:00</option>
                        <option value="13:00">13:00</option>
                        <option value="14:00">14:00</option>
                        <option value="18:00">18:00</option>
                        <option value="19:00">19:00</option>
                        <option value="20:00">20:00</option>
                    </select>
                </div>
            </div>
            <div class="form-group" style="margin-bottom: 24px;">
                <label>Vendégek száma</label>
                <input type="number" name="guests" value="2" min="1" max="20" required style="width: 100%;">
            </div>
            <button type="submit" class="btn btn-primary" style="width: 100%;">Létrehozás</button>
        </form>
    `;
    
    modal.classList.add('active');
}

window.saveNewBooking = async function(e) {
    e.preventDefault();
    const formData = new FormData(e.target);
    const booking = {
        name: formData.get('name'),
        email: formData.get('email'),
        phone: formData.get('phone'),
        date: formData.get('date'),
        time: formData.get('time'),
        guests: parseInt(formData.get('guests')),
        status: 'Megerősítve',
        userId: 0
    };

    try {
        await DataService.saveBooking(booking);
        alert('Foglalás sikeresen létrehozva!');
        closeAdminModal();
        loadAdminView('bookings');
    } catch (error) {
        alert('Hiba: ' + error.message);
    }
}

// New Menu Item Modal
function openNewMenuItemModal() {
    const modal = document.getElementById('admin-modal');
    const title = document.getElementById('admin-modal-title');
    const body = document.getElementById('admin-modal-body');
    
    title.innerText = 'Új Menüelem Hozzáadása';
    body.innerHTML = `
        <form onsubmit="saveNewMenuItem(event)" class="booking-form" style="box-shadow: none; padding: 0;">
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Étel neve</label>
                <input type="text" name="name" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Kategória</label>
                <select name="category" required style="width: 100%; padding: 10px;">
                    <option value="Levesek">Levesek</option>
                    <option value="Főételek">Főételek</option>
                    <option value="Desszertek">Desszertek</option>
                    <option value="Italok">Italok</option>
                </select>
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Ár (Ft)</label>
                <input type="number" name="price" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Leírás</label>
                <textarea name="description" style="width: 100%; height: 80px; padding: 10px; border: 1px solid var(--border); border-radius: 6px;"></textarea>
            </div>
            <div class="form-group" style="margin-bottom: 24px; flex-direction: row; align-items: center; gap: 8px;">
                <input type="checkbox" name="available" id="item-available" checked>
                <label for="item-available">Elérhető</label>
            </div>
            <button type="submit" class="btn btn-primary" style="width: 100%;">Hozzáadás</button>
        </form>
    `;
    
    modal.classList.add('active');
}

window.saveNewMenuItem = async function(e) {
    e.preventDefault();
    const formData = new FormData(e.target);
    const menuItem = {
        name: formData.get('name'),
        category: formData.get('category'),
        price: parseInt(formData.get('price')),
        description: formData.get('description'),
        available: e.target.elements.available.checked
    };

    try {
        await DataService.saveMenuItem(menuItem);
        alert('Menüelem sikeresen hozzáadva!');
        closeAdminModal();
        loadAdminView('menu');
    } catch (error) {
        alert('Hiba: ' + error.message);
    }
}

// New Table Modal
function openNewTableModal() {
    const modal = document.getElementById('admin-modal');
    const title = document.getElementById('admin-modal-title');
    const body = document.getElementById('admin-modal-body');
    
    title.innerText = 'Új Asztal Hozzáadása';
    body.innerHTML = `
        <form onsubmit="saveNewTable(event)" class="booking-form" style="box-shadow: none; padding: 0;">
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Asztalszám</label>
                <input type="number" name="number" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Kapacitás (fő)</label>
                <input type="number" name="capacity" value="4" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 12px;">
                <label>Helyszín</label>
                <input type="text" name="location" placeholder="pl. Terasz, Ablak mellett" required style="width: 100%;">
            </div>
            <div class="form-group" style="margin-bottom: 24px; flex-direction: row; align-items: center; gap: 8px;">
                <input type="checkbox" name="available" id="table-available" checked>
                <label for="table-available">Elérhető</label>
            </div>
            <button type="submit" class="btn btn-primary" style="width: 100%;">Hozzáadás</button>
        </form>
    `;
    
    modal.classList.add('active');
}

window.saveNewTable = async function(e) {
    e.preventDefault();
    const formData = new FormData(e.target);
    const table = {
        number: parseInt(formData.get('number')),
        capacity: parseInt(formData.get('capacity')),
        location: formData.get('location'),
        available: e.target.elements.available.checked
    };

    try {
        await DataService.saveTable(table);
        alert('Asztal sikeresen hozzáadva!');
        closeAdminModal();
        loadAdminView('tables');
    } catch (error) {
        alert('Hiba: ' + error.message);
    }
}

// --- Delete Functions ---

window.deleteMenuItem = async function(id) {
    if(confirm('Biztosan törölni szeretné ezt a menüelemet?')) {
        try {
            await DataService.deleteMenuItem(id);
            loadAdminView('menu');
        } catch (error) {
            alert('Törlés sikertelen: ' + error.message);
        }
    }
};

window.deleteTable = async function(id) {
    if(confirm('Biztosan törölni szeretné ezt az asztalt?')) {
        try {
            // DataService needs deleteTable
            const token = sessionStorage.getItem('authToken');
            const response = await fetch(`${window.location.origin}/tables/${id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok) throw new Error('Failed to delete table');
            loadAdminView('tables');
        } catch (error) {
            alert('Törlés sikertelen: ' + error.message);
        }
    }
};

// --- Stats & Settings ---

function renderAdminStats(bookings, menu, tables) {
    const today = new Date().toISOString().split('T')[0];
    const todaysBookings = bookings.filter(b => b.date === today);
    const todayGuests = todaysBookings.reduce((sum, b) => sum + (parseInt(b.guests) || 0), 0);
    const availableTables = tables.filter(t => t.available).length;

    return `
    <div class="stats-grid" style="display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 24px;">
        <div class="card" style="text-align: center; padding: 32px;">
            <div style="font-size: 48px; margin-bottom: 16px;">📅</div>
            <h3>Mai Foglalások</h3>
            <p style="font-size: 32px; font-weight: bold; color: var(--primary);">${todaysBookings.length}</p>
        </div>
        <div class="card" style="text-align: center; padding: 32px;">
            <div style="font-size: 48px; margin-bottom: 16px;">👥</div>
            <h3>Várható Vendégek</h3>
            <p style="font-size: 32px; font-weight: bold; color: var(--primary);">${todayGuests}</p>
        </div>
        <div class="card" style="text-align: center; padding: 32px;">
            <div style="font-size: 48px; margin-bottom: 16px;">🍽️</div>
            <h3>Menü Elemek</h3>
            <p style="font-size: 32px; font-weight: bold; color: var(--primary);">${menu.length}</p>
        </div>
        <div class="card" style="text-align: center; padding: 32px;">
            <div style="font-size: 48px; margin-bottom: 16px;">🪑</div>
            <h3>Szabad Asztalok</h3>
            <p style="font-size: 32px; font-weight: bold; color: var(--primary);">${availableTables} / ${tables.length}</p>
        </div>
    </div>`;
}

function renderAdminSettings(settings) {
    return `
    <div class="card" style="max-width: 800px; margin: 0 auto;">
        <form onsubmit="saveAdminSettings(event)">
            <h3 style="margin-bottom: 24px;">Étterem Adatai</h3>
            
            <div class="form-row">
                <div class="form-group">
                    <label>Étterem neve</label>
                    <input type="text" name="restaurantName" value="${settings.restaurantName || ''}" required>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label>Cím</label>
                    <input type="text" name="address" value="${settings.address || ''}">
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label>Telefonszám</label>
                    <input type="text" name="phone" value="${settings.phone || ''}">
                </div>
                <div class="form-group">
                    <label>Email</label>
                    <input type="email" name="email" value="${settings.email || ''}">
                </div>
            </div>

            <h3 style="margin: 24px 0;">Nyitvatartás</h3>

            <div class="form-row">
                <div class="form-group">
                    <label>Hétfő - Péntek</label>
                    <input type="text" name="weekdays" value="${settings.openingHours?.weekdays || ''}">
                </div>
                <div class="form-group">
                    <label>Szombat - Vasárnap</label>
                    <input type="text" name="weekends" value="${settings.openingHours?.weekends || ''}">
                </div>
            </div>

            <div style="display: flex; justify-content: flex-end; margin-top: 24px;">
                <button type="submit" class="btn btn-primary">Mentés</button>
            </div>
        </form>
    </div>`;
}

window.saveAdminSettings = async function(e) {
    e.preventDefault();
    const form = e.target;
    const formData = new FormData(form);
    
    const settings = {
        restaurantName: formData.get('restaurantName'),
        address: formData.get('address'),
        phone: formData.get('phone'),
        email: formData.get('email'),
        openingHours: {
            weekdays: formData.get('weekdays'),
            weekends: formData.get('weekends')
        }
    };

    try {
        await DataService.saveSettings(settings);
        alert('Beállítások sikeresen mentve!');
        if (typeof loadSettings === 'function') {
            loadSettings(); 
        }
    } catch (error) {
        alert('Hiba a mentés során: ' + error.message);
    }
};

window.deleteBooking = async function(id) {
    if(confirm('Biztosan törölni szeretné?')) {
        try {
            await DataService.deleteBooking(id);
            loadAdminView('bookings');
        } catch (error) {
            alert('Törlés sikertelen: ' + error.message);
        }
    }
};

window.deleteUser = async function(id) {
    if(confirm('Biztosan törölni szeretné ezt a felhasználót?')) {
        try {
            await DataService.deleteUser(id);
            loadAdminView('users');
        } catch (error) {
            alert('Törlés sikertelen: ' + error.message);
        }
    }
};