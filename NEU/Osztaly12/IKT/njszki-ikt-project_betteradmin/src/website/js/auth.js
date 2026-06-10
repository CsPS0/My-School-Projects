// Authentication Management
let currentUser = null;
let authToken = null;

// Initialize auth on page load
document.addEventListener('DOMContentLoaded', () => {
  checkStoredAuth();
});

// Check if user is already logged in (from sessionStorage)
function checkStoredAuth() {
  const storedUser = sessionStorage.getItem('currentUser');
  const storedToken = sessionStorage.getItem('authToken');
  
  if (storedUser && storedToken) {
    currentUser = JSON.parse(storedUser);
    authToken = storedToken;
    
    if (currentUser.role === 'admin') {
        showAdminUI();
    } else {
        updateUIForLoggedInUser();
    }
  } else {
      updateUIForLoggedOutUser();
  }
}

// Login
async function handleLogin(event) {
  event.preventDefault();

  const usernameInput = document.getElementById('login-username');
  const passwordInput = document.getElementById('login-password');
  const errorDiv = document.getElementById('login-error');
  
  const username = usernameInput.value;
  const password = passwordInput.value;

  try {
    const response = await fetch(`${window.location.origin}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    });

    const data = await response.json();

    if (response.ok) {
        currentUser = data.user;
        authToken = data.token;
        sessionStorage.setItem('currentUser', JSON.stringify(currentUser));
        sessionStorage.setItem('authToken', authToken);
        
        closeLoginModal();
        usernameInput.value = '';
        passwordInput.value = '';

        if (currentUser.role === 'admin') {
            showAdminUI();
        } else {
            updateUIForLoggedInUser();
        }

    } else {
        errorDiv.textContent = data.error || 'Hibás felhasználónév vagy jelszó!';
        errorDiv.style.display = 'block';
    }
  } catch (error) {
    console.error('Login error:', error);
    errorDiv.textContent = 'Hiba történt a bejelentkezés során!';
    errorDiv.style.display = 'block';
  }
}

function showAdminUI() {
    console.log('showAdminUI called');
    // Hide main site
    const mainWebsite = document.getElementById('main-website');
    const adminPanel = document.getElementById('admin-panel');
    
    if (mainWebsite) {
        console.log('Hiding main website');
        mainWebsite.style.display = 'none';
    }
    
    if (adminPanel) {
        console.log('Showing admin panel');
        adminPanel.style.display = 'block';
        adminPanel.innerHTML = '<div style="display:flex;justify-content:center;align-items:center;height:100vh;"><h1>Admin felület betöltése...</h1></div>';
    } else {
        console.error('CRITICAL: admin-panel div not found in DOM');
        return;
    }
    
    // Initialize Admin Interface (from admin.js)
    if (typeof loadAdminInterface === 'function') {
        console.log('Calling loadAdminInterface...');
        loadAdminInterface();
    } else {
        console.error('Admin interface not found! Ensure admin.js is loaded.');
        if (adminPanel) {
            adminPanel.innerHTML = '<div style="color:red;padding:20px;">Hiba: Az adminisztrációs felület nem tölthető be. (admin.js hiányzik vagy hibás)</div>';
        }
    }
}

// Signup
async function handleSignup(event) {
  event.preventDefault();

  const fullname = document.getElementById('signup-fullname').value;
  const username = document.getElementById('signup-username').value;
  const email = document.getElementById('signup-email').value;
  const password = document.getElementById('signup-password').value;
  const errorDiv = document.getElementById('signup-error');
  const successDiv = document.getElementById('signup-success');

  try {
    const response = await fetch(`${window.location.origin}/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password, email, fullName: fullname })
    });
    
    const data = await response.json();

    if (response.ok) {
        successDiv.textContent = 'Sikeres regisztráció! Most már bejelentkezhet.';
        successDiv.style.display = 'block';
        errorDiv.style.display = 'none';

        // Clear form
        document.getElementById('signup-form').reset();

        // Auto close and open login after 2 seconds
        setTimeout(() => {
            closeSignupModal();
            showLoginModal();
        }, 2000);
    } else {
        errorDiv.textContent = data.error || 'Hiba történt a regisztráció során!';
        errorDiv.style.display = 'block';
        successDiv.style.display = 'none';
    }

  } catch (error) {
    console.error('Signup error:', error);
    errorDiv.textContent = 'Hiba történt a regisztráció során!';
    errorDiv.style.display = 'block';
  }
}

// Logout
function handleLogout() {
  currentUser = null;
  authToken = null;
  sessionStorage.removeItem('currentUser');
  sessionStorage.removeItem('authToken');
  
  // Reset UI
  document.getElementById('admin-panel').style.display = 'none';
  document.getElementById('main-website').style.display = 'block';
  
  updateUIForLoggedOutUser();
  window.location.reload(); 
}

// Update UI based on login state
function updateUIForLoggedInUser() {
  // Hide auth buttons, show user menu
  const navAuth = document.getElementById('nav-auth');
  const navUser = document.getElementById('nav-user');
  const userName = document.getElementById('user-name');
  
  if (navAuth) navAuth.style.display = 'none';
  if (navUser) navUser.style.display = 'flex';
  if (userName && currentUser) userName.textContent = currentUser.fullName || currentUser.username;

  // Show booking form, hide login required notice
  const loginRequired = document.getElementById('booking-login-required');
  const bookingForm = document.getElementById('booking-form-container');
  
  if (loginRequired) loginRequired.style.display = 'none';
  if (bookingForm) bookingForm.style.display = 'block';

  // Pre-fill booking form
  if (currentUser) {
      const userIdInput = document.getElementById('user-id');
      const nameInput = document.getElementById('name');
      const emailInput = document.getElementById('email');

      if (userIdInput) userIdInput.value = currentUser.id;
      if (nameInput) nameInput.value = currentUser.fullName;
      if (emailInput) emailInput.value = currentUser.email;
  }
}

function updateUIForLoggedOutUser() {
  // Show auth buttons, hide user menu
  const navAuth = document.getElementById('nav-auth');
  const navUser = document.getElementById('nav-user');
  
  if (navAuth) navAuth.style.display = 'flex';
  if (navUser) navUser.style.display = 'none';

  // Hide booking form, show login required notice
  const loginRequired = document.getElementById('booking-login-required');
  const bookingForm = document.getElementById('booking-form-container');
  
  if (loginRequired) loginRequired.style.display = 'flex';
  if (bookingForm) bookingForm.style.display = 'none';
}

// Modal functions
function showLoginModal() {
  const modal = document.getElementById('login-modal');
  if (modal) {
      modal.classList.add('active');
      const errorDiv = document.getElementById('login-error');
      if (errorDiv) errorDiv.style.display = 'none';
  }
}

function closeLoginModal() {
  const modal = document.getElementById('login-modal');
  if (modal) modal.classList.remove('active');
}

function showSignupModal() {
  const modal = document.getElementById('signup-modal');
  if (modal) {
      modal.classList.add('active');
      const errorDiv = document.getElementById('signup-error');
      const successDiv = document.getElementById('signup-success');
      if (errorDiv) errorDiv.style.display = 'none';
      if (successDiv) successDiv.style.display = 'none';
  }
}

function closeSignupModal() {
  const modal = document.getElementById('signup-modal');
  if (modal) modal.classList.remove('active');
}

// Helper to scroll to booking with login check
function scrollToBooking() {
  if (!currentUser) {
    const bookingSection = document.getElementById('booking');
    if (bookingSection) bookingSection.scrollIntoView({ behavior: 'smooth' });
    // Optionally show login modal after scrolling
    setTimeout(() => {
      showLoginModal();
    }, 500);
  } else {
    const bookingSection = document.getElementById('booking');
    if (bookingSection) bookingSection.scrollIntoView({ behavior: 'smooth' });
  }
}

// Check if user is logged in
function isLoggedIn() {
  return currentUser !== null;
}

// Get current user
function getCurrentUser() {
  return currentUser;
}