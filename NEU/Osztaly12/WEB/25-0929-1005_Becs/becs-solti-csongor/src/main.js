"use strict"

const buses = 166200000;
const ubahn = 352400000;
const trams = 273400000;

const walking = '32%: sétáló';
const publicTransit = '32%: tömegközelekedő';
const cycling = '10%: biciklista';
const driving = '26%: autós';

const utasokSzamaElem = document.getElementById('passengers');
const osszesUtas = buses + ubahn + trams;
const osszesUtasMillio = osszesUtas / 1000000;
utasokSzamaElem.textContent = `${osszesUtasMillio.toFixed(1)} millió`;

const diagramElem = document.getElementById('diagram');
diagramElem.innerHTML = '';

function createBar(szelesseg, szoveg) {
    const wrapper = document.createElement('div');
    wrapper.classList.add('bar-wrapper');

    const bar = document.createElement('div');
    bar.classList.add('bar');
    bar.style.width = `${szelesseg}%`;
    bar.textContent = `${szelesseg}%`;

    const textNode = document.createTextNode(szoveg);

    wrapper.appendChild(bar);
    wrapper.appendChild(textNode);

    return wrapper;
}

const adatok = [walking, publicTransit, cycling, driving];

adatok.forEach(adat => {
    const reszek = adat.split(': ');
    const szazalek = parseInt(reszek[0]);
    const szoveg = reszek[1];

    const ujSav = createBar(szazalek, szoveg);
    diagramElem.appendChild(ujSav);
});