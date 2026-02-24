import "@assets/app.css";
import { findCarByPlate } from "./data/cars.js";
import { findPersonById } from "./data/persons.js";

const main = document.getElementById("main");
const errorElement = document.getElementById("error");
const resetButton = document.getElementById("reset");
const searchInput = document.getElementById("search");

function createStatCard({ label, value, valueClass = "" }) {
  const wrapper = document.createElement("div");
  wrapper.className = "rounded-xl bg-fuchsia-50 p-3";

  const dt = document.createElement("dt");
  dt.className =
    "text-xs font-semibold uppercase tracking-wide text-fuchsia-500";
  dt.textContent = label;

  const dd = document.createElement("dd");
  dd.className = `mt-1 font-medium text-fuchsia-900 ${valueClass}`.trim();
  dd.textContent = value;

  wrapper.append(dt, dd);

  return wrapper;
}

function createCarElement(car) {
  const article = document.createElement("article");
  article.className =
    "rounded-2xl bg-white p-5 shadow-sm ring-1 ring-fuchsia-200";

  const h2 = document.createElement("h2");
  h2.className = "car-title text-base font-semibold text-fuchsia-900";
  h2.textContent = `${car.brand} ${car.model}`;

  const dl = document.createElement("dl");
  dl.className = "info mt-4 grid grid-cols-2 gap-3 text-sm";

  const yearCard = createStatCard({ label: "Year", value: car.year });
  const plateCard = createStatCard({
    label: "Plate",
    value: car.plate,
    valueClass: "font-mono",
  });
  
  const ownerCard = createStatCard({
    label: "Owner",
    value: "Click here to show owner info",
    valueClass: "cursor-pointer underline hover:text-fuchsia-700",
  });
  
  const ownerValueElement = ownerCard.querySelector("dd");

  ownerValueElement.addEventListener("click", async (e) => {
    e.preventDefault();
    if (ownerValueElement.dataset.loaded) return;

    try {
      const person = await findPersonById(car.owner_id);
      
      const ownerNameCard = createStatCard({
        label: "Name",
        value: `${person.first_name} ${person.last_name}`,
      });
      
      const ownerEmailCard = createStatCard({
        label: "E-mail",
        value: person.email_address,
      });

      dl.replaceChild(ownerNameCard, ownerCard);
      dl.appendChild(ownerEmailCard);

    } catch (err) {
      errorElement.querySelector("p").textContent = err.message;
      errorElement.classList.remove("hidden");
    }
  });

  dl.append(yearCard, plateCard, ownerCard);
  article.append(h2, dl);

  return article;
}

searchInput.addEventListener("change", async (e) => {
  const plate = e.target.value;
  main.replaceChildren();
  
  errorElement.classList.add("hidden");

  try {
    const car = await findCarByPlate(plate);
    errorElement.classList.add("hidden");
    const carElement = createCarElement(car);
    main.appendChild(carElement);
  } catch (err) {
    errorElement.querySelector("p").textContent = err.message;
    errorElement.classList.remove("hidden");
  }
});

resetButton.addEventListener("click", () => {
  errorElement.classList.add("hidden");
  searchInput.value = "";
  main.replaceChildren();
});