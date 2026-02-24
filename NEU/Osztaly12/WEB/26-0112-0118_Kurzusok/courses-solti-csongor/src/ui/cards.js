import { mergeIncludes } from "../data.js";

export function createCard(course, isAnnual) {
  const card = document.createElement("div");
  card.className = "border border-gray-200 rounded-xl shadow-md p-4 bg-white flex flex-col justify-between";

  const title = document.createElement("h2");
  title.className = "text-center text-2xl font-semibold text-gray-900";
  title.textContent = course.name;
  card.appendChild(title);

  const contentWrapper = document.createElement("div");

  const priceBlock = document.createElement("div");
  priceBlock.className = "flex justify-center items-end gap-2 my-4";

  const priceVal = document.createElement("span");
  priceVal.className = "text-4xl font-semibold text-gray-900";

  let finalPrice = course.price;
  if (isAnnual) {
    finalPrice = finalPrice - finalPrice * course.discount_percentage;
  }

  priceVal.textContent = new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
  }).format(finalPrice);

  priceBlock.appendChild(priceVal);

  const perMonth = document.createElement("span");
  perMonth.className = "text-sm text-gray-500";
  perMonth.textContent = "/ month";
  priceBlock.appendChild(perMonth);

  contentWrapper.appendChild(priceBlock);

  const billingInfo = document.createElement("p");
  billingInfo.className = "text-center text-sm text-gray-500 mb-4";
  billingInfo.textContent = isAnnual ? "Billed annually at a discount" : "Billed monthly";
  contentWrapper.appendChild(billingInfo);
  
  card.appendChild(contentWrapper);

  const list = document.createElement("ul");
  list.className = "flex-grow";

  const allIncludedPackages = mergeIncludes(course.id);

  allIncludedPackages.forEach((pkg) => {
    pkg.includes_additional.forEach((item) => {
      const li = document.createElement("li");
      li.className =
        "border-t border-gray-200 py-1 px-2 hover:bg-blue-400 hover:text-white transition-colors duration-150 cursor-default rounded-sm";
      li.textContent = item;
      list.appendChild(li);
    });
  });

  card.appendChild(list);

  return card;
}