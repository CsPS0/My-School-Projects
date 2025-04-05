"use strict"
//1.
let daysInWeek = ["hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat", "vasárnap"]
//2.
let megadottNap = window.prompt("Adj meg egy sorszámot a hétből (1-7)!")
if (megadottNap < 1 && megadottNap > 7) {
    console.log("Érvénytelen nap!")
}
else {
    for (i = 1; i < daysInWeek.length; i++) {
        console.log("A nap neve:", megadottNap[i])
    }
    //3.
    switch (megadottNap) {
        case 0 && 1 && 2 && 3 && 4: console.log("Hétköznap");break;
        case 5 && 6: console.log("Hétvége");break;
    //4.
    }
    switch (megadottNap) {
        case 1 && 3 && 5 && 7: console.log("A nap sorszáma páros");break;
        case 2 && 4 && 6: console.log("A nap sorszáma páratlan");break;
    }
}