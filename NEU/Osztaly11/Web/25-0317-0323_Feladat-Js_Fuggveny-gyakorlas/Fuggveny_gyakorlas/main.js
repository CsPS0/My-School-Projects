"use strict"

const app = document.getElementById("app")
app.classList.add("container", "mx-auto", "px-4", "py-8", "max-w-4xl")

function createHeader(title, subtitle) {
    const header = document.createElement("header")
    header.classList.add("text-center", "mb-10")
    const focim = document.createElement("h1")
    focim.textContent = title
    focim.classList.add("text-4xl", "font-bold", "text-indigo-700", "mb-3")
    const alcim = document.createElement("p")
    alcim.textContent = subtitle
    alcim.classList.add("text-xl", "text-gray-600")
    header.append(focim, alcim)
    return header
}

function createSection(title, description, bgColor = "bg-white") {
    const section = document.createElement("section")
    section.classList.add(`${bgColor}`, "p-6", "rounded-lg", "shadow-md", "md-10")
    const szcim = document.createElement("h2")
    szcim.textContent = title
    szcim.classList.add("text-2xl", "font-semibold", "text-indigo-800", "mb-3")
    const szleiras = document.createElement("p")
    szleiras.textContent = description
    szleiras.classList.add("text-gray-700", "mb-6")
    section.append(szcim, szleiras)
    return section
}

function createButton(text, onClick, color = "indigo") {
    const button = document.createElement("button")
    button.textContent = text
    button.classList.add(`bg-${color}-500`, `hover:bg-${color}-600`, "text-white", "py-2", "px-4", "rounded-md", "transition", "duration-200")
    button.addEventListener("click", onClick)
    return button
}

let createCard
createCard = function(title, content, buttonText, buttonAction) {
    const kartya = document.createElement("div")
    kartya.classList.add("bg-white", "rounded-lg", "shadow-md", "p-6", "mb-6")
    const kcim = document.createElement("h3")
    kcim.textContent = title
    kcim.classList.add("text-xl", "font-semibold", "text-gray-800", "mb-3")
    const ktartalom = document.createElement("div")
    ktartalom.classList.add("text-gray-700", "mb-4")
    if (typeof content === "string") {
        ktartalom.textContent = content
    }
    else {
        ktartalom.appendChild(content)
    }
    const kgomb = createButton(buttonText, buttonAction)
    kartya.append(kcim, ktartalom, kgomb)
    return kartya
}

const kepek = [
    {id: 1, image: "./img/kep1.png"},
    {id: 2, image: "./img/kep2.webp"},
    {id: 3, image: "./img/kep3.webp"}
];

let createGalleryItem
createGalleryItem = function(imageSrc, caption) {
    const galeria = document.createElement("div")
    galeria.classList.add("overflow-hidden", "rounded-lg", "shadow-md")
    const kepDiv = document.createElement("div")
    kepDiv.classList.add("w-full", "h-48", "bg-gray-300", "flex", "items-center", "justify-center")
    const kepSzoveg = document.createElement("span")
    kepSzoveg.textContent = "Kép: " + imageSrc
    kepSzoveg.classList.add("text-gray-600")
    kepDiv.appendChild(kepSzoveg)
    const captionDiv = document.createElement("div")
    captionDiv.textContent = caption
    captionDiv.classList.add("p-3", "text-center", "text-gray-700")
    galeria.append(kepDiv, captionDiv)
    return galeria;
};

/*
const galleryContainer = document.getElementById("gallery");
for (let i = 0; i < kepek.length; i++) {
    const kepelem = kepek[i];
    const newGalleryItem = createGalleryItem(kepelem.image, "Képaláírás " + kepelem.id);
    galleryContainer.appendChild(newGalleryItem);
}
*/

let createList
createList = function(items, ordered = "false") {
    if (ordered === "true") {
        const lista = document.createElement("ul")
        lista.classList.add("list-decimal", "ml-6")
    }
    else {
        const lista = document.createElement("ol")
        lista.classList.add("list-disc", "ml-6")
    }

    for (let i = 0; i < items; i++) {
        const elem = document.createElement("li")
        elem.textContent = 
    }
}