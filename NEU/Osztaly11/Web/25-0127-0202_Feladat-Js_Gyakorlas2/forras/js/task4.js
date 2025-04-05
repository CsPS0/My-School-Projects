"use strict"

//4.1
let months = ["január", "február",
    "március", "április", "május", "június", "július", "augusztus", "szeptember", "október",
    "november", "december"]

//4.2
let sorszam = parseInt(window.prompt("Add meg egy hónap sorszámát (1-12): "))

if (sorszam < 1 || sorszam > 12) {
    console.log("Érvénytelen hónap!")
}
else {
    const honapNev = months[sorszam -1 ]
    console.log("A hónap neve: ", honapNev)
}

//4.3
let evszak = "";
if (sorszam === 12 || sorszam === 1 || sorszam === 2) {
    console.log("Téli hónap")
}

else if (sorszam === 3 ||sorszam === 4 || sorszam === 5) {
    console.log("Tavaszi hónap")
}

else if (sorszam === 6 || sorszam === 7 || sorszam === 8) {
    console.log("Nyári hónap")
}

else if (sorszam === 9 || sorszam === 10 || sorszam === 11) {
    console.log("Őszi hónap")
}

//4.4
if (sorszam % 2 === 0) {
    console.log("páros")
}
else {
    console.log("páratlan")
}

//4.5
switch(sorszam) {
    case 1:
    case 3:
    case 5:
    case 7:
    case 8:
    case 10:
    case 12:
        console.log("A hónap 31 napos")
    case 2:
        console.log("A hónap 28 napos")
    case 4:
    case 6:
    case 9:
    case 11:
        console.log("A hónap 30 napos")
}