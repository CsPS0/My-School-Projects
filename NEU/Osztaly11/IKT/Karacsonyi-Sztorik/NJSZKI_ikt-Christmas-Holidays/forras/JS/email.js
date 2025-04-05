document.addEventListener('DOMContentLoaded', function () {
    const EMAILJS_USER_ID = 'API-KEY';
    const EMAILJS_SERVICE_ID = 'API-KEY';
    const EMAILJS_TEMPLATE_ID = 'API-KEY';

    const form = document.getElementById('story-form');
    
    if (form) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            // Kivesszük az adatokat a form mezőkből
            const from_name = form.querySelector('[name="name"]').value;
            const from_email = form.querySelector('[name="from_email"]').value;
            const story = form.querySelector('[name="story"]').value;

            const templateParams = {
                from_name: from_name,
                from_email: from_email,
                story: story
            };

            console.log('Template Params:', templateParams); // Ellenőrzés

            // Küldés az EmailJS-nek
            emailjs.send(EMAILJS_SERVICE_ID, EMAILJS_TEMPLATE_ID, templateParams, EMAILJS_USER_ID)
                .then(function(response) {
                    console.log('Success:', response);
                    alert('Köszönjük a történetet!');
                }, function(error) {
                    console.error('Error:', error);
                    alert('Hiba történt a küldés során.');
                });
        });
    } else {
        console.error('Form element not found!');
    }
});