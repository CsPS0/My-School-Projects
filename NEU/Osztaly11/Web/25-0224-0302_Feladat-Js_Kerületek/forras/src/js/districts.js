"use strict"

function getSelectedDistrict() {
    const index = document.querySelector('#selected-district').value;
    return index === "" ? null : Number(index);
}

const districts = [
    { id: 1, name: "I. kerület - Budavár", description: "A Budai Vár és környéke, történelmi nevezetességek otthona.", image: "district_1.png" },
    { id: 2, name: "II. kerület", description: "Buda elegáns zöldövezeti része, villákkal és természetközeli helyekkel.", image: "district_2.png" },
    { id: 3, name: "III. kerület - Óbuda-Békásmegyer", description: "Ókori római emlékek és a Duna-parti hangulat jellemzi.", image: "district_3.png" },
    { id: 4, name: "IV. kerület - Újpest", description: "Észak-Pest egyik legnagyobb lakóövezete, ipari és sportközponttal.", image: "district_4.png" },
    { id: 5, name: "V. kerület - Belváros-Lipótváros", description: "Budapest szíve, a Parlament és a legfontosabb hivatalok helyszíne.", image: "district_5.png" },
    { id: 6, name: "VI. kerület - Terézváros", description: "A kulturális és éjszakai élet központja, az Andrássy út otthona.", image: "district_6.png" },
    { id: 7, name: "VII. kerület - Erzsébetváros", description: "A híres zsidó negyed, romkocsmákkal és pezsgő élettel.", image: "district_7.png" },
    { id: 8, name: "VIII. kerület - Józsefváros", description: "Régi és új keveredése, egyetemi és kulturális központ.", image: "district_8.png" },
    { id: 9, name: "IX. kerület - Ferencváros", description: "A Duna-part és a felújított városrészek új élettel teli környékei.", image: "district_9.png" },
    { id: 10, name: "X. kerület - Kőbánya", description: "Ipari és borászati múltú kerület, nagy parkokkal és sörgyárral.", image: "district_10.png" },
    { id: 11, name: "XI. kerület - Újbuda", description: "A Gellért-hegytől a panelnegyedekig terjedő sokszínű városrész.", image: "district_11.png" },
    { id: 12, name: "XII. kerület - Hegyvidék", description: "Budapest egyik legzöldebb kerülete, a Normafa és a János-hegy otthona.", image: "district_12.png" },
    { id: 13, name: "XIII. kerület", description: "Modern üzleti negyed és lakóövezet a Duna-part közelében.", image: "district_13.png" },
    { id: 14, name: "XIV. kerület - Zugló", description: "A Városliget és a sportélet központja, családbarát lakóövezet.", image: "district_14.png" },
    { id: 15, name: "XV. kerület", description: "Kertvárosi részek és ipari övezetek keveredése Észak-Pesten.", image: "district_15.png" },
    { id: 16, name: "XVI. kerület", description: "Kertvárosi jellege miatt közkedvelt a családosok körében.", image: "district_16.png" },
    { id: 17, name: "XVII. kerület - Rákosmente", description: "A főváros egyik legnagyobb kertvárosi kerülete.", image: "district_17.png" },
    { id: 18, name: "XVIII. kerület - Pestszentlőrinc-Pestszentimre", description: "Zöldövezeti kerület a repülőtér közelében.", image: "district_18.png" },
    { id: 19, name: "XIX. kerület - Kispest", description: "Hagyományos munkáskerület, mára modernizálódott városrész.", image: "district_19.png" },
    { id: 20, name: "XX. kerület - Pesterzsébet", description: "Régi kertvárosi hangulat és lakótelepek egyaránt jellemzik.", image: "district_20.png" },
    { id: 21, name: "XXI. kerület - Csepel", description: "A Csepel-szigeten fekvő ipari és lakóövezeti kerület.", image: "district_21.png" },
    { id: 22, name: "XXII. kerület - Budafok-Tétény", description: "A borászatáról híres dél-budai kerület.", image: "district_22.png" },
    { id: 23, name: "XXIII. kerület - Soroksár", description: "Kisvárosi hangulatú, csendes kertvárosi környék.", image: "district_23.png" }
];


