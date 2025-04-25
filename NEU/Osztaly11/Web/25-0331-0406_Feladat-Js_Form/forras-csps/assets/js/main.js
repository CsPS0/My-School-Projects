"use strict";

document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("inventory-form");
    const tableBody = document.getElementById("inventory-table-body");
    const peopleSelect = document.getElementById("szemelyek");
    const errorDialog = document.querySelector("dialog");
    peopleSelect.innerHTML = '<option value="">Válassza ki a személyt...</option>';
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
        const quality = document.querySelector('input[name="quality"]:checked')?.value;

        if (!name || isNaN(qty) || !person || !date || !quality) {
            errorDialog.showModal();
            return false;
        }
        
        if (qty <= 0) {
            errorDialog.showModal();
            return false;
        }
        
        if (!people.includes(person)) {
            errorDialog.showModal();
            return false;
        }
        
        if (date.length !== 10) {
            errorDialog.showModal();
            return false;
        }

        return true;
    }

    form.addEventListener("submit", (event) => {
        event.preventDefault();

        if (validateForm()) {
            const newItem = {
                name: form.nev.value.trim(),
                qty: parseInt(form.darabszam.value),
                person: form.szemelyek.value,
                date: form.datum.value,
                quality: document.querySelector('input[name="quality"]:checked').value,
            };
            items.push(newItem);
            renderItems();
            form.reset();
        }
    });
    renderItems();
});