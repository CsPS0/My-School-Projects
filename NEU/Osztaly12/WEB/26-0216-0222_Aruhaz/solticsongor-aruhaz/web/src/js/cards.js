import { openProductDialog } from "./dialogs.js";
import { getProduct } from "./products.js";

export function generateProductCard(product) {
    const template = document.getElementById("product-card");
    const clone = template.content.cloneNode(true);
    
    const img = clone.querySelector(".image");
    img.src = product.image;
    img.alt = product.name;
    
    clone.querySelector(".name").textContent = product.name;
    clone.querySelector(".volume").textContent = product.volume;
    
    const priceFormatted = product.price.toLocaleString("de-DE", { style: "currency", currency: "EUR" });
    clone.querySelector(".price").textContent = priceFormatted;
    
    const baseUnit = clone.querySelector(".base-unit");
    if (product.baseUnitPrice && product.baseUnitPrice.price !== null) {
        baseUnit.querySelector(".unit").textContent = product.baseUnitPrice.unit;
        baseUnit.querySelector(".price").textContent = product.baseUnitPrice.price.toLocaleString("de-DE", { style: "currency", currency: "EUR" });
    } else {
        baseUnit.style.display = "none";
    }
    
    const button = clone.querySelector("button");
    button.addEventListener("click", () => {
        getProduct(product.id).then(freshProduct => {
            openProductDialog(freshProduct);
        });
    });

    return clone;
}
