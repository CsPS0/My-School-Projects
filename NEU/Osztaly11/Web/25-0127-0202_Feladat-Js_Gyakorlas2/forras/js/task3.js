"use strict"

//3.1
let szam = parseFloat(window.prompt("Adj meg egy számot: "))
let tomb = []

while (isNaN(szam)) {
    szam = parseFloat(window.prompt("Kérlek egy számot adj meg!"))
    tomb.push(szam)
}
//3.2
let osszeg = 0
for (let i = 0; i < tomb.length; i++) {
    osszeg+=tomb[i]
}

//3.3
console.log("A számok összege: ", osszeg)

//3.4
const atlag = osszeg / tomb.length
console.log("A számok átlaga: ", Math.round(atlag, 2))