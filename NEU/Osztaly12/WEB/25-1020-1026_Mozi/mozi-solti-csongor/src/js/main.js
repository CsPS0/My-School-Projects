'use strict'

function createSpan(szoveg, osztalyok = []) {
    const span = document.createElement("span");
    span.textContent = szoveg;
    span.classList.add(...osztalyok);
    return span;
}

function capitalize(szoveg) {
    const [elso, ...tobbi] = szoveg;
    return `${elso.toUpperCase()}${tobbi.join("")}`;
}

const fSzaml = document.getElementById("film-count");
fSzaml.textContent = "";
fSzaml.append(createSpan(films.length, ["text-lg"]), createSpan(" film", ["stat-text"]));

const fHossz = document.getElementById("film-length");
fHossz.textContent = "";
fHossz.append(createSpan(films.length * 120, ["text-lg"]), createSpan(" órányi élmény", ["stat-text"]));
fHossz.classList.add("astrict");

const olcsoJegyAr = document.getElementById("cheapest-ticket-price");
const olcsoJegyResz = document.getElementById("cheapest-ticket-details");

const rendJegyek = [...tickets].sort((a, b) => a[2] - b[2]);
const [olcsoNev, olcsoTipus, olcsoAr] = rendJegyek[0];

olcsoJegyAr.textContent = "";
olcsoJegyAr.append(createSpan(olcsoAr, ["text-lg"]), createSpan(" forinttól", ["stat-text"]));
olcsoJegyAr.classList.add("double-astrict");

olcsoJegyResz.append(` ${capitalize(olcsoNev)} (${olcsoTipus})`);

function createTicketCard(nev, tipus, ar) {
    const kartya = document.createElement("div");
    kartya.classList.add("ticket-card");

    const h3 = document.createElement("h3");
    h3.textContent = capitalize(nev);

    const pTipus = document.createElement("p");
    pTipus.textContent = tipus;
    pTipus.classList.add("ticket-type");

    const pAr = document.createElement("p");
    pAr.textContent = `${ar} forint`;
    pAr.classList.add("ticket-price");

    kartya.append(h3, pTipus, pAr);
    return kartya;
}

function generateTicketCards() {
    const kartyak = [];
    for (const jegy of tickets) {
        const [nev, tipus, ar] = jegy;
        const kartya = createTicketCard(nev, tipus, ar);
        kartyak.push(kartya);
    }
    const jegyekEl = document.getElementById("tickets");
    jegyekEl.replaceChildren(...kartyak);
}
generateTicketCards();

function createFilmCard(nev, kep) {
    const kartya = document.createElement("div");
    kartya.classList.add("film-card");

    const img = document.createElement("img");
    img.src = kep;
    img.alt = nev;

    const h3 = document.createElement("h3");
    h3.textContent = nev;

    kartya.append(img, h3);
    return kartya;
}

function generateFilmCards() {
    const mapFilmek = films.map(film => {
        const kepNev = film.replaceAll(" ", "_");
        const kepUtvonal = `src/assets/images/${kepNev}.jpeg`;
        return [film, kepUtvonal];
    });

    const kartyak = [];
    const topFilmek = mapFilmek.splice(0, 3);

    for (const film of topFilmek) {
        const [nev, kep] = film;
        kartyak.push(createFilmCard(nev, kep));
    }

    const filmekEl = document.getElementById("films");
    filmekEl.replaceChildren(...kartyak);

    const kepek = [];
    for (const film of mapFilmek) {
        const [nev, kep] = film;
        const img = document.createElement("img");
        img.src = kep;
        img.alt = nev;
        kepek.push(img);
    }

    const otherFilmsEl = document.getElementById("other-films");
    otherFilmsEl.replaceChildren(...kepek);
}
generateFilmCards();