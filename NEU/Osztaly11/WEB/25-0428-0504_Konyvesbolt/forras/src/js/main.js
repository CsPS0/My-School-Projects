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

function sum(array) {
    return array.reduce((total, current) => total + current, 0);
}

function displayCard(day, store) {
    const card = document.getElementById('card');
    
    card.classList.remove('hidden');
    
    card.style.margin = '0 auto';
    
    const cardImg = card.querySelector('img');
    cardImg.src = `src/images/${store}.jpg`;
    
    const cardBody = document.getElementById('card-body');
    cardBody.innerHTML = '';
    
    const heading = document.createElement('h3');
    heading.textContent = store;
    heading.style.marginBottom = '0.5rem';
    
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
    
    const dayParagraph = document.createElement('p');
    dayParagraph.textContent = `Napi forgalom: ${storeData[day-1].toLocaleString()} Ft`;
    
    const monthParagraph = document.createElement('p');
    monthParagraph.textContent = `Havi forgalom: ${sum(storeData).toLocaleString()} Ft`;
    
    cardBody.appendChild(heading);
    cardBody.appendChild(dayParagraph);
    cardBody.appendChild(monthParagraph);
}

document.querySelector('form').addEventListener('submit', function(event) {
    event.preventDefault();
    
    const day = document.getElementById('day').value;
    const store = document.getElementById('store').value;
    
    if (!stores.includes(store)) {
        return;
    }
    
    displayCard(day, store);
});