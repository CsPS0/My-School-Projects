const express = require("express");
// const cors = require("cors");
const fs = require("fs");
const path = require("path");

const app = express();
const PORT = 3000;

// Middleware
// app.use(cors());
app.use((req, res, next) => {
  res.header('Access-Control-Allow-Origin', '*');
  res.header('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept, Authorization');
  if (req.method === 'OPTIONS') {
    res.sendStatus(200);
  } else {
    next();
  }
});
app.use(express.json());
app.use(express.static(path.join(__dirname, '../website')));

// Data files
const dataDir = path.join(__dirname, 'data');
const bookingsFile = path.join(dataDir, 'bookings.json');
const menuFile = path.join(dataDir, 'menu.json');
const tablesFile = path.join(dataDir, 'tables.json');
const usersFile = path.join(dataDir, 'users.json');
const settingsFile = path.join(dataDir, 'settings.json');

// Helper functions
function readJSON(file) {
  try {
    if (!fs.existsSync(file)) {
        return file === settingsFile ? {} : [];
    }
    return JSON.parse(fs.readFileSync(file, 'utf8'));
  } catch (error) {
    console.error(`Error reading ${file}:`, error);
    return file === settingsFile ? {} : [];
  }
}

function writeJSON(file, data) {
  try {
    fs.writeFileSync(file, JSON.stringify(data, null, 2));
  } catch (error) {
    console.error(`Error writing ${file}:`, error);
  }
}

// Auth middleware
function requireAuth(req, res, next) {
  const authHeader = req.headers.authorization;
  if (!authHeader || !authHeader.startsWith('Bearer ')) {
    return res.status(401).json({ error: 'Unauthorized' });
  }
  const token = authHeader.substring(7);
  // Simple token check for prototype
  if (!token) {
    return res.status(401).json({ error: 'Invalid token' });
  }
  next();
}

// Routes

// Test
app.get("/test", (req, res) => {
  res.send("Hello, world!");
});

// Login
app.post("/login", (req, res) => {
  const { username, password } = req.body;
  const users = readJSON(usersFile);
  
  const user = users.find(u => u.username === username && u.password === password);

  if (user) {
    // Return user info and a mock token
    const token = user.role === 'admin' ? 'admin-token' : `user-token-${user.id}`;
    const { password, ...userWithoutPassword } = user;
    res.json({ 
        token, 
        user: userWithoutPassword 
    });
  } else {
    res.status(401).json({ error: 'Invalid credentials' });
  }
});

// Register
app.post("/register", (req, res) => {
    const { username, password, email, fullName } = req.body;
    const users = readJSON(usersFile);

    if (users.find(u => u.username === username)) {
        return res.status(400).json({ error: 'Username already exists' });
    }
    if (users.find(u => u.email === email)) {
        return res.status(400).json({ error: 'Email already exists' });
    }

    const newUser = {
        id: users.length > 0 ? Math.max(...users.map(u => u.id)) + 1 : 1,
        username,
        password, // Note: In production, hash this!
        email,
        fullName,
        role: 'user'
    };

    users.push(newUser);
    writeJSON(usersFile, users);

    const { password: _, ...userWithoutPassword } = newUser;
    res.json({
        message: 'Registration successful',
        user: userWithoutPassword,
        token: `user-token-${newUser.id}`
    });
});

// Logout
app.post("/logout", (req, res) => {
  res.json({ message: 'Logged out' });
});

// Settings Routes
app.get("/settings", (req, res) => {
    const settings = readJSON(settingsFile);
    res.json(settings);
});

app.post("/settings", requireAuth, (req, res) => {
    // In a real app, check if user is admin based on token
    const settings = req.body;
    writeJSON(settingsFile, settings);
    res.json(settings);
});

// Public routes
app.get("/menu", (req, res) => {
  const menu = readJSON(menuFile);
  res.json(menu);
});

app.post("/bookings", (req, res) => {
  const bookings = readJSON(bookingsFile);
  const newBooking = { ...req.body, id: Date.now().toString() };
  bookings.push(newBooking);
  writeJSON(bookingsFile, bookings);
  res.json(newBooking);
});

// Protected routes
app.get("/bookings", requireAuth, (req, res) => {
  const bookings = readJSON(bookingsFile);
  res.json(bookings);
});

app.delete("/bookings/:id", requireAuth, (req, res) => {
  const bookings = readJSON(bookingsFile);
  const filtered = bookings.filter(b => b.id !== req.params.id);
  writeJSON(bookingsFile, filtered);
  res.json({ message: 'Booking deleted' });
});

app.get("/tables", requireAuth, (req, res) => {
  const tables = readJSON(tablesFile);
  res.json(tables);
});

app.post("/tables", requireAuth, (req, res) => {
  const tables = readJSON(tablesFile);
  const newTable = { ...req.body, id: Date.now() };
  tables.push(newTable);
  writeJSON(tablesFile, tables);
  res.json(newTable);
});

app.delete("/tables/:id", requireAuth, (req, res) => {
  const tables = readJSON(tablesFile);
  const filtered = tables.filter(t => t.id != req.params.id);
  writeJSON(tablesFile, filtered);
  res.json({ message: 'Table deleted' });
});

app.post("/menu", requireAuth, (req, res) => {
  const menu = readJSON(menuFile);
  const newItem = { ...req.body, id: Date.now() };
  menu.push(newItem);
  writeJSON(menuFile, menu);
  res.json(newItem);
});

app.delete("/menu/:id", requireAuth, (req, res) => {
  const menu = readJSON(menuFile);
  const filtered = menu.filter(item => item.id != req.params.id);
  writeJSON(menuFile, filtered);
  res.json({ message: 'Menu item deleted' });
});

// User management routes
app.get("/users", requireAuth, (req, res) => {
  const users = readJSON(usersFile);
  // Remove passwords before sending
  const usersWithoutPasswords = users.map(u => {
    const { password, ...userWithoutPassword } = u;
    return userWithoutPassword;
  });
  res.json(usersWithoutPasswords);
});

app.delete("/users/:id", requireAuth, (req, res) => {
  const users = readJSON(usersFile);
  const userId = parseInt(req.params.id);
  
  const userToDelete = users.find(u => u.id === userId);
  if (!userToDelete) {
    return res.status(404).json({ error: 'User not found' });
  }

  if (userToDelete.username === 'admin') {
    return res.status(400).json({ error: 'Cannot delete admin user' });
  }

  const filtered = users.filter(u => u.id !== userId);
  writeJSON(usersFile, filtered);
  res.json({ message: 'User deleted' });
});

// Helper to get local IP address
function getLocalIP() {
  const { networkInterfaces } = require('os');
  const nets = networkInterfaces();
  const results = [];

  for (const name of Object.keys(nets)) {
    for (const net of nets[name]) {
      // Skip over non-IPv4 and internal (i.e. 127.0.0.1) addresses
      if (net.family === 'IPv4' && !net.internal) {
        results.push(net.address);
      }
    }
  }
  return results;
}

app.listen(PORT, '0.0.0.0', () => {
  const ips = getLocalIP();
  console.log(`Server running on:`);
  console.log(`- Local:   http://localhost:${PORT}`);
  ips.forEach(ip => {
    console.log(`- Network: http://${ip}:${PORT}`);
  });
});