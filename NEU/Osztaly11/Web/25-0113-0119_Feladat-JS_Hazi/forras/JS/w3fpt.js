let complexExercisesInitialized = false;

function openPage(pageName, elmnt, color) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].classList.remove("active");
    }
    document.getElementById(pageName).style.display = "block";
    elmnt.classList.add("active");

    if (pageName === 'News' && !complexExercisesInitialized) {
        initializeComplexExercises();
        complexExercisesInitialized = true;
    }
}

function initializeComplexExercises() {
    alert("Welcome to Complex Exercises!");
    const script = document.createElement('script');
    script.src = './JS/complex.js';
    document.body.appendChild(script);
}

document.getElementById("defaultOpen").click();