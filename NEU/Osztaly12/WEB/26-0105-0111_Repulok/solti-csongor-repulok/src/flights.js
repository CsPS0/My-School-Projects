import { faker } from '@faker-js/faker';

const flights = [];

export function createFlight() {
    const flight = {
        flightNumber: faker.string.numeric({ length: 4, allowLeadingZeros: true }),
        airline: faker.airline.airline().name,
        airplane: faker.airline.airplane().name,
        departure: faker.airline.airport().iataCode,
        arrival: faker.airline.airport().iataCode,
        date: faker.date.anytime(),
        captain: faker.person.fullName()
    };
    
    flights.push(flight);
    return flight;
}

export function getFlights(filter, sort) {
    let result = flights.filter(flight => {
        for (const key in filter) {
            // Flexible matching: check if the flight value includes the filter value (case insensitive) for strings
            // or exact match for others if needed. The task implies "filtering based on keys".
            // Task 26 says: "aircraft Airbus", "flightNumber 3".
            // It seems we should check if the property contains the value or matches.
            // Let's assume partial string match for robustness or exact based on requirement context.
            // Re-reading task 15a: "The given record appears if the value associated with the keys contains the given field!"
            // "tartalmazza az adott mező!" -> contains.
            
            const flightValue = String(flight[key]).toLowerCase();
            const filterValue = String(filter[key]).toLowerCase();
            
            if (!flightValue.includes(filterValue)) {
                return false;
            }
        }
        return true;
    });

    if (sort) {
        result.sort((a, b) => {
            if (a[sort] < b[sort]) return -1;
            if (a[sort] > b[sort]) return 1;
            return 0;
        });
    }

    return result;
}
