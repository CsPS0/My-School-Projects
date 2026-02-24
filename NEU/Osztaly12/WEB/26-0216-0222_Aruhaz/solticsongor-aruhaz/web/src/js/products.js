export const BASE_URL = "http://localhost:3000";

export function getProducts() {
    return fetch(`${BASE_URL}/products`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to fetch products");
            }
            return response.json();
        });
}

export function getProduct(id) {
    return fetch(`${BASE_URL}/products/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to fetch product");
            }
            return response.json();
        });
}
