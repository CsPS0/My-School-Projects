// Settings Management
let restaurantSettings = {
  restaurantName: 'Rusztikus Étterem',
  address: '1052 Budapest, Petőfi Sándor utca 5.',
  phone: '+36 1 234 5678',
  email: 'info@rusztikusetterem.hu',
  openingHours: {
    weekdays: '11:00 - 23:00',
    weekends: '12:00 - 24:00'
  }
};

// Load settings on page load
document.addEventListener('DOMContentLoaded', async () => {
  await loadSettings();
  applySettingsToPage();
});

// Load settings from API
async function loadSettings() {
    try {
        const response = await fetch(`${window.location.origin}/settings`);
        if (response.ok) {      restaurantSettings = await response.json();
    }
  } catch (error) {
    console.log('Error loading settings, using defaults', error);
  }
}

// Apply settings to page elements
function applySettingsToPage() {
  // Header
  const restaurantNameElements = document.querySelectorAll('#restaurant-name, #footer-restaurant-name');
  restaurantNameElements.forEach(el => {
    if (el) el.textContent = restaurantSettings.restaurantName;
  });

  // Update page title
  document.title = `${restaurantSettings.restaurantName} - Foglalás és Menü`;

  // Footer
  const addressEl = document.getElementById('footer-address');
  if (addressEl) addressEl.textContent = `📍 ${restaurantSettings.address}`;

  const phoneEl = document.getElementById('footer-phone');
  if (phoneEl) phoneEl.textContent = `📞 ${restaurantSettings.phone}`;

  const emailEl = document.getElementById('footer-email');
  if (emailEl) emailEl.textContent = `✉️ ${restaurantSettings.email}`;

  const weekdaysEl = document.getElementById('footer-weekdays');
  if (weekdaysEl) weekdaysEl.textContent = `Hétfő - Péntek: ${restaurantSettings.openingHours.weekdays}`;

  const weekendsEl = document.getElementById('footer-weekends');
  if (weekendsEl) weekdaysEl.textContent = `Szombat - Vasárnap: ${restaurantSettings.openingHours.weekends}`;
}