"use strict";

let users = [];

function objectsGenerate(also = 1000, felso = 9999) {
    return Math.floor(Math.random() * (felso - also + 1) + also);
}

for (let i = 0; i < 10; i++) {
    let name = String(objectsGenerate());
    let pass = String.fromCharCode(objectsGenerate(65, 90));
    let id = i;
    let principal = objectsGenerate(10000, 100000);
    let rate = Math.floor(Math.random() * (8 - 1 + 1) + 1);
    let years = Math.floor(Math.random() * (5 - 1 + 1) + 1);
    let timesPerYear = Math.random() < 0.5 ? 1 : 12;

    users.push({ name, pass, id, principal, rate, years, timesPerYear });
}

function kamatosKamat(P, r, t, n) {
    r = r / 100;
    let A = P * Math.pow(1 + r / n, n * t);
    return Math.round(A);
}

for (let user of users) {
    user.endBalance = kamatosKamat(user.principal, user.rate, user.years, user.timesPerYear);
}

console.table(users);