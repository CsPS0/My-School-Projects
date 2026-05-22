import { BASE_URL } from "./config.js";

export let toothbrushes = [];

export const technologies = [
  "Magnetic",
  "Rotating",
  "Rotating-Oscillating",
  "Sonic",
];

export async function getToothbrushes() {
  const url = `${BASE_URL}/toothbrushes`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    const result = await response.json();
    toothbrushes = result.data;
    return toothbrushes;
  } catch (error) {
    throw error;
  }
}

export async function getStocks(brush_id) {
  const url = `${BASE_URL}/toothbrushes/${brush_id}/stocks`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    const result = await response.json();
    return result.data;
  } catch (error) {
    throw error;
  }
}

export async function deleteToothbrush(brush_id) {
  const url = `${BASE_URL}/toothbrushes/${brush_id}`;
  try {
    const response = await fetch(url, {
      method: "DELETE",
    });
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    toothbrushes = toothbrushes.filter((t) => t.id != brush_id);
  } catch (error) {
    throw error;
  }
}

export async function createToothbrush(toothbrush) {
  const url = `${BASE_URL}/toothbrushes/`;
  try {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(toothbrush),
    });
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    const result = await response.json();
    toothbrushes.push(result.data);
    return result.data;
  } catch (error) {
    throw error;
  }
}

export async function updateToothbrush(brush_id, toothbrush) {
  const url = `${BASE_URL}/toothbrushes/${brush_id}`;
  try {
    const response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(toothbrush),
    });
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    const result = await response.json();
    const index = toothbrushes.findIndex((t) => t.id == brush_id);
    if (index !== -1) {
      toothbrushes[index] = result.data;
    }
    return result.data;
  } catch (error) {
    throw error;
  }
}
