import { GetTickets, CreateTicket } from '../utils/tickets.js';

const ticketsContainer = document.getElementById('tickets');
const newTicketBtn = document.getElementById('new-ticket');
const actionModal = document.getElementById('action-modal');
const newTicketModal = document.getElementById('new-ticket-modal');
const newTicketForm = document.getElementById('new-ticket-form');
const closeActionModalBtn = document.getElementById('close-action-modal');
const closeNewTicketModalBtn = document.getElementById('close-new-ticket-modal');
const printViewBtn = document.getElementById('print-view');
const checkValidityBtn = document.getElementById('check-validity');

let cardTemplate = '';

const loadTemplate = async () => {
    const response = await fetch('/card.html');
    cardTemplate = await response.text();
};

const renderTickets = async () => {
    const tickets = await GetTickets();
    const today = new Date().toISOString().substring(0, 10);
    ticketsContainer.innerHTML = '';

    tickets.forEach(ticket => {
        const div = document.createElement('div');
        div.innerHTML = cardTemplate;
        const card = div.firstElementChild;
        
        card.querySelector('.route').textContent = `${ticket.from.toUpperCase()} - ${ticket.to.toUpperCase()}`;
        card.querySelector('.via').textContent = ticket.via.toUpperCase();
        card.querySelector('.valid').textContent = ticket.valid.toUpperCase();
        
        const isValid = ticket.valid >= today;
        if (isValid) {
            card.className = 'p-2 rounded cursor-pointer bg-green-50 border border-green-500';
        } else {
            card.className = 'p-2 rounded cursor-pointer bg-red-50 border border-red-500';
        }

        card.dataset.id = ticket.id;
        card.addEventListener('click', () => {
            actionModal.dataset.id = ticket.id;
            actionModal.showModal();
        });

        ticketsContainer.appendChild(card);
    });
};

newTicketBtn.addEventListener('click', () => {
    const today = new Date().toISOString().substring(0, 10);
    newTicketForm.valid.value = today;
    newTicketForm.valid.min = today;
    newTicketModal.showModal();
});

closeActionModalBtn.addEventListener('click', () => actionModal.close());
closeNewTicketModalBtn.addEventListener('click', () => newTicketModal.close());

printViewBtn.addEventListener('click', () => {
    const id = actionModal.dataset.id;
    window.location.href = `print.html?ticketId=${id}`;
});

checkValidityBtn.addEventListener('click', () => {
    const id = actionModal.dataset.id;
    window.location.href = `check.html?ticketId=${id}`;
});

newTicketForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const formData = new FormData(newTicketForm);
    const data = Object.fromEntries(formData.entries());
    
    data.price = Math.floor(Math.random() * 50) + 10;
    
    const result = await CreateTicket(data);
    if (result) {
        newTicketModal.close();
        newTicketForm.reset();
        await renderTickets();
    }
});

await loadTemplate();
await renderTickets();
