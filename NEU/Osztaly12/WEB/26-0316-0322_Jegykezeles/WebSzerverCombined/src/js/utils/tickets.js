import { BASE_URL } from './config.js';

export const GetTickets = async () => {
    const response = await fetch(`${BASE_URL}/tickets`);
    if (response.ok) {
        const json = await response.json();
        return json.data;
    }
    return [];
};

export const GetTicket = async (id) => {
    const response = await fetch(`${BASE_URL}/tickets/${id}`);
    if (response.ok) {
        const json = await response.json();
        return json.data;
    }
    return null;
};

export const CreateTicket = async (data) => {
    const response = await fetch(`${BASE_URL}/tickets`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
    if (response.ok) {
        const json = await response.json();
        return json.data;
    }
    return null;
};
