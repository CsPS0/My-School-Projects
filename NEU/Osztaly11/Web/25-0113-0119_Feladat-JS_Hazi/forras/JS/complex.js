"use strict";

// 2.
for (let index = 1; index <= 100; index++) {
    console.log(`${index}: ${index % 2 === 0 ? "páros" : "páratlan"}`);
}

// 3.
let ersteZahl = parseFloat(prompt("Kérlek, add meg az első számot!"));
let zweiteZahl = parseFloat(prompt("Kérlek, add meg a második számot!"));
if (!isNaN(ersteZahl) && !isNaN(zweiteZahl)) {
    alert(`Az összegük: ${ersteZahl + zweiteZahl}`);
} else {
    alert("Érvénytelen számot adtál meg.");
}

// 4.
while (isNaN(ersteZahl)) {
    ersteZahl = parseFloat(prompt("Kérlek, adj meg egy érvényes számot az első értékhez!"));
}
while (isNaN(zweiteZahl)) {
    zweiteZahl = parseFloat(prompt("Kérlek, adj meg egy érvényes számot a második értékhez!"));
}
alert(`Az összegük: ${ersteZahl + zweiteZahl}`);

// 5.
const piWert = 3.14;

// 6.
let körSugar = parseFloat(prompt("Add meg a kör sugarát!"));
while (isNaN(körSugar)) {
    körSugar = parseFloat(prompt("Érvénytelen érték. Add meg a kör sugarát számként!"));
}
console.log(`K = ${2 * körSugar * piWert}`);
console.log(`T = ${körSugar ** 2 * piWert}`);

// 7.
let monat = parseInt(prompt("Adj meg egy hónapot (1-12 között)!"));
while (monat < 1 || monat > 12 || isNaN(monat)) {
    monat = parseInt(prompt("Érvénytelen hónap. Adj meg egy számot 1 és 12 között!"));
}
switch (monat) {
    case 12:
    case 1:
    case 2:
        alert("Tél");
        break;
    case 3:
    case 4:
    case 5:
        alert("Tavasz");
        break;
    case 6:
    case 7:
    case 8:
        alert("Nyár");
        break;
    case 9:
    case 10:
    case 11:
        alert("Ősz");
        break;
    default:
        alert("Nem ismerem ezt a hónapot!");
}

// 8.
let unterGrenze = parseInt(prompt("Add meg az alsó határt!"));
let felsőHatár = parseInt(prompt("Add meg a felső határt!"));
while (isNaN(unterGrenze) || isNaN(felsőHatár) || felsőHatár <= unterGrenze) {
    unterGrenze = parseInt(prompt("Add meg az alsó határt!"));
    felsőHatár = parseInt(prompt("Adj meg egy nagyobb felső határt!"));
}
let szamokSzama = felsőHatár - unterGrenze + 1;
let osszeg = 0;
let szorzat = 1;
for (let aktuálisSzám = unterGrenze; aktuálisSzám <= felsőHatár; aktuálisSzám++) {
    osszeg += aktuálisSzám;
    szorzat *= aktuálisSzám;
}
console.log(`Számok száma: ${szamokSzama}`);
console.log(`Számok összege: ${osszeg}`);
console.log(`Számok átlaga: ${osszeg / szamokSzama}`);
console.log(`Számok szorzata: ${szorzat}`);