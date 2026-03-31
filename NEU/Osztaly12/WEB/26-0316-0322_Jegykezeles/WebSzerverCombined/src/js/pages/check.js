import { GetTicket } from '../utils/tickets.js';

const typeText = {
    single: 'Menetjegy',
    return: 'Menettéri jegy',
}

const ticketId = new URLSearchParams(location.search).get('ticketId');

const fillData = async () => {
    if (!ticketId) return;

    const ticket = await GetTicket(ticketId);
    if (!ticket) return;

    document.getElementById('from').textContent = ticket.from.toUpperCase();
    document.getElementById('to').textContent = ticket.to.toUpperCase();
    document.getElementById('via').textContent = ticket.via.toUpperCase();
    document.getElementById('date').textContent = ticket.valid.toUpperCase();
    document.getElementById('type').textContent = typeText[ticket.type].toUpperCase();

    const today = new Date().toISOString().substring(0, 10);
    const isValid = ticket.valid >= today;
    const isValidEl = document.getElementById('is-valid');

    if (isValid) {
        isValidEl.textContent = 'ÉRVÉNYES';
        isValidEl.className = 'mt-2 p-2 rounded bg-green-600 text-white text-center text-4xl font-bold';
    } else {
        isValidEl.textContent = 'ÉRVÉNYTELEN';
        isValidEl.className = 'mt-2 p-2 rounded bg-red-600 text-white text-center text-4xl font-bold';
    }
};

fillData();
