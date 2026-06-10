import { BASE_URL } from './config.js';

export let dogs = [];

export const dogbreeds = [
    "Tacskó",
    "Németjuhász",
    "Golden retriever",
    "Bulldog",
    "Beagle",
    "Puli"
];

export async function getDogs() {
    const url = `${BASE_URL}dogs`;
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
        }
        dogs = await response.json();
        return dogs;
    } catch (error) {
        console.error(error.message);
        throw error;
    }
}

export async function createDog(dog) {
    const url = `${BASE_URL}dogs`;
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dog)
        });
        if (!response.ok) {
            throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
        }
        const newDog = await response.json();
        dogs.push(newDog);
        return newDog;
    } catch (error) {
        console.error(error.message);
        throw error;
    }
}
