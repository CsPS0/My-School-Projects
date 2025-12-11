"use strict"

const userProfile = {
    name: "Breen Freeman Csongor Nihilanth",
    position: "Combine Soldier - 7 Hour War Veteran",
    description: "After Gordon Freeman's Revolution, one escaped Combine Soldier, who got invited by GLaDOS."
};

document.querySelector('#profile--name').textContent = userProfile.name;
document.querySelector('#profile--position').textContent = userProfile.position;
document.querySelector('#profile--description').textContent = userProfile.description;

function capitalize(text) {
    return text.toUpperCase().slice(0, 1) + text.substring(1);
}

function createTableRow(subject) {
    const row = document.createElement('tr');
    row.dataset.id = subject.id;
    
    const id = document.createElement('td');
    id.textContent = subject.id;

    const name = document.createElement('td');
    name.textContent = subject.name;

    const statusWraper = document.createElement('td')
    const status = document.createElement('span');
    status.textContent = capitalize(subject.status);
    status.classList.add(subject.status);
    
    statusWraper.append(status);

    row.append(id, name, statusWraper);
    return row;
}

function fillTable(array) {
    const rows = [];
    for (const subject of array) {
        rows.push(createTableRow(subject));
    }
    table.replaceChildren(...rows);
    addEventListenersToRows()
}

const table = document.querySelector('#list-of-subjects tbody');
fillTable(subjects);

function showSubject(subject) {
    for (const key in subject) {
        const element = document.querySelector(`#current-subject--${key}`);
        if (!element) continue;

        if (key === 'status') {
            element.textContent = capitalize(subject[key]);
            element.classList.remove('alive', 'terminated');
            element.classList.add(subject[key]);
        } else if (key === 'traits') {
            const traits = [];
            for (const trait of subject[key]) {
                const span = document.createElement('span');
                span.textContent = trait;
                traits.push(span);
            }
            element.replaceChildren(...traits);
        } else {
            element.textContent = subject[key];
        }
    }
}

function addEventListenersToRows() {
    table.querySelectorAll('tr').forEach(x => x.addEventListener('click', e => 
        showSubject(subjects.find(y => y.id == x.dataset.id))
    ));
}

function filterSubjects(name) {
    return subjects.filter(x => x.name.toLowerCase().includes(name.toLowerCase()));
}

document.querySelector('#search').addEventListener('submit', e => {
    e.preventDefault();
    fillTable(filterSubjects(document.querySelector('#name').value));
});