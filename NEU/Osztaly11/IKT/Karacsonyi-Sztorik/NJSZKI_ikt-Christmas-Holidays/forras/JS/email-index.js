// Initialize EmailJS
emailjs.init("4OBCX1dlx8-UfEGAm");  // A fenti EmailJS felhasználói ID-t használd

// Form küldés kezelése
document.getElementById("subscribe-form").addEventListener("submit", function(event) {
  event.preventDefault(); // Ne frissítse az oldalt

// A form adatok összegyűjtése
var formData = {
    name: document.getElementById("name").value,
    email: document.getElementById("email").value
};

// Email küldése
emailjs.send('service_7l419tm', 'template_snm1yi9', formData)
    .then(function(response) {
        alert('Feliratkozás sikeres!');
    }, function(error) {
        alert('Hiba történt: ' + error.text);
    });
});