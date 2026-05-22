import bodyParser from 'body-parser';
import jsonServer from 'json-server';
import fs from 'fs';
import multer from 'multer';
import * as relationships from './relationships.js';

const server = jsonServer.create();
const router = jsonServer.router('data/db.json');
const config = JSON.parse(fs.readFileSync('./data/config.json', 'utf-8'));
const routes = JSON.parse(fs.readFileSync('./data/routes.json', 'utf-8'));

const forms = multer();
const middlewares = jsonServer.defaults({
    static: './dist'
});

server.use(middlewares);
server.use(jsonServer.rewriter(routes));
server.use(jsonServer.bodyParser);
server.use(forms.array());
server.use(bodyParser.urlencoded({ extended: true }));

if (config.timestamps) {
    server.use((req, res, next) => {
        if (req.method === 'POST') {
            req.body.created_at = new Date().toISOString()
            req.body.updated_at = new Date().toISOString()
        }
        if (req.method === 'PUT') {
            req.body.updated_at = new Date().toISOString()
        }
        next()
    })
}

router.render = (req, res) => {
    let data = res.locals.data;
    const GetDestinationArray = destination => router.db.toPlainObject()['__wrapped__'][destination];

    for (const rel of config.relationships) {
        relationships.hasManyIndex(rel, req, data, GetDestinationArray);
        relationships.hasManySingle(rel, req, data, GetDestinationArray);
        data = relationships.belongsToIndex(rel, req, data, GetDestinationArray);
        relationships.belongsToSingle(rel, req, data, GetDestinationArray);
    }
    res.jsonp({ data });
}

server.use(router);
server.listen(8888, () => {
    console.log('A JSON Szerver fut a 8888-as porton (http://localhost:8888)');
});
