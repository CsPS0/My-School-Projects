"use strict"

function createListItem(text) {
    const li = document.createElement('li');
    li.textContent = text;
    return li;
}

function createSpan(text) {
    const span = document.createElement('span');
    span.textContent = text;
    return span;
}

function getLineArray(line) {
    switch (line) {
        case '1':
            return line1;
        case '2':
            return line2;
        case 'D':
            return lineD;
        case '71':
            return line71;
        default:
            return [];
    }
}

function addNewData(line, name, boarded, disembarked) {
    const lineArray = getLineArray(line);
    lineArray.push([name, boarded, disembarked]);
}

function passengerStats() {
    const lines = { '1': line1, '2': line2, 'D': lineD, '71': line71 };

    for (const lineId in lines) {
        const lineArray = lines[lineId];
        let totalPassengers = 0;
        let totalBoarded = 0;
        let stopCount = 0;

        lineArray.forEach(stop => {
            totalPassengers += stop[1] + stop[2];
            totalBoarded += stop[1];
            stopCount++;
        });

        const averageBoarded = stopCount > 0 ? (totalBoarded / stopCount).toFixed(2) : 0;

        const sumElement = document.querySelector(`#sum-${lineId}`);
        if (sumElement) {
            sumElement.innerHTML = `Összesen: ${totalPassengers}<br>Átlagos utascsere: ${averageBoarded}`;
        }
    }
}
passengerStats();

const filterResults = document.querySelector('#filter-results');
function filterStops(line, value) {
    const lineArray = getLineArray(line);
    filterResults.innerHTML = '';

    const filtered = lineArray.filter(stop => stop[1] === value || stop[2] === value);

    filtered.forEach(stop => {
        const listItem = createListItem(`${stop[0]} - felszállók: ${stop[1]}, leszállók: ${stop[2]}`);
        filterResults.appendChild(listItem);
    });
}

const popularResults = document.querySelector('#popular-results');
function popularStops() {
    popularResults.innerHTML = '';
    const allStops = [...line1, ...line2, ...lineD, ...line71];

    allStops.sort((a, b) => b[1] - a[1]);

    const top3Stops = allStops.slice(0, 3);

    top3Stops.forEach(stop => {
        const listItem = createListItem(`${stop[0]} (${stop[1]} felszálló)`);
        popularResults.appendChild(listItem);
    });
}
popularStops();

const findResults = document.querySelector('#find-results');
function findStop(name) {
    findResults.innerHTML = '';
    const lines = { '1': line1, '2': line2, 'D': lineD, '71': line71 };
    const foundLines = [];

    for (const lineId in lines) {
        const lineArray = lines[lineId];
        const stopFound = lineArray.some(stop => stop[0].toLowerCase() === name.toLowerCase());
        if (stopFound) {
            foundLines.push(lineId);
        }
    }

    if (foundLines.length > 0) {
        foundLines.forEach(lineId => {
            const listItem = createListItem(lineId);
            findResults.appendChild(listItem);
        });
    } else {
        const listItem = createListItem('Nincs ilyen megálló.');
        findResults.appendChild(listItem);
    }
}

document.querySelector('#add-item').addEventListener('submit', event => {
    event.preventDefault();
    addNewData(
        document.querySelector('#line').value,
        document.querySelector('#name').value,
        +document.querySelector('#boarded').value,
        +document.querySelector('#disembarked').value,
    );
    passengerStats();
    popularStops();
})

document.querySelector('#filter-item').addEventListener('submit', event => {
    event.preventDefault();
    filterStops(
        document.querySelector('#filter-line').value,
        +document.querySelector('#filter-value').value,
    )
})

document.querySelector('#find-stop').addEventListener('submit', event => {
    event.preventDefault();
    findStop(
        document.querySelector('#stop-name').value,
    )
})
