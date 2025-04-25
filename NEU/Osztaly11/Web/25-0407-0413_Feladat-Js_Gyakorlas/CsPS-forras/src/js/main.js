const form = document.getElementById('slangForm');
const wordInput = document.getElementById('word');
const definitionInput = document.getElementById('definition');
const exampleInput = document.getElementById('example');
const yearInput = document.getElementById('year');
const tableBody = document.querySelector('table tbody');

form.addEventListener('submit', function (e) {
    e.preventDefault();

    const newSlang = {
        word: wordInput.value,
        definition: definitionInput.value,
        example: exampleInput.value || null,
        year: new Number(yearInput.value).getFullYear(),
    };

    slangs.push(newSlang);
    updateTable();
    form.reset();
});

function updateTable() {
    tableBody.textContent = '';
    slangs.forEach(slang => {
        const row = document.createElement('tr');
        
        
        const wordCell = document.createElement('td');
        wordCell.textContent = slang.word;
        row.appendChild(wordCell);

        const definitionCell = document.createElement('td');
        definitionCell.textContent = slang.definition;
        if (slang.example) {
            definitionCell.title = slang.example;
        }
        row.appendChild(definitionCell);

        const yearCell = document.createElement('td');
        const yearSpan = document.createElement('span');
        yearSpan.classList.add('tag');
        yearSpan.textContent = slang.year;
        yearCell.appendChild(yearSpan);
        row.appendChild(yearCell);

        tableBody.appendChild(row);
    });
}

updateTable();