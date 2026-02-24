export const BASE_URL = "http://localhost:3000";

export function getStore(id) {
    return fetch(`${BASE_URL}/stores/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Failed to fetch store");
            }
            return response.json();
        });
}
