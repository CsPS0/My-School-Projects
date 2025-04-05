"use strict";

function sortArr(arr=[2, 5, 3, 7, 4]){
    return arr.sort(function(a, b){return a-b})
}
// nézze meg mit ad vissza a meghívás: console.log(sortArr())
//Innentől lefelé írjon

//1.
let szamokTomb = []
let bekertSzamok
do {
    bekertSzamok = window.prompt("Adj meg egy számot!")
    szamokTomb.push(bekertSzamok)
} while (bekertSzamok === isNaN)
//2.
for (i = 0; i < szamokTomb.length; i++) {
    //let szamokSzorzata = 
}
//3.
console.log("A számok szorzata: ", szamokSzorzata)
//4.
let szamokMedian = sortArr(szamokTomb)
let szamokMedianKerekites = Math.round(szamokMedian, 2)
console.log("A számok mediánja: ", szamokMedianKerekites)