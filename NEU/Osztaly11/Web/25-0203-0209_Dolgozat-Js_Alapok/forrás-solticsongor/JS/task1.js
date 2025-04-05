"use strict"

//1.
function randomFloat() {
    let alsoHatar = 69;
    let felsoHatar = 420;

    let randomSzam = parseFloat(Math.random(alsoHatar, felsoHatar));
    if (randomSzam == 1) {
        let kerekítettSzam = Math.round(randomSzam)
        console.log("A generált szám: ", kerekítettSzam)
    }
    else {
        console.log("A generált szám: ", randomSzam)
    }
}

//2.
let szam1 = window.prompt("Adj meg egy számot: ")
let szam2 = window.prompt("Adj meg még egy számot: ")
let randomGeneralt = parseFloat(Math.random(szam1, szam2))

if (szam1 > szam2) {
    window.alert("“A felső határnak nagyobbnak kell lennie, mint az alsó határ!")
}

else {
    window.alert("A létrehozott random float: ", randomGeneralt)
}