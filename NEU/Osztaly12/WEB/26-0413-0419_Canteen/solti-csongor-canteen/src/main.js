import { getMenus, createMenu, deleteMenu, menus } from './js/canteen.js';

const dayOrder = {
    'Hétfő': 1,
    'Kedd': 2,
    'Szerda': 3,
    'Csütörtök': 4,
    'Péntek': 5
};

const typeOrder = {
    'A': 1,
    'B': 2,
    'Diétás': 3,
    'Vegán': 4
};

const tableBody = document.getElementById('table-body');
const menuRowTemplate = document.getElementById('menu-row-template');
const menuDialog = document.getElementById('menu-dialog');
const confirmDialog = document.getElementById('confirm-dialog');
const menuForm = document.getElementById('menu-form');
const openDialogBtn = document.getElementById('add-button') || document.getElementById('open-dialog-btn');
const closeDialogBtn = document.getElementById('close-dialog-btn');
const dialogTitle = document.getElementById('dialog-title');

function cellValue(value) {
    if (value === undefined || value === null || value === '') {
        return '-';
    }
    return value;
}

function tableError(message) {
    tableBody.innerHTML = '';
    const tr = document.createElement('tr');
    const td = document.createElement('td');
    td.colSpan = 8;
    td.textContent = message;
    td.className = 'px-4 py-8 text-center text-red-600 font-bold bg-red-50 rounded-lg my-4';
    tr.appendChild(td);
    tableBody.appendChild(tr);
}

function renderMenus(menuList) {
    tableBody.innerHTML = '';
    
    if (!menuList || menuList.length === 0) {
        tableError('Nincs még felvett menü.');
        return;
    }

    const sortedMenus = [...menuList].sort((a, b) => {
        const orderA = dayOrder[a.day] || 99;
        const orderB = dayOrder[b.day] || 99;
        if (orderA !== orderB) {
            return orderA - orderB;
        }
        const typeA = typeOrder[a.type] || 99;
        const typeB = typeOrder[b.type] || 99;
        return typeA - typeB;
    });

    sortedMenus.forEach(menu => {
        const clone = menuRowTemplate.content.cloneNode(true);
        clone.querySelector('.menu-day').textContent = menu.day;
        clone.querySelector('.menu-type').textContent = menu.type;
        clone.querySelector('.menu-soup').textContent = cellValue(menu.soup);
        clone.querySelector('.menu-main').textContent = cellValue(menu.main);
        clone.querySelector('.menu-drink').textContent = cellValue(menu.drink);
        clone.querySelector('.menu-fruit').textContent = cellValue(menu.fruit);
        clone.querySelector('.menu-dessert').textContent = cellValue(menu.dessert);
        
        const deleteBtn = clone.querySelector('.delete-btn');
        deleteBtn.onclick = () => showConfirmDialog(menu);
        
        tableBody.appendChild(clone);
    });
}

function showCreateDialog() {
    menuDialog.setAttribute('data-id', '');
    if (dialogTitle) dialogTitle.textContent = 'Új kerékpár';
    menuForm.reset();
    menuDialog.showModal();
}

function showConfirmDialog(menu) {
    confirmDialog.setAttribute('data-id', menu.id);
    const daySpan = confirmDialog.querySelector('.day');
    const typeSpan = confirmDialog.querySelector('.type');
    if (daySpan) daySpan.textContent = menu.day;
    if (typeSpan) typeSpan.textContent = menu.type;
    confirmDialog.showModal();
}

if (openDialogBtn) openDialogBtn.onclick = showCreateDialog;
if (closeDialogBtn) closeDialogBtn.onclick = () => menuDialog.close();

menuForm.onsubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(menuForm);
    const menuData = Object.fromEntries(formData.entries());
    
    if (!menuData.id) delete menuData.id;

    try {
        await createMenu(menuData);
        renderMenus(menus);
        menuForm.reset();
        menuDialog.close();
    } catch (error) {
        alert('Nem sikerült elmenteni a menüt.');
    }
};

const confirmDeleteBtn = confirmDialog.querySelector('.delete');
if (confirmDeleteBtn) {
    confirmDeleteBtn.onclick = async (e) => {
        e.preventDefault();
        const id = confirmDialog.getAttribute('data-id');
        try {
            await deleteMenu(id);
            renderMenus(menus);
            confirmDialog.close();
        } catch (error) {
            alert('Sikertelen törlés.');
        }
    };
}

const cancelDeleteBtn = confirmDialog.querySelector('.cancel');
if (cancelDeleteBtn) {
    cancelDeleteBtn.onclick = () => {
        confirmDialog.close();
    };
}

async function init() {
    try {
        await getMenus();
        renderMenus(menus);
    } catch (error) {
        tableError('Hiba a betöltés során.');
    }
}

init();
