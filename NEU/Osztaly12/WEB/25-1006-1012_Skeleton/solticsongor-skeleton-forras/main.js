"use strict"

// 1. feladat
function task01(szavak) {
  let teljesHossz = 0;
  for (const szo of szavak) {
    teljesHossz += szo.length;
  }
  return (teljesHossz / szavak.length).toFixed(2);
}

// 2. feladat
function task02(szamok) {
  const eredmeny = [];
  for (const szam of szamok) {
    eredmeny.push(Math.round(Math.abs(szam) / 3));
  }
  return eredmeny;
}

// 3. feladat
function task03(szamok) {
  for (let i = szamok.length - 1; i >= 0; i--) {
    if (szamok[i] % 3 === 0 && szamok[i] % 5 === 0) {
      return szamok[i];
    }
  }
  return null;
}

// 4. feladat
function task04(szavak) {
  for (let i = 0; i < szavak.length; i++) {
    if (szavak[i].length === 5) {
      return i;
    }
  }
  return -1;
}

// 5. feladat
function task05(szamok) {
  const eredmeny = [];
  for (const szam of szamok) {
    if (szam > 0 && szam % 2 === 0) {
      eredmeny.push(szam);
    }
  }
  return eredmeny;
}