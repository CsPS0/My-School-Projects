// Main application
document.addEventListener('DOMContentLoaded', () => {
  initializeApp();
});

function initializeApp() {
  loadMenu();
  setupNavigation();
  setupBookingForm();
  setMinDate();
}

// Navigation
function setupNavigation() {
  const navLinks = document.querySelectorAll('.nav-link');

  navLinks.forEach(link => {
    link.addEventListener('click', (e) => {
      e.preventDefault();
      const target = link.getAttribute('href').substring(1);
      scrollToSection(target);

      // Update active state
      navLinks.forEach(l => l.classList.remove('active'));
      link.classList.add('active');
    });
  });
}

function scrollToSection(sectionId) {
  const section = document.getElementById(sectionId);
  if (section) {
    section.scrollIntoView({ behavior: 'smooth' });
  }
}

// Set minimum date for booking
function setMinDate() {
  const dateInput = document.getElementById('date');
  if (dateInput) {
    const today = new Date().toISOString().split('T')[0];
    dateInput.setAttribute('min', today);
  }
}

// Load and display menu
async function loadMenu() {
  try {
    const menuItems = await DataService.getMenu();
    displayMenu(menuItems);
    setupMenuFilters(menuItems);
  } catch (error) {
    console.error('Error loading menu:', error);
  }
}

function displayMenu(items, category = 'all') {
  const menuGrid = document.getElementById('menu-grid');
  if (!menuGrid) return;

  const filteredItems = category === 'all'
    ? items
    : items.filter(item => item.category === category);

  menuGrid.innerHTML = filteredItems.map(item => `
    <div class="menu-item">
      <div class="menu-item-header">
        <h3 class="menu-item-name">${item.name}</h3>
        <span class="menu-item-price">${item.price} Ft</span>
      </div>
      <p class="menu-item-description">${item.description}</p>
      <span class="menu-item-category">${item.category}</span>
      ${item.allergens ? `<p style="font-size: 12px; color: #999; margin-top: 8px;">Allergének: ${item.allergens}</p>` : ''}
    </div>
  `).join('');
}

function setupMenuFilters(menuItems) {
  const categoryBtns = document.querySelectorAll('.category-btn');

  categoryBtns.forEach(btn => {
    btn.addEventListener('click', () => {
      const category = btn.dataset.category;
      displayMenu(menuItems, category);

      // Update active state
      categoryBtns.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
    });
  });
}

// Booking form
function setupBookingForm() {
  const form = document.getElementById('booking-form');
  if (!form) return;

  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    // Get current user from Auth if available
    const userIdInput = document.getElementById('user-id');
    const userId = userIdInput ? userIdInput.value : null;

    const formData = {
      userId: userId ? parseInt(userId) : null,
      name: document.getElementById('name').value,
      email: document.getElementById('email').value,
      phone: document.getElementById('phone').value,
      date: document.getElementById('date').value,
      time: document.getElementById('time').value,
      guests: parseInt(document.getElementById('guests').value),
      tableNumber: parseInt(document.getElementById('table').value) || null
    };

    // Validate
    const validation = validateBooking(formData);

    if (!validation.success) {
      showMessage(validation.message, 'error');
      return;
    }

    // Save booking
    try {
      // API call
      await DataService.saveBooking(formData);
      showMessage('Foglalás sikeresen elküldve! Hamarosan felvesszük Önnel a kapcsolatot.', 'success');
      form.reset();
      
      // If we have a logged in user, pre-fill name/email again
      if (typeof getCurrentUser === 'function') {
          const user = getCurrentUser();
          if (user) {
              document.getElementById('name').value = user.fullName;
              document.getElementById('email').value = user.email;
              document.getElementById('user-id').value = user.id;
          }
      }

    } catch (error) {
      console.error(error);
      showMessage('Hiba történt a foglalás során. Kérjük, próbálja újra!', 'error');
    }
  });
}

function showMessage(message, type) {
  const messageDiv = document.getElementById('booking-message');
  if (!messageDiv) return;

  messageDiv.textContent = message;
  messageDiv.className = `form-message ${type}`;

  setTimeout(() => {
    messageDiv.className = 'form-message';
  }, 5000);
}