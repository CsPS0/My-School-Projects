"use strict"

const headEl = document.getElementById("header");
const dataEl = document.getElementById("data");
const shortEl = document.getElementById("short");
const stationsEl = document.getElementById("stations");
const coverEl = document.getElementById("cover");

function title() {
    const h = headEl.getElementsByTagName("h1")[0];
    const cim = h ? h.textContent : 'ismeretlen';
    
    const p = document.createElement("p");
    p.appendChild(document.createTextNode('Cím: '));

    const span = document.createElement("span");
    span.appendChild(document.createTextNode(cim));

    p.appendChild(span);
    return p;
}

function episodes() {
    let count = 0;
    let episodesUl = null;
    const list = dataEl.getElementsByTagName("li");

    for (let i = 0; i < list.length; i++) {
        const strongs = list[i].getElementsByTagName("strong");
        if (strongs.length && strongs[0].textContent.trim() == 'Részek') {
            const uls = list[i].getElementsByTagName("ul");
            if (uls.length) {
                episodesUl = uls[0];
                break;
            }
        }
    }

    if (episodesUl) {
        count = episodesUl.getElementsByTagName("li").length;
    }

    const p = document.createElement("p");
    p.textContent = 'Részek száma: ';
    
    const span = document.createElement('span');
    span.textContent = String(count);

    p.appendChild(span);
    return p;
}

function pages() {
    const lis = dataEl.getElementsByTagName("li");
    let count = 0;

    for(let i = 0; i < lis.length; i++) {
        const strongs = lis[i].getElementsByTagName("strong");
        if (strongs.length && strongs[0].textContent == "Oldalszám:") {
            const spans = lis[i].getElementsByTagName("span");
            if (spans.length) {
                count = spans[0].textContent;
            }
            break;
        }
    }

    const p = document.createElement("p");
    p.textContent = "Első rész hossza: ";

    const span = document.createElement("span");
    span.textContent = count;

    p.appendChild(span);
    return p;
}

function first() {
    const lis = dataEl.getElementsByTagName("li");
    let episodesUl = null;

    for (let i = 0; i < lis.length; i++) {
        const strongs = lis[i].getElementsByTagName("strong");
        if (strongs.length && strongs[0].textContent == 'Részek') {
            const uls = lis[i].getElementsByTagName("ul");
            if (uls.length) {
                episodesUl = uls[0];
                break;
            }
        }
    }

    if (episodesUl) {
        const items = episodesUl.getElementsByTagName("li");
        if (items.length) {
            items[0].classList.add('first');
        }
    }
}

function vnh() {
    const li = document.createElement("li");
    li.textContent = 'VDNH';

    const firstEl = stationsEl.firstElementChild;
    if (firstEl) {
        stationsEl.insertBefore(li, firstEl);
    }
}

function coverSwap() {
    const img = coverEl.getAttribute("src");

    if (img == "2033.jpg") {
        coverEl.setAttribute("src", "2033-new.jpg");
        coverEl.setAttribute("alt", "Metró 2033 könyv új borító");
        document.body.style.setProperty("--main-color", "#ce322b");
    } else {
        coverEl.setAttribute("src", "2033.jpg");
        coverEl.setAttribute("alt", "Metró 2033 könyv borító");
        document.body.style.setProperty("--main-color", "#ffcc00");
    }
}

function visited(e) {
    const el = e.target;
    const mainColor = getComputedStyle(document.body).getPropertyValue("--main-color");
    el.style.backgroundColor = mainColor;
    el.style.color = "#000";
    el.style.borderStyle = "double";
    el.style.cursor = "crosshair";
}

// Szebített futtatás :)
function main() {
    shortEl.append(title(), episodes(), pages());
    first();
    vnh();

    coverEl.addEventListener("click", coverSwap);

    const stations = stationsEl.children;
    for (let i = 0; i < stations.length; i++) {
        stations[i].addEventListener('click', visited);
    }
}

main();