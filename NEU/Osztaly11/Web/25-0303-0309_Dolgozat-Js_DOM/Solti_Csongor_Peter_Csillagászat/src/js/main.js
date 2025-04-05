"use strict";

const objektumok = [
    { id: 1, nev: "Hold", tipus: "Természetes hold", kep: "moon.png" },
    { id: 2, nev: "Mars", tipus: "Bolygó", kep: "mars.png" },
    { id: 3, nev: "Androméda", tipus: "Galaxis", kep: "andromeda.png" },
    { id: 4, nev: "Orion-köd", tipus: "Köd", kep: "orion.jpg" },
    { id: 5, nev: "Föld", tipus: "Bolygó", kep: "earth.png" },
    { id: 6, nev: "Halley-üstökös", tipus: "Üstökös", kep: "halley.jpg" }
];

const megfigyeltObjektumok = [];
const toroltObjektum = [];

document.body.classList.add('bg-skate-900', 'min-h-screen')

// Header
const header = document.createElement("header")
header.classList.add('bg-slate-800', 'border-b', 'border-slate-700', 'py-4')

const h1 = document.createElement("h1")
h1.textContent = "Csillagászati Objektumok"
h1.classList.add('text-3xl', 'font-bold', 'text-center', 'text-teal-400')

const p = document.createElement("p")
p.textContent = "Megfigyelt objektumok: "
p.classList.add('text-center', 'text-teal-200', 'mt-2')

const span = document.createElement("span")
span.id = "szamlalo"
span.textContent = "0"

p.append(span)
header.append(h1, p)
document.body.appendChild(header)

// Main
const main = document.createElement("main")
main.classList.add('container', 'mx-auto', 'px-4', 'py-8')

const kartyaContainer = document.createElement("div")
kartyaContainer.classList.add('grid', 'grid-cols-1', 'md:grid-cols-2', 'lg:grid-cols-3', 'gap-6')

main.append(kartyaContainer)
document.body.appendChild(main)

// Footer
const footer = document.createElement("footer")
footer.textContent = "Készítette: Solti Csongor Péter 🤓"
footer.classList.add('bg-slate-800', 'text-teal-200', 'text-center', 'py-4', 'mt-8')
document.body.appendChild(footer)

function letrehozKartya(objektum) {
    const card_body = document.createElement("div")
    card_body.classList.add('bg-slate-800', 'rounded-lg', 'shadow-xl', 'p-4')

    const card_img = document.createElement("img")
    card_img.src = `./img/${objektum.kep}`
    card_img.classList.add('w-full', 'h-48', 'object-cover', 'rounded-lg', 'mb-4')

    const card_h2 = document.createElement("h2")
    card_h2.textContent = objektum.nev

    // Megfigyelt objektumok kiemelése
    if (megfigyeltObjektumok.includes(objektum.id)) {
        card_h2.classList.add('text-teal-400', 'line-through', 'text-xl')
    } else {
        card_h2.classList.add('text-white', 'text-xl')
    }

    const card_p = document.createElement("p")
    card_p.textContent = objektum.tipus
    card_p.classList.add('text-slate-300')

    const card_div = document.createElement("div")
    card_div.classList.add('flex', 'gap-2', 'mt-4')

    const megfigyelesGomb = document.createElement("button")
    if (megfigyeltObjektumok.includes(objektum.id)) {
        megfigyelesGomb.classList.add('bg-slate-600', 'text-white', 'px-3', 'py-1', 'rounded')
        megfigyelesGomb.textContent = "Visszavon"
    } else {
        megfigyelesGomb.classList.add('bg-teal-600', 'text-white', 'px-3', 'py-1', 'rounded')
        megfigyelesGomb.textContent = "Megfigyel"
    }

    megfigyelesGomb.onclick = () => valtMegfigyelesAllapot(objektum.id);

    const torlesGomb = document.createElement("button")
    torlesGomb.textContent = "Töröl"
    torlesGomb.classList.add('bg-orange-600', 'text-white', 'px-3', 'py-1', 'rounded')
    torlesGomb.onclick = () => torolObjektum(objektum.id);

    card_div.append(megfigyelesGomb, torlesGomb)
    card_body.append(card_img, card_h2, card_p, card_div)

    return card_body
}

function frissitMegjelenites() {
    while (kartyaContainer.firstChild) {
        kartyaContainer.removeChild(kartyaContainer.firstChild);
    }

    for (let i = 0; i < objektumok.length; i++) {
        const kartya = letrehozKartya(objektumok[i]);
        if (kartya) kartyaContainer.append(kartya);
    }

    document.getElementById('szamlalo').textContent = megfigyeltObjektumok.length;
}

function valtMegfigyelesAllapot(id) {
    let fake = true
    for (let i = 0; i < megfigyeltObjektumok; i++) {
        if (megfigyeltObjektumok[i] == id) {
            megfigyeltObjektumok.pop(objektumok[i])
            fake = false
        }
    }

    if (hamis == true) {
        megfigyeltObjektumok.push(id)
    }

    frissitMegjelenites();
}

function torolObjektum(id) {
    let fake = true
    for (let i = 0; i < toroltObjektum.length; i++) {
        if (id === torolObjektum[i]) {
            torolObjektum.splice(id, 1)
            fake = false
        }
    }
    if (fake == true) {
        torolObjektum.push(id)
    }
    frissitMegjelenites();
}
frissitMegjelenites();