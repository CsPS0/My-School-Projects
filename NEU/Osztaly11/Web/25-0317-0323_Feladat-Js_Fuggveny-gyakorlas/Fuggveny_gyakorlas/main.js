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

const kepek = [
    {id: 1, image: "./img/kep1.png"},
    {id: 2, image: "./img/kep2.webp"},
    {id: 3, image: "./img/kep3.webp"}
];

function createGalleryItem (imageSrc, caption) {
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

function createBox(width, height, color) {
    const box = document.createElement("div");
    box.style.width = `${width}px`;
    box.style.height = `${height}px`;
    box.style.backgroundColor = color;
    box.classList.add("rounded", "m-2", "shadow-md", "transition", "duration-300", "hover:scale-105");
    return box;
}

const createCard = function(title, content) {
    const card = document.createElement("div");
    card.className = "bg-white rounded-lg shadow-md p-4 mb-4";
    const h2 = document.createElement("h2");
    h2.className = "text-lg font-semibold mb-2";
    h2.textContent = title;
    const p = document.createElement("p");
    p.className = "text-gray-600";
    p.textContent = content;
    card.append(h2, p);
    return card;
};

let createList = function(items, ordered = false) {
    const list = ordered ? document.createElement("ol") : document.createElement("ul");
    list.classList.add(ordered ? "list-decimal" : "list-disc", "ml-6");
    items.forEach(item => {
        const li = document.createElement("li");
        li.textContent = item;
        list.appendChild(li);
    });
    return list;
};

const createColorCard = (color, name) => {
    const card = document.createElement("div");
    card.className = `w-full h-20 rounded-lg flex items-center justify-center mb-2 text-white font-bold ${color}`;
    card.textContent = name;

    card.addEventListener("mouseover", () => {
        card.classList.add("shadow-lg", "transform", "scale-105");
    });

    card.addEventListener("mouseout", () => {
        card.classList.remove("shadow-lg", "transform", "scale-105");
    });

    return card;
};

const createWeatherIcon = (type, description) => {
    const container = document.createElement("div");
    container.className = "flex flex-col items-center justify-center p-4";

    const iconDiv = document.createElement("div");
    iconDiv.className = "text-4xl mb-2";

    switch (type) {
        case "sun":
            iconDiv.textContent = "☀️"; break;
        case "cloud":
            iconDiv.textContent = "☁️"; break;
        case "rain":
            iconDiv.textContent = "🌧️"; break;
        case "snow":
            iconDiv.textContent = "❄️"; break;
        default:
            iconDiv.textContent = "🌈";
    }

    const descSpan = document.createElement("span");
    descSpan.textContent = description;
    descSpan.className = "text-gray-700";

    container.append(iconDiv, descSpan);
    return container;
};

const createStatBar = (label, percentage) => {
    const container = document.createElement("div");
    container.className = "mb-4";

    const labelDiv = document.createElement("div");
    labelDiv.className = "flex justify-between mb-1";

    const nameSpan = document.createElement("span");
    nameSpan.textContent = label;
    nameSpan.className = "text-gray-700";

    const percentSpan = document.createElement("span");
    percentSpan.textContent = `${percentage}%`;
    percentSpan.className = "text-gray-700";

    labelDiv.append(nameSpan, percentSpan);

    const barContainer = document.createElement("div");
    barContainer.className = "w-full bg-gray-200 rounded-full h-2.5";

    const bar = document.createElement("div");
    bar.className = "bg-indigo-600 h-2.5 rounded-full";
    bar.style.width = `${percentage}%`;

    barContainer.appendChild(bar);
    container.append(labelDiv, barContainer);
    return container;
};

(function() {
    document.addEventListener("DOMContentLoaded", () => {
        const main = document.querySelector("main");
        main.classList.add("p-6", "space-y-6");

        main.appendChild(createCard("Welcome!", "Ez egy kártya példa function expression-nel."));
        main.appendChild(createCard("Hello CsPS!", "Ez a projekt gyönyörű!"));

        const fruits = ["Alma", "Banán", "Cseresznye"];
        main.appendChild(createList(fruits));

        main.appendChild(createBox(100, 100, "teal"));
        main.appendChild(createBox(150, 80, "orange"));

        const button = createButton("Kattints rám!", () => alert("Szép munka!"));
        main.appendChild(button);

        main.appendChild(createColorCard("bg-red-500", "Veszély"));
        main.appendChild(createColorCard("bg-green-600", "Biztonság"));

        main.appendChild(createWeatherIcon("sun", "Napos idő"));
        main.appendChild(createWeatherIcon("rain", "Esős idő"));

        main.appendChild(createStatBar("JavaScript", 90));
        main.appendChild(createStatBar("Tailwind CSS", 75));
    });
})();