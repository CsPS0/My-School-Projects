import { GetTicket } from '../utils/tickets.js';

const typeText = {
    single: {
        hu: 'Menetjegy',
        en: 'Ticket',
    },
    return: {
        hu: 'Menettéri jegy',
        en: 'Return ticket',
    },
}

const ticketId = new URLSearchParams(location.search).get('ticketId');

const fillData = async () => {
    if (!ticketId) return;

    const ticket = await GetTicket(ticketId);
    if (!ticket) return;

    document.getElementById('type-hu').textContent = typeText[ticket.type].hu.toUpperCase();
    document.getElementById('type-en').textContent = typeText[ticket.type].en.toUpperCase();
    document.getElementById('from').textContent = ticket.from.toUpperCase();
    document.getElementById('to').textContent = ticket.to.toUpperCase();
    document.getElementById('via').textContent = ticket.via.toUpperCase();
    document.getElementById('date').textContent = ticket.valid.toUpperCase();
    document.getElementById('price').textContent = new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(ticket.price).toUpperCase();
    document.getElementById('qr').src = `https://quickchart.io/qr?text=${ticket.id}`;
};

fillData();
