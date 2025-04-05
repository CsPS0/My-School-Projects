document.addEventListener('DOMContentLoaded', function () {
    const EMAILJS_USER_ID = '4OBCX1dlx8-UfEGAm';
    const EMAILJS_SERVICE_ID = 'service_7l419tm';
    const EMAILJS_TEMPLATE_ID = 'template_v4g9u8g';

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