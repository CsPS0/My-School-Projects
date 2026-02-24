import { getStore } from "./stores.js";

export function openProductDialog(product) {
    const dialog = document.getElementById("product-info");
    
    dialog.querySelector(".image").src = product.image;
    dialog.querySelector(".image").alt = product.name;
    
    dialog.querySelector(".category").textContent = product.category;
    dialog.querySelector(".brand").textContent = product.brand;
    dialog.querySelector(".name").textContent = product.name;
    dialog.querySelector(".volume").textContent = product.volume;
    
    dialog.querySelector(".price").textContent = product.price.toLocaleString("de-DE", { style: "currency", currency: "EUR" });
    
    const baseUnit = dialog.querySelector(".base-unit");
    baseUnit.style.display = "block"; 
    baseUnit.innerHTML = '<span class="unit"></span> <span class="price"></span>'; 

    if (product.baseUnitPrice && product.baseUnitPrice.price !== null) {
        baseUnit.querySelector(".unit").textContent = product.baseUnitPrice.unit;
        baseUnit.querySelector(".price").textContent = product.baseUnitPrice.price.toLocaleString("de-DE", { style: "currency", currency: "EUR" });
    } else {
        baseUnit.style.display = "none";
    }
    
    const storeList = dialog.querySelector("#available-stores");
    storeList.innerHTML = ""; 
    
    if (product.availableStores) {
        product.availableStores.forEach(storeRef => {
            const li = document.createElement("li");
            li.textContent = storeRef.name; 
            li.classList.add("cursor-pointer");
            
            li.addEventListener("click", () => {
                getStore(storeRef.id).then(storeData => {
                    openStoreDialog(storeData);
                });
            });
            
            storeList.appendChild(li);
        });
    }
    
    dialog.showModal();
}

export function openStoreDialog(store) {
    const dialog = document.getElementById("store-info");
    
    dialog.querySelector(".type").textContent = store.type;
    dialog.querySelector(".address").textContent = store.address;
    
    dialog.showModal();
}
