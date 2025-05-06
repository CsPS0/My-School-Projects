"use strict";

const belvaros = [
    345000, 289000, 310000, 275000, 320000, 295000, 340000, 360000, 280000, 300000,
    315000, 290000, 305000, 325000, 310000, 335000, 345000, 295000, 310000, 330000,
    285000, 320000, 300000, 310000, 335000, 290000, 305000, 315000, 340000, 300000
];

const andrassy = [
    340000, 295000, 310000, 325000, 300000, 315000, 330000, 285000, 320000, 305000,
    310000, 335000, 290000, 345000, 300000, 310000, 295000, 320000, 340000, 280000,
    300000, 315000, 290000, 305000, 325000, 310000, 335000, 345000, 295000, 310000
];

const vaci = [
    320000, 300000, 310000, 335000, 290000, 305000, 325000, 310000, 335000, 295000,
    340000, 280000, 300000, 315000, 280000, 305000, 325000, 310000, 335000, 345000,
    295000, 310000, 340000, 300000, 310000, 335000, 350000, 305000, 325000, 315000
];

const margit = [
    340000, 290000, 300000, 334000, 280000, 307000, 320000, 300000, 340000, 298000,
    330000, 290000, 310000, 310000, 290000, 320000, 320000, 280000, 325000, 355000,
    285000, 300000, 335000, 280000, 300000, 330000, 330000, 285000, 330000, 280000
];

const stores = [
    'Belváros',
    'Andrássy',
    'Váci',
    'Margit'
];

// 12. feladat: sum függvény
function sum(array) {
    return array.reduce((total, current) => total + current, 0);
}

// 13. feladat: displayCard függvény
function displayCard(day, store) {
    // Kártya lekérése
    const card = document.getElementById('card');
    
    // Hidden osztály eltávolítása
    card.classList.remove('hidden');
    
    // Középre igazítás inline stílussal
    card.style.margin = '0 auto';
    
    // Kép beállítása
    const cardImg = card.querySelector('img');
    cardImg.src = `src/images/${store}.jpg`;
    
    // Card-body elem kiürítése
    const cardBody = document.getElementById('card-body');
    cardBody.innerHTML = '';
    
    // H3 létrehozása
    const heading = document.createElement('h3');
    heading.textContent = store;
    heading.style.marginBottom = '0.5rem';
    
    // Adatok lekérése és megjelenítése
    let storeData;
    switch (store) {
        case 'Belváros':
            storeData = belvaros;
            break;
        case 'Andrássy':
            storeData = andrassy;
            break;
        case 'Váci':
            storeData = vaci;
            break;
        case 'Margit':
            storeData = margit;
            break;
    }
    
    // Napi forgalom bekezdés
    const dayParagraph = document.createElement('p');
    dayParagraph.textContent = `Napi forgalom: ${storeData[day-1].toLocaleString()} Ft`;
    
    // Havi forgalom bekezdés
    const monthParagraph = document.createElement('p');
    monthParagraph.textContent = `Havi forgalom: ${sum(storeData).toLocaleString()} Ft`;
    
    // Elemek hozzáadása a card-body-hoz
    cardBody.appendChild(heading);
    cardBody.appendChild(dayParagraph);
    cardBody.appendChild(monthParagraph);
}

// 14. feladat: Űrlap eseménykezelő
document.querySelector('form').addEventListener('submit', function(event) {
    // Alapértelmezett művelet letiltása
    event.preventDefault();
    
    // Adatok lekérése
    const day = document.getElementById('day').value;
    const store = document.getElementById('store').value;
    
    // Validálás
    if (!stores.includes(store)) {
        return;
    }
    
    // Kártya megjelenítése
    displayCard(day, store);
});