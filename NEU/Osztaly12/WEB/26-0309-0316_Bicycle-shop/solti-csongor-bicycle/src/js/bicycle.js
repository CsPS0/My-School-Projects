import { BASE_URL } from './config.js';

export async function getBicycles() {
  const response = await fetch(BASE_URL);
  if (!response.ok) {
    throw new Error('Hiba a kerékpárok lekérésekor!');
  }
  return await response.json();
}

export async function createBicycle(bicycle) {
  const response = await fetch(BASE_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(bicycle)
  });
  if (!response.ok) {
    throw new Error('Hiba a kerékpár létrehozásakor!');
  }
  return await response.json();
}

export async function updateBicycle(bicycle) {
  const response = await fetch(`${BASE_URL}/${bicycle.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(bicycle)
  });
  if (!response.ok) {
    throw new Error('Hiba a kerékpár frissítésekor!');
  }
  return await response.json();
}

export async function deleteBicycle(id) {
  const response = await fetch(`${BASE_URL}/${id}`, {
    method: 'DELETE'
  });
  if (!response.ok) {
    throw new Error('Hiba a kerékpár törlésekor!');
  }
  return await response.json();
}
