"use strict"

const students = [
    {
        name: 'Farkas Ferenc',
        groups: ['11.a', '11_SZFT2', '11_MA1', '11_AN3'],
        subjects: [
            {
                name: 'Webprogramozás',
                average: 4.78
            },
            {
                name: 'Asztali alkalmazások',
                average: 4.45
            },
            {
                name: 'Adatbázis',
                average: 5.00
            },
        ]
    },
    {
        name: 'Kiss Klára',
        groups: ['12.d', '12_SZFT5', '12_ANG4'],
        subjects: [
            {
                name: 'Webprogramozás',
                average: 4.25
            },
            {
                name: 'Backend programozás',
                average: 4.80
            },
            {
                name: 'Szoftvertesztelés',
                average: 4.60
            },
        ]
    },
    {
        name: 'Szabó István',
        groups: ['10.a', '10_PROG3', '10_ANG2'],
        subjects: [
            {
                name: 'Programozás alapok',
                average: 3.90
            },
            {
                name: 'Informatika alapok',
                average: 4.10
            },
            {
                name: 'Angol nyelv',
                average: 3.70
            },
        ]
    },
    {
        name: 'Nagy Emese',
        groups: ['13.b', '13_ANG5'],
        subjects: [
            {
                name: 'Frontend programozás',
                average: 4.85
            },
            {
                name: 'Backend programozás',
                average: 4.95
            },
            {
                name: 'Asztali alkalmazások',
                average: 4.70
            },
        ]
    },
    {
        name: 'Horváth Bence',
        groups: ['9.b', '9_PROG1', '9_ANG1'],
        subjects: [
            {
                name: 'Programozás alapok',
                average: 4.50
            },
            {
                name: 'Informatika alapok',
                average: 4.40
            },
            {
                name: 'Angol nyelv',
                average: 4.80
            },
        ]
    },
    {
        name: 'Tóth Anna',
        groups: ['11_SZFT4', '11.b', '11_ANG3'],
        subjects: [
            {
                name: 'Webprogramozás',
                average: 4.10
            },
            {
                name: 'Docker',
                average: 3.90
            },
            {
                name: 'Adatbázis',
                average: 4.00
            },
        ]
    },
    {
        name: 'Molnár László',
        groups: ['9.d', '9_PROG2', '9_ANG1'],
        subjects: [
            {
                name: 'Programozás alapok',
                average: 4.60
            },
            {
                name: 'Informatika alapok',
                average: 4.30
            },
            {
                name: 'Matematika',
                average: 4.50
            },
        ]
    },
    {
        name: 'Balogh Erika',
        groups: ['10_PROG1', '10.c', '10_ANG1'],
        subjects: [
            {
                name: 'Programozás alapok',
                average: 3.85
            },
            {
                name: 'Informatika alapok',
                average: 3.70
            },
            {
                name: 'Angol nyelv',
                average: 4.00
            },
        ]
    },
    {
        name: 'Varga Dávid',
        groups: ['12.a', '12_SZFT3', '12_ANG2'],
        subjects: [
            {
                name: 'Backend programozás',
                average: 4.90
            },
            {
                name: 'Szoftvertesztelés',
                average: 4.75
            },
            {
                name: 'Webprogramozás',
                average: 4.85
            },
        ]
    },
    {
        name: 'Juhász Katalin',
        groups: ['13.a', '13_SZFT6', '13_ANG4'],
        subjects: [
            {
                name: 'Frontend programozás',
                average: 4.70
            },
            {
                name: 'Backend programozás',
                average: 4.95
            },
            {
                name: 'Matematika',
                average: 4.60
            },
        ]
    },
];

const kontener = document.getElementById("container");

for (let i = 0; i < students.length; i++) {
    let minden = document.createElement("div")
    minden.classList.add('p-2')
    let tnev = document.createElement("h5")
    tnev.classList.add('font-semibold', 'text-2xl')
    tnev.textContent = students[i].name
    minden.append(tnev)
    let tcsoport = document.createElement("div")
    tcsoport.classList.add('flex', 'flex-wrap', 'gap-2')

    for (let j = 0; j < students[i].groups.length; j++) {
        let csoportok = document.createElement("span")
        csoportok.classList.add('p-1', 'bg-sky-500', 'text-white', 'rounded-md')
        csoportok.textContent = students[i].groups[j]
        tcsoport.append(csoportok)
    }

    let tablazat = document.createElement("table")
    tablazat.classList.add('w-full', 'mt-1')
    let tablaFej = document.createElement("thead")
    tablaFej.classList.add('bg-sky-500', 'text-white')
    let tablaFejlecSor = document.createElement("tr")
    let tablaCim1 = document.createElement("th")
    tablaCim1.classList.add('p-1')
    let tablaCim2 = document.createElement("th")
    tablaCim2.classList.add('p-1')
    tablaCim1.textContent = "Tantárgy"
    tablaCim2.textContent = "Átlag"
    tablaFejlecSor.append(tablaCim1, tablaCim2)
    tablaFej.append(tablaFejlecSor)
    tablazat.append(tablaFej)
    
    let tablaTest = document.createElement("tbody")
    let avg = 0
    for (let h = 0; h < students[i].subjects.length; h++) {
        let tantargySor = document.createElement("tr")
        tantargySor.classList.add('odd:bg-sky-200')
        let tantagySzoveg1 = document.createElement("td")
        tantagySzoveg1.textContent = students[i].subjects[h].name
        tantagySzoveg1.classList.add('p-1')
        let tantargySzoveg2 = document.createElement("td")
        tantargySzoveg2.textContent = students[i].subjects[h].average
        tantargySzoveg2.classList.add('p-1', 'text-center')
        tantargySor.append(tantagySzoveg1, tantargySzoveg2)
        tablaTest.append(tantargySor)
        avg += students[i].subjects[h].average
    }
    
    avg = avg / students[i].subjects.length
    tablazat.append(tablaTest)

    let atlagSzamitas = document.createElement("div")
    atlagSzamitas.style = 'text-weight: bold'
    atlagSzamitas.textContent = "Átlag: "
    let szamitas = document.createElement("span")
    szamitas.classList.add('font-bold')
    szamitas.textContent = avg.toFixed(2)
    atlagSzamitas.append(szamitas)

    minden.append(tnev, tcsoport, tablazat, atlagSzamitas)
    kontener.append(minden)
}