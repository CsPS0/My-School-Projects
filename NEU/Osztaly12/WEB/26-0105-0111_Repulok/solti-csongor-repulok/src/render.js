import Table from 'cli-table3';
import chalk from 'chalk';

export default function renderCLITable(flights, columnColors = {}) {
    const table = new Table({
        head: ['#', 'Légitársaság', 'Repülő', 'Honnan', 'Hova']
    });

    const keyMap = ['flightNumber', 'airline', 'airplane', 'departure', 'arrival'];

    flights.forEach(flight => {
        const { flightNumber, airline, airplane, departure, arrival } = flight;
        
        let row = [
            flightNumber,
            airline,
            airplane,
            departure,
            arrival
        ];

        row = row.map((val, index) => {
            const key = keyMap[index];
            if (columnColors[key] && chalk[columnColors[key]]) {
                return chalk[columnColors[key]](val);
            }
            return val;
        });

        table.push(row);
    });

    return table.toString();
}
