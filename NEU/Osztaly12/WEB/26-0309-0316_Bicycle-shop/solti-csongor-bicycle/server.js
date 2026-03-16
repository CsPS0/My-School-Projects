import http from 'node:http';
import fs from 'node:fs';

const server = http.createServer((req, res) => {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  if (req.method === 'OPTIONS') {
    res.writeHead(204);
    res.end();
    return;
  }

  const db = JSON.parse(fs.readFileSync('./db.json', 'utf8'));

  if (req.url === '/bicycles' && req.method === 'GET') {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(db.bicycles));
  } else if (req.url === '/bicycles' && req.method === 'POST') {
    let body = '';
    req.on('data', chunk => body += chunk.toString());
    req.on('end', () => {
      const newItem = JSON.parse(body);
      newItem.id = db.bicycles.length > 0 ? Math.max(...db.bicycles.map(b => b.id)) + 1 : 1;
      db.bicycles.push(newItem);
      fs.writeFileSync('./db.json', JSON.stringify(db, null, 2));
      res.writeHead(201, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify(newItem));
    });
  } else if (req.url.startsWith('/bicycles/') && (req.method === 'PUT' || req.method === 'DELETE')) {
    const id = parseInt(req.url.split('/').pop());
    if (req.method === 'PUT') {
      let body = '';
      req.on('data', chunk => body += chunk.toString());
      req.on('end', () => {
        const updatedItem = JSON.parse(body);
        const index = db.bicycles.findIndex(b => b.id === id);
        if (index !== -1) {
          db.bicycles[index] = { ...updatedItem, id };
          fs.writeFileSync('./db.json', JSON.stringify(db, null, 2));
          res.writeHead(200, { 'Content-Type': 'application/json' });
          res.end(JSON.stringify(db.bicycles[index]));
        } else {
          res.writeHead(404);
          res.end();
        }
      });
    } else {
      const index = db.bicycles.findIndex(b => b.id === id);
      if (index !== -1) {
        const deleted = db.bicycles.splice(index, 1);
        fs.writeFileSync('./db.json', JSON.stringify(db, null, 2));
        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(deleted[0]));
      } else {
        res.writeHead(404);
        res.end();
      }
    }
  } else {
    res.writeHead(404);
    res.end();
  }
});

server.listen(8888, '0.0.0.0', () => {
  console.log('Mock server running on port 8888');
});
