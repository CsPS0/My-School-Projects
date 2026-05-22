import "@assets/app.css";
import { formatPrice } from "./js/helper.js";
import { manufacturers, getManufacturers } from "./js/manufacturer.js";
import {
  toothbrushes,
  technologies,
  getToothbrushes,
  getStocks,
  deleteToothbrush,
  createToothbrush,
  updateToothbrush,
} from "./js/toothbrush.js";

const productsContainer = document.getElementById("products");
const toothbrushCardTemplate = document.getElementById("toothbrush-card");
const stockTableRowTemplate = document.getElementById("stock-table-row");
const stockDialog = document.getElementById("stock-dialog");
const toothbrushDialog = document.getElementById("toothbrush-dialog");
const deleteDialog = document.getElementById("delete-dialog");
const toothbrushForm = toothbrushDialog.querySelector("form");
const manufacturerSelect = document.getElementById("manufacturer_id");
const technologySelect = document.getElementById("technology");

function createCard(toothbrush) {
  const clone = toothbrushCardTemplate.content.cloneNode(true);
  const manufacturer = manufacturers.find((m) => m.id == toothbrush.manufacturer_id);

  clone.querySelector("h2").textContent = `${manufacturer ? manufacturer.name : "Unknown"} ${toothbrush.model}`;
  clone.querySelector(".technology").textContent = `${toothbrush.technology} toothbrush`;
  clone.querySelector(".rating span").textContent = toothbrush.rating;
  clone.querySelector(".price span:last-child").textContent = formatPrice(toothbrush.price_huf);
  clone.querySelector(".battery span:last-child").textContent = `${toothbrush.battery_life_days} days`;
  clone.querySelector(".modes span:last-child").textContent = toothbrush.mode_count;
  clone.querySelector(".color span:last-child").textContent = toothbrush.color;
  clone.querySelector(".waterproof span:last-child").textContent = toothbrush.waterproof_rating;
  clone.querySelector(".weight span:last-child").textContent = `${toothbrush.weight_grams} g`;

  const stockBtn = clone.querySelector(".stock-button");
  stockBtn.addEventListener("click", async () => {
    const stocks = await getStocks(toothbrush.id);
    const tbody = stockDialog.querySelector("tbody");
    tbody.innerHTML = "";
    stocks.forEach((stock) => {
      const rowClone = stockTableRowTemplate.content.cloneNode(true);
      rowClone.querySelector(".name").textContent = stock.shop_name;
      rowClone.querySelector(".address").textContent = stock.address;
      const badge = rowClone.querySelector(".qty span");
      badge.textContent = stock.stock_quantity;

      if (stock.stock_quantity >= 10) {
        badge.classList.add("bg-green-100", "text-green-700");
      } else if (stock.stock_quantity > 3) {
        badge.classList.add("bg-orange-100", "text-orange-700");
      } else {
        badge.classList.add("bg-red-100", "text-red-700");
      }
      tbody.appendChild(rowClone);
    });
    stockDialog.showModal();
  });

  const editBtn = clone.querySelector(".edit-button");
  editBtn.addEventListener("click", () => {
    toothbrushDialog.querySelector("h2").textContent = "Edit Toothbrush";
    toothbrushDialog.dataset.id = toothbrush.id;
    toothbrushForm.model.value = toothbrush.model;
    toothbrushForm.manufacturer_id.value = toothbrush.manufacturer_id;
    toothbrushForm.price_huf.value = toothbrush.price_huf;
    toothbrushForm.technology.value = toothbrush.technology;
    toothbrushForm.battery_life_days.value = toothbrush.battery_life_days;
    toothbrushForm.mode_count.value = toothbrush.mode_count;
    toothbrushForm.weight_grams.value = toothbrush.weight_grams;
    toothbrushForm.color.value = toothbrush.color;
    toothbrushForm.waterproof_rating.value = toothbrush.waterproof_rating;
    toothbrushDialog.showModal();
  });

  const deleteBtn = clone.querySelector(".delete-button");
  deleteBtn.addEventListener("click", () => {
    deleteDialog.dataset.id = toothbrush.id;
    deleteDialog.showModal();
  });

  return clone;
}

function displayCards(toothbrushList) {
  productsContainer.innerHTML = "";
  toothbrushList.forEach((t) => {
    productsContainer.appendChild(createCard(t));
  });
}

stockDialog.querySelector("button").addEventListener("click", (e) => {
  e.preventDefault();
  stockDialog.close();
});

document.getElementById("create-toothbrush-button").addEventListener("click", () => {
  toothbrushDialog.querySelector("h2").textContent = "Create Toothbrush";
  delete toothbrushDialog.dataset.id;
  toothbrushForm.reset();
  toothbrushDialog.showModal();
});

toothbrushDialog.querySelector(".cancel").addEventListener("click", () => {
  toothbrushDialog.close();
});

document.getElementById("cancel-delete").addEventListener("click", () => {
  deleteDialog.close();
});

document.getElementById("confirm-delete").addEventListener("click", async () => {
  const id = deleteDialog.dataset.id;
  if (id) {
    await deleteToothbrush(id);
    displayCards(toothbrushes);
  }
  deleteDialog.close();
});

toothbrushForm.addEventListener("submit", async (e) => {
  e.preventDefault();
  const formData = new FormData(toothbrushForm);
  const data = Object.fromEntries(formData.entries());

  data.manufacturer_id = parseInt(data.manufacturer_id);
  data.price_huf = parseInt(data.price_huf);
  data.battery_life_days = parseInt(data.battery_life_days);
  data.mode_count = parseInt(data.mode_count);
  data.weight_grams = parseInt(data.weight_grams);

  if (toothbrushDialog.dataset.id) {
    const id = toothbrushDialog.dataset.id;
    const existing = toothbrushes.find((t) => t.id == id);
    const updatedData = { ...existing, ...data };
    await updateToothbrush(id, updatedData);
  } else {
    data.rating = 0;
    data.in_stock = true;
    await createToothbrush(data);
  }

  displayCards(toothbrushes);
  toothbrushForm.reset();
  toothbrushDialog.close();
});

async function init() {
  await getManufacturers();
  manufacturers.forEach((m) => {
    const option = document.createElement("option");
    option.value = m.id;
    option.textContent = m.name;
    manufacturerSelect.appendChild(option);
  });

  technologies.forEach((tech) => {
    const option = document.createElement("option");
    option.value = tech;
    option.textContent = tech;
    technologySelect.appendChild(option);
  });

  await getToothbrushes();
  displayCards(toothbrushes);
}

init();
