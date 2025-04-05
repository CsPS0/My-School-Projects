"use strict"

function addDistrict() {
    const kontener = document.getElementById("districts");
    const selectedDistrictIndex = getSelectedDistrict();
    if (selectedDistrictIndex === null) {
        alert("No district selected!");
        console.error("No district selected!");
        return;
    }
    const selectedDistrict = districts[selectedDistrictIndex];
    const kartyaElem = document.createElement("div");
    kartyaElem.classList.add("card");
    const keruletNev = document.createElement("h2");
    keruletNev.textContent = selectedDistrict.name;
    const keruletLeiras = document.createElement("p");
    keruletLeiras.textContent = selectedDistrict.description;
    const keruletKep = document.createElement("img");
    keruletKep.src = `./src/images/${selectedDistrict.image}`;
    keruletKep.alt = selectedDistrict.name;
    kartyaElem.append(keruletNev, keruletLeiras, keruletKep);
    kontener.append(kartyaElem);
}

function clearAll() {
    const elem = document.getElementById("districts");

    if (!elem.hasChildNodes()) {
        alert("No items to be removed!");
        console.error("No items to be removed!");
        return;
    }

    elem.innerHTML = "";
}

function clearRandom() {
    const elem = document.getElementById("districts");

    if (!elem.hasChildNodes()) {
        alert("No items to be removed!");
        console.error("No items to be removed!");
        return;
    }

    const randomIndex = Math.floor(Math.random() * elem.children.length);
    elem.children[randomIndex].remove();
}