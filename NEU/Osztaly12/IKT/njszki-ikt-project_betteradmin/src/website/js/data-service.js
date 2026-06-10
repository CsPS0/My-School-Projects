// Data Service to handle API calls to server

const BASE_URL = window.location.origin;

const DataService = {
  _getToken() {
    return sessionStorage.getItem('authToken');
  },

  // Helper to make authenticated GET request
  async _apiGet(endpoint) {
    const token = this._getToken();
    const response = await fetch(`${BASE_URL}${endpoint}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      throw new Error(`Failed to fetch ${endpoint}`);
    }
    return await response.json();
  },

  // Helper to make authenticated POST request
  async _apiPost(endpoint, data) {
    const token = this._getToken();
    const response = await fetch(`${BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
    if (!response.ok) {
      throw new Error(`Failed to post ${endpoint}`);
    }
    return await response.json();
  },

  // Helper for public GET
  async _publicGet(endpoint) {
    const response = await fetch(`${BASE_URL}${endpoint}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch ${endpoint}`);
    }
    return await response.json();
  },

  // Helper for public POST
  async _publicPost(endpoint, data) {
    const response = await fetch(`${BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
    if (!response.ok) {
      throw new Error(`Failed to post ${endpoint}`);
    }
    return await response.json();
  },

  async getBookings() {
    return await this._apiGet('/bookings');
  },

  async getMenu() {
    return await this._publicGet('/menu');
  },

  async getTables() {
    return await this._apiGet('/tables');
  },

  // Save functions
  async saveBooking(booking) {
    // We use public post for bookings, but frontend ensures user is logged in
    // Optionally, we could pass the token here if the backend required it
    return await this._publicPost('/bookings', booking);
  },

  async saveMenuItem(menuItem) {
    return await this._apiPost('/menu', menuItem);
  },

  async saveTable(table) {
    return await this._apiPost('/tables', table);
  },

  async deleteBooking(id) {
    const token = this._getToken();
    const response = await fetch(`${BASE_URL}/bookings/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      throw new Error('Failed to delete booking');
    }
    return await response.json();
  },

  async deleteMenuItem(id) {
    const token = this._getToken();
    const response = await fetch(`${BASE_URL}/menu/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      throw new Error('Failed to delete menu item');
    }
    return await response.json();
  },

  async getSettings() {
    // Settings can be public or protected, usually public for reading
    return await this._publicGet('/settings');
  },

  async saveSettings(settings) {
    return await this._apiPost('/settings', settings);
  },

  async getUsers() {
    return await this._apiGet('/users');
  },

  async deleteUser(id) {
    const token = this._getToken();
    const response = await fetch(`${BASE_URL}/users/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.error || 'Failed to delete user');
    }
    return await response.json();
  }
};