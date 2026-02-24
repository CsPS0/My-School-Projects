import { getProducts } from "./js/products.js";
import { generateProductCard } from "./js/cards.js";
import "./assets/style.css";

function generateProductCards(products) {
    const grid = document.getElementById("products-grid");
    grid.innerHTML = "";
    products.forEach(product => {
        const card = generateProductCard(product);
        grid.appendChild(card);
    });
}

getProducts().then(products => {
    generateProductCards(products);
}).catch(error => {
    console.error("Error loading products:", error);
});
