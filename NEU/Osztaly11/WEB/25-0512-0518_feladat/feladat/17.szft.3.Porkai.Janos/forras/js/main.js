"use strict"

const budafokiUt = [
    3450000, 2890000, 3100000, 2750000, 3200000, 2950000, 3400000, 3600000, 2800000, 3000000,
    3150000, 2900000, 3050000, 3250000, 3100000, 3350000, 3450000, 2950000, 3100000, 3300000,
    2850000, 3200000, 3000000, 3100000, 3350000, 2900000, 3050000, 3150000, 3400000, 3000000
];

const kerepesiUt = [
    3400000, 2950000, 3100000, 3250000, 3000000, 3150000, 3300000, 2850000, 3200000, 3050000,
    3100000, 3350000, 2900000, 3450000, 3000000, 3100000, 2950000, 3200000, 3400000, 2800000,
    3000000, 3150000, 2900000, 3050000, 3250000, 3100000, 3350000, 3450000, 2950000, 3100000
];

const thokolyUt = [
    3200000, 3000000, 3100000, 3350000, 2900000, 3050000, 3250000, 3100000, 3350000, 2950000,
    3400000, 2800000, 3000000, 3150000, 2800000, 3050000, 3250000, 3100000, 3350000, 3450000,
    2950000, 3100000, 3400000, 3000000, 3100000, 3350000, 3500000, 3050000, 3250000, 3150000
];

const becsiUt = [
    3400000, 2900000, 3000000, 3340000, 2800000, 3070000, 3200000, 3000000, 3400000, 2980000,
    3300000, 2900000, 3100000, 3100000, 2900000, 3200000, 3200000, 2800000, 3250000, 3550000,
    2850000, 3000000, 3350000, 2800000, 3000000, 3300000, 3300000, 2850000, 3300000, 2800000
];

function sum(tomb){
    let osszeg = 0
    for(let i = 0; i < tomb.length; i++){
        osszeg += tomb[i]
    }
    return osszeg
}

function updateCard(day, store){

    const details = document.getElementById("details")
    details.replaceChildren()

    const card = document.getElementById("card")
    card.classList.remove("hidden")
    card.style.marginInline = "auto"

    const img = card.querySelector("img")
    img.src = "images/" + store + ".jpg"

    let tomb = []
    let boltneve = ""
    if(store == "becsi"){
        tomb = becsiUt
        boltneve = "Bécsi Út"
    }
    else if (store == "budafoki")
    {
        tomb = budafokiUt
        boltneve = "Budafoki út"
    }
    else if (store == "kerepesi")
    {
        tomb = kerepesiUt
        boltneve = "Kerepesi út"
    }
    else if (store == "thokoly")
    {
        tomb = thokolyUt
        boltneve = "Thököly út"
    }

    const h3 = document.createElement("h3")
    h3.textContent = boltneve
    h3.style.marginBottom = "0.5rem"

    const p1 = document.createElement("p")
    p1.textContent = `Az áruház bevétele a(z) ${day}. napon: ${tomb[day-1]} forint.`
    const p2 = document.createElement("p")
    p2.textContent = `Az áruház bevétele az egész hónapban: ${sum(tomb)} forint.`

    details.append(h3, p1, p2)
}

document.querySelector("form").addEventListener("submit", function(event){
    event.preventDefault()
    const day = document.querySelector("#day").value
    const store = document.querySelector("#store").value
    updateCard(day, store)
})