"use strict";

// 2.
console.log("Hello World!");

// 3.
let familienName = "Solti";
console.log(familienName);
console.log(typeof familienName);

// 4.
let vorName = "Csongor Péter";

// 5.
let teljesName = familienName + " " + vorName;
console.log(teljesName);

// 6.
let zahlA = 10;
let szamB = "10";

// 6a.
if (zahlA == szamB) {
    console.log("A két változó értéke egyezik.");
} else {
    console.log("A két változó értéke nem egyezik.");
}

// 6b.
if (zahlA === szamB) {
    console.log("A két változó értéke és típusa egyezik.");
} else {
    console.log("A két változó értéke vagy típusa nem egyezik.");
}

// 7.
console.log(zahlA + szamB);
console.log(zahlA - szamB);
console.log(zahlA * szamB);
console.log(zahlA / szamB);

// 8.
let randomZahl = 5;
console.log((zahlA + randomZahl) - szamB);

// 9.
for (let i = 1; i <= 10; i++) {
    console.log(i);
}

console.log("-----")