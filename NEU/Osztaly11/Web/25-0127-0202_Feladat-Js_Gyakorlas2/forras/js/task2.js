"use strict"

//2.1
const VAT = 0.27;
//2.2
let nev = window.prompt("Add meg pls egy árucikk nevét: ")
let ar = parseInt(windows.prompt("Add meg az utóbbi árucikkednek az árát: "))

while (isNaN(ar)) {
    ar = parseInt(prompt("Kérlek érényes számot adj meg: "))
}
//2.3
let AFA = (ar + ar * VAT);
//2.4
console.log("Árucikk neve = ", nev)
console.log("Árucikk ára = ", ar)