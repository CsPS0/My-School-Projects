import "./assets/app.css";
import { allPackages } from "./data.js";
import { createCard } from "./ui/cards.js";

const coursesContainer = document.querySelector("#courses");
const billingToggle = document.querySelector("#billingToggle");

coursesContainer.className = "grid gap-8 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 p-4 m-4";

function renderCards() {
  coursesContainer.innerHTML = "";
  const isAnnual = billingToggle.value === "annual";
  const packages = allPackages();

  packages.forEach((pkg) => {
    const card = createCard(pkg, isAnnual);
    coursesContainer.appendChild(card);
  });
}

renderCards();

billingToggle.addEventListener("change", renderCards);