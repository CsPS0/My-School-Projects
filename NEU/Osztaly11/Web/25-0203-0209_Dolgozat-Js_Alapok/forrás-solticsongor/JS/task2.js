"use strict"

//1.
const DISCOUNT = parseFloat(0.15);
//2.
let termekNev
do {
    termekNev = toString(window.prompt("Adj meg egy termék nevet!"))
} while (termekNev === isNaN)
let termekAr = window.prompt("Add meg a terméked árát!")
//3.
let kedvezmenyesAr = ((termekAr - termekAr) * DISCOUNT)
//4.
console.log("A(z) " + termekNev + "kedvezményes ára: " + kedvezmenyesAr + "forint")