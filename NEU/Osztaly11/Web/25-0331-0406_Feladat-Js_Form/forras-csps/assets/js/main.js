"use strict";

document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("inventory-form");
    const tableBody = document.getElementById("inventory-table-body");
    const peopleSelect = document.getElementById("szemelyek");

    people.forEach(person => {
        const option = document.createElement("option");
        option.value = person;
        option.textContent = person;
        peopleSelect.appendChild(option);
    });

    function renderItems() {
        tableBody.innerHTML = "";
        items.forEach(item => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${item.name}</td>
                <td>${item.qty}</td>
                <td>${item.person}</td>
                <td><span class="tag">${item.date}</span></td>
                <td>${item.quality}</td>
            `;
            tableBody.appendChild(row);
        });
    }

    function validateForm() {
        const name = form.nev.value.trim();
        const qty = parseInt(form.darabszam.value);
        const person = form.szemelyek.value;
        const date = form.datum.value;
        const quality = form.quality.value;

        if (!name || !qty || !person || !date || !quality) {
            alert("Minden mezőt ki kell tölteni!");
            return false;
        }
        if (qty < 1) {
            alert("A darabszámnak nagyobbnak kell lennie 0-nál!");
            return false;
        }
        if (!people.includes(person)) {
            alert("A 'Bejegyezte' mező értéke nem helyes!");
            return false;
        }
        if (date.length !== 10) {
            alert("A dátumnak helyes formátumban kell lennie!");
            return false;
        }

        return true;
    }

    form.addEventListener("submit", (event) => {
        event.preventDefault();

        if (validateForm()) {
            const newItem = {
                name: form.nev.value,
                qty: parseInt(form.darabszam.value),
                person: form.szemelyek.value,
                date: form.datum.value,
                quality: form.quality.value,
            };
            items.push(newItem);
            renderItems();
            form.reset();
        }
    });
    renderItems();
});