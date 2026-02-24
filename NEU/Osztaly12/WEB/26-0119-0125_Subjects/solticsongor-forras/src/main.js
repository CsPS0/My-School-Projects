"use strict"

import "@assets/app.css";
import subjects from "./subjects.js";

const header = document.createElement("header");
header.style.backgroundColor = "#931634";

const title = document.createElement("h1");
title.textContent = "Felvett tárgyak listája";
title.className = "text-white text-6xl font-thin py-16 pl-8";

header.appendChild(title);
document.body.prepend(header);

let sortDirectionName = "asc";
let sortDirectionCredit = "asc";

const tbody = document.getElementById("subject-table-body");

function renderTable() {
  tbody.textContent = "";

  subjects.forEach((subject, index) => {
    const row = document.createElement("tr");
    
    let borderClass = "border-b-2 border-[#931634]";
    if (index === subjects.length - 1) {
      borderClass = "";
    }
    
    row.className = `${borderClass} hover:bg-black/10 transition-colors`;

    const cellCode = document.createElement("td");
    cellCode.textContent = subject.code;
    cellCode.className = "py-4 px-2";

    const cellName = document.createElement("td");
    cellName.textContent = subject.subject;
    cellName.className = "py-4 px-2 font-semibold";

    const cellCredit = document.createElement("td");
    cellCredit.textContent = subject.credit;
    cellCredit.className = "py-4 px-2";

    row.appendChild(cellCode);
    row.appendChild(cellName);
    row.appendChild(cellCredit);
    tbody.appendChild(row);
  });
}

renderTable();

const headerName = document.getElementById("header-name");
const headerCredit = document.getElementById("header-credit");

headerName.addEventListener("click", () => {
  if (sortDirectionName === "asc") {
    subjects.sort((a, b) => a.subject.localeCompare(b.subject));
    sortDirectionName = "desc";
  } else {
    subjects.sort((a, b) => b.subject.localeCompare(a.subject));
    sortDirectionName = "asc";
  }
  renderTable();
});

headerCredit.addEventListener("click", () => {
  if (sortDirectionCredit === "asc") {
    subjects.sort((a, b) => Number(a.credit) - Number(b.credit));
    sortDirectionCredit = "desc";
  } else {
    subjects.sort((a, b) => Number(b.credit) - Number(a.credit));
    sortDirectionCredit = "asc";
  }
  renderTable();
});