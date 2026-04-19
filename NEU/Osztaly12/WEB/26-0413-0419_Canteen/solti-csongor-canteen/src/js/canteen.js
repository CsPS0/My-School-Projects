import { BASE_URL } from './config.js';

export let menus = [];

export async function getMenus() {
    const response = await fetch(`${BASE_URL}/menus`);
    if (!response.ok) throw new Error('Hiba a letöltés során.');
    const result = await response.json();
    const newMenus = result.data || result;
    menus.splice(0, menus.length, ...newMenus);
    return menus;
}

export async function createMenu(menuData) {
    const response = await fetch(`${BASE_URL}/menus`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(menuData)
    });
    if (!response.ok) {
        throw new Error('Sikertelen mentés.');
    }
    const result = await response.json();
    const newMenu = result.data || result;
    menus.push(newMenu);
    return newMenu;
}

export async function deleteMenu(id) {
    const response = await fetch(`${BASE_URL}/menus/${id}`, {
        method: 'DELETE'
    });
    if (!response.ok) {
        throw new Error('Sikertelen törlés.');
    }
    const index = menus.findIndex(m => m.id == id);
    if (index !== -1) {
        menus.splice(index, 1);
    }
    return true;
}
