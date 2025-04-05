"use strict"

//1.1 feladat
let alsohatar = 1;
let felsohatar = 10;

function random(alsohatar, felsohatar) {
    let toPut = Math.floor(Math.round() * (felsohatar - alsohatar + 1) + alsohatar)
    console.log(toPut)
}

//1.2 feladat
document.getElementById('feladat2').addEventListener('click', function() {
    var ahsz = window.prompt("Add meg az alsóhatárt: ")
    var fhsz = window.prompt("Add meg a felsőhatárt: ")
    var randomChoise = [ahsz, fhsz];
    var chosenValue = randomChoise[Math.round(Math.random())];

    window.alert("A két szám közöti random eredmény: ", chosenValue)
});