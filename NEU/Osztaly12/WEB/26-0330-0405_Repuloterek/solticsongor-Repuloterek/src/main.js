import "@assets/app.css";

const baseTableBody = document.getElementById("baseTableBody");
const baseForm = document.getElementById("baseForm");

async function fetchBases() {
    try {
        const response = await fetch("/api/bases");
        const json = await response.json();
        const bases = json.data || [];

        baseTableBody.innerHTML = "";

        if (bases.length === 0) {
            baseTableBody.innerHTML = `
                <tr>
                    <td colspan="3" class="p-10 text-center text-slate-400">Nincs adat az adatbázisban</td>
                </tr>
            `;
            return;
        }

        bases.forEach(base => {
            const row = document.createElement("tr");
            row.className = "hover:bg-slate-50 transition-colors";

            const cityCell = document.createElement("td");
            cityCell.className = "px-6 py-4";
            
            const cityNameDiv = document.createElement("div");
            cityNameDiv.className = "font-bold text-slate-800";
            cityNameDiv.textContent = base.city;
            
            const idDiv = document.createElement("div");
            idDiv.className = "text-xs text-slate-400 font-mono";
            idDiv.textContent = `UUID: ${base.id}`;
            
            cityCell.appendChild(cityNameDiv);
            cityCell.appendChild(idDiv);

            const icaoCell = document.createElement("td");
            icaoCell.className = "px-6 py-4";
            
            const icaoContainer = document.createElement("div");
            icaoContainer.className = "flex items-center gap-3";
            
            const icaoSpan = document.createElement("span");
            icaoSpan.className = "bg-blue-100 text-blue-700 px-2 py-1 rounded font-mono text-sm font-bold";
            icaoSpan.textContent = base.icao_airport_code;
            
            const runwaySpan = document.createElement("span");
            runwaySpan.className = "text-slate-400 text-sm italic";
            runwaySpan.textContent = base.max_runway_length === "NULL" ? "Nincs adat" : `${base.max_runway_length} m`;
            
            icaoContainer.appendChild(icaoSpan);
            icaoContainer.appendChild(runwaySpan);
            icaoCell.appendChild(icaoContainer);

            const airlineCell = document.createElement("td");
            airlineCell.className = "px-6 py-4 text-slate-600 font-medium";
            airlineCell.textContent = base.airline ? base.airline.name : "Ismeretlen";

            row.appendChild(cityCell);
            row.appendChild(icaoCell);
            row.appendChild(airlineCell);

            baseTableBody.appendChild(row);
        });
    } catch (error) {
        console.error("Error fetching bases:", error);
    }
}

baseForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const formData = new FormData(baseForm);
    
    try {
        const response = await fetch("/api/bases", {
            method: "POST",
            body: formData
        });

        if (response.ok) {
            baseForm.reset();
            fetchBases();
        } else {
            console.error("Failed to save base");
        }
    } catch (error) {
        console.error("Error submitting form:", error);
    }
});

fetchBases();
