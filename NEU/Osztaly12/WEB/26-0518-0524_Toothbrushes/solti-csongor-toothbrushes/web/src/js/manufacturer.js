import { BASE_URL } from "./config.js";

export let manufacturers = [];

export async function getManufacturers() {
  const url = `${BASE_URL}/manufacturers`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`A szerver ${response.status} kóddal válaszolt a(z) ${url} útvonalon`);
    }
    const result = await response.json();
    manufacturers = result.data;
    return manufacturers;
  } catch (error) {
    throw error;
  }
}
