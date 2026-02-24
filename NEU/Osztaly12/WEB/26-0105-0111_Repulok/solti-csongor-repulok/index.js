import { createFlight, getFlights } from './src/flights.js';
import renderCLITable from './src/render.js';
import chalk from 'chalk';
import fs from 'fs';

// 17. Generate 200 flights
for (let i = 0; i < 200; i++) {
    createFlight();
}

// Function to strip ANSI codes for file output
const stripAnsi = (str) => str.replace(/\x1B\[[0-9;]*[mG]/g, '');

// 23.a & 25. Bonus: Multiple columns coloring
console.log(chalk.bgBlue.black(' Airbus ') + ' repülők:');
const airbusFlights = getFlights({ airplane: 'Airbus' });
const airbusTable = renderCLITable(airbusFlights, { airplane: 'blue', airline: 'yellow' });
console.log(airbusTable);
fs.writeFileSync('airbus_flights.txt', stripAnsi(airbusTable));

console.log('\n');

// 23.b
console.log(chalk.bgGreen.black(' Boeing ') + ' repülők az ' + chalk.bgBlue.black(' Air ') + ' szót tartalmazó légitársaságoktól:');
const boeingAirFlights = getFlights({ airplane: 'Boeing', airline: 'Air' }, 'flightNumber');
const boeingTable = renderCLITable(boeingAirFlights, { airline: 'blue' });
console.log(boeingTable);
fs.writeFileSync('boeing_air_flights.txt', stripAnsi(boeingTable));

// 26. Bonus: CLI Parameter filtering
// Example: node index.js airplane Airbus flightNumber 3
const args = process.argv.slice(2);
if (args.length >= 2) {
    console.log('\n' + chalk.bgMagenta.black(' Egyéni szűrés ') + ':');
    const customFilter = {};
    for (let i = 0; i < args.length; i += 2) {
        if (args[i+1]) {
            customFilter[args[i]] = args[i+1];
        }
    }
    const customFlights = getFlights(customFilter);
    const customTable = renderCLITable(customFlights, { [Object.keys(customFilter)[0]]: 'cyan' });
    console.log(customTable);
    fs.writeFileSync('custom_filter_results.txt', stripAnsi(customTable));
}
