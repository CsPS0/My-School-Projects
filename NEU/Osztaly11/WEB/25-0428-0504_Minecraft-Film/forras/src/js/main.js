"use strict"

const quotes = [
    {
        hu: 'Én, Steve vagyok!',
        en: 'I am Steve!',
        image: 'quotes/i_am_steve.png',
    },
    {
        hu: 'Csirke Zsoké!',
        en: 'Chicken Jockey!',
        image: 'quotes/chicken_jockey.png',
    },
    {
        hu: 'Tűz szerszám!',
        en: 'Flint and steel!',
        image: 'quotes/flint_and_steel.png',
    },
    {
        hu: 'Az Alvilág!',
        en: 'The Nether!',
        image: 'quotes/the_nether.png',
    },
    {
        hu: 'Ez egy barkácsasztal!',
        en: 'This is a crafting table!',
        image: 'quotes/crafting_table.png',
    },
    {
        hu: 'Forrá láva és csirke!',
        en: 'Hot lava and chicken!',
        image: 'quotes/hot_lava_and_chicken.png',
    },
    {
        hu: 'Gyémánt páncél, teljes szett!',
        en: 'Diamond armor, full set!',
        image: 'quotes/diamond_armor.png',
    },
    {
        hu: 'Kiengedés!',
        en: 'Release!',
        image: 'quotes/release.png',
    },
    {
        hu: 'Forrón jövök be!',
        en: 'Coming in hot!',
        image: 'quotes/coming_in_hot.png',
    },
];

const ticketsSold = [56, 34, 23, 45, 67, 89, 12, 34, 56, 78,];

const selectableLanguages = ['en', 'hu'];

// 11. feladat: randomQuote függvény
function randomQuote() {
    const randomIndex = Math.floor(Math.random() * quotes.length);
    return quotes[randomIndex];
}

// 12. feladat: displaySumOfTickets függvény
function displaySumOfTickets(sum) {
    document.getElementById('sum').textContent = `Az eladott jegyek száma: ${sum} db`;
}

// 13. feladat: Jegyek összegének kiszámítása és megjelenítése
const sumOfTickets = ticketsSold.reduce((acc, curr) => acc + curr, 0);
displaySumOfTickets(sumOfTickets);

// 14. feladat: Űrlap eseményfigyelő
document.querySelector('form').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const name = document.getElementById('name').value;
    const language = document.getElementById('language').value;
    
    if (!selectableLanguages.includes(language)) {
        alert('A kiválasztott nyelv érvénytelen!');
        return;
    }
    
    const selectedQuote = randomQuote();
    
    const quoteCard = document.getElementById('quote-card');
    quoteCard.classList.remove('hidden');
    
    const quoteCardImg = quoteCard.querySelector('img');
    quoteCardImg.src = `src/images/${selectedQuote.image}`;
    quoteCardImg.alt = selectedQuote[language];
    
    const quoteCardBody = document.getElementById('quote-card-body');
    quoteCardBody.innerHTML = '';
    
    const nameElement = document.createElement('h3');
    nameElement.textContent = name;
    nameElement.style.textAlign = 'center';
    
    const quoteElement = document.createElement('p');
    quoteElement.textContent = selectedQuote[language];
    quoteElement.style.textAlign = 'center';
    quoteElement.style.fontStyle = 'italic';
    
    quoteCardBody.appendChild(nameElement);
    quoteCardBody.appendChild(quoteElement);
});