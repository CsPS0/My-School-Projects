"use strict";

// 1. Feladat
let randomSzamok = [];

if (randomSzamok.length === 0) {
    for (let i = 0; i < 5; i++) {
        let randomInt = Math.floor(Math.random() * 100) + 1;
        randomSzamok.push(randomInt);
    }
} else {
    console.log("A tömb már inicializálva van.");
}
console.log("Tömb:", randomSzamok);

// 2. Feladat
let gyokTomb = [];
for (let i = 0; i < randomSzamok.length; i++) {
    let gyok = Math.sqrt(randomSzamok[i]).toFixed(2);
    gyokTomb.push(gyok);
}
console.log("Négyzetgyökök:", gyokTomb);

// 3. Feladat
function getMinMax(tomb, tipus) {
    if (tipus === "min") {
        return Math.min(...tomb);
    } else if (tipus === "max") {
        return Math.max(...tomb);
    }
}

let legkisebb = getMinMax(randomSzamok, "min");
let legnagyobb = getMinMax(randomSzamok, "max");

console.log("Legkisebb szám:", legkisebb);
console.log("Legnagyobb szám:", legnagyobb);

// 4. Feladat
let osszeg = 0;
for (let i = 0; i < randomSzamok.length; i++) {
    osszeg += randomSzamok[i];
}
let atlag = Math.round(osszeg / randomSzamok.length);
console.log("Átlag:", atlag);

// 5. Feladat
let paros = [];
let paratlan = [];
for (let i = 0; i < randomSzamok.length; i++) {
    if (randomSzamok[i] % 2 === 0) {
        paros.push(randomSzamok[i]);
    } else {
        paratlan.push(randomSzamok[i]);
    }
}
console.log("Páros számok:", paros);
console.log("Páratlan számok:", paratlan);

// 6. Feladat
let hatvanyok = [];
for (let i = 0; i < randomSzamok.length; i++) {
    let hatvany = Math.pow(randomSzamok[i], i + 1);
    hatvanyok.push(hatvany);
}
console.log("Számok hatványai:", hatvanyok);

// 7. Feladat
let alacsony = [];
let kozepes = [];
let magas = [];
for (let i = 0; i < randomSzamok.length; i++) {
    if (randomSzamok[i] < 30) {
        alacsony.push(randomSzamok[i]);
    } else if (randomSzamok[i] >= 30 && randomSzamok[i] <= 69) {
        kozepes.push(randomSzamok[i]);
    } else {
        magas.push(randomSzamok[i]);
    }
}
console.log("Alacsony:", alacsony);
console.log("Közepes:", kozepes);
console.log("Magas:", magas);