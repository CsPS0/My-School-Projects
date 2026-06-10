import { getDogs, createDog, dogbreeds } from './dogs.js';

const dogsContainer = document.getElementById('dogs-container');
const dogCardTemplate = document.getElementById('dog-card');
const breedSelect = document.getElementById('breed');
const dogDialog = document.getElementById('dog-dialog');
const dogForm = document.getElementById('dog-form');
const createDogButton = document.getElementById('create-dog-button');

function createCard(dog) {
    const clone = dogCardTemplate.content.cloneNode(true);
    clone.querySelector('.name').textContent = dog.name;
    clone.querySelector('.breed').textContent = dog.breed;
    clone.querySelector('.age').textContent = dog.age;
    clone.querySelector('.owner').textContent = dog.owner;
    return clone;
}

function displayCards(dogs) {
    dogsContainer.innerHTML = '';
    dogs.forEach(dog => {
        dogsContainer.appendChild(createCard(dog));
    });
}

function populateBreeds() {
    dogbreeds.forEach(breed => {
        const option = document.createElement('option');
        option.value = breed;
        option.textContent = breed;
        breedSelect.appendChild(option);
    });
}

async function init() {
    populateBreeds();
    try {
        const dogs = await getDogs();
        displayCards(dogs);
    } catch (error) {
        alert(error.message);
    }
}

createDogButton.addEventListener('click', () => {
    dogForm.reset();
    dogDialog.showModal();
});

dogForm.addEventListener('submit', async (event) => {
    event.preventDefault();
    const formData = new FormData(dogForm);
    const dog = Object.fromEntries(formData.entries());
    
    dog.age = Number(dog.age);

    try {
        await createDog(dog);
        const dogs = await getDogs();
        displayCards(dogs);
        dogDialog.close();
    } catch (error) {
        alert(error.message);
    }
});

init();
