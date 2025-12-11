"use strict"

const ITEMS_PER_PAGE = 20;
let tracks = [...data];
const tracksContainer = document.querySelector('#tracks');

document.querySelector('#next-page').addEventListener('click', e => {
    if (tracksContainer.dataset.page > data.length / ITEMS_PER_PAGE) return;
    tracksContainer.dataset.page = +tracksContainer.dataset.page + 1;
    generateCards(tracksContainer.dataset.page);
});

document.querySelector('#prev-page').addEventListener('click', e => {
    if (tracksContainer.dataset.page <= 1) return;
    tracksContainer.dataset.page = +tracksContainer.dataset.page - 1;
    generateCards(tracksContainer.dataset.page);
});

const user = {
    username: 'CsPS',
    nickname: 'Csongi',
};

const { username, nickname } = user;
document.querySelector('#username').textContent = username;
document.querySelector('#nickname').textContent = nickname;


function createCard({ name, artists, image, explicit }) {
    const card = document.createElement('div');
    card.classList.add('card');
    
    const img = document.createElement('img');
    img.src = image;
    img.classList.add('card--cover');
    card.appendChild(img);
    
    const title = document.createElement('span');
    title.textContent = name;
    title.title = name;
    title.classList.add('card--track');
    card.appendChild(title);
    
    const artist = document.createElement('span');
    artist.title = artists.join(', ');
    artist.classList.add('card--artists');
    
    if (explicit) {
        const explicitSpan = document.createElement('span');
        explicitSpan.classList.add('card--explicit');
        artist.appendChild(explicitSpan);
    }
    
    artist.append(artists.join(', '));
    card.appendChild(artist);
    card.addEventListener('click', () => selectTrack({ name, artists, image }));
    return card;
}

const paginator = document.querySelector('#page-numbers');
function generatePaginator() {
    paginator.textContent = '';
    const pageCount = Math.ceil(tracks.length / ITEMS_PER_PAGE);
    for (let i = 1; i <= pageCount; i++) {
        const pageLink = document.createElement('a');
        pageLink.href = '#';
        pageLink.textContent = i;
        if (i == tracksContainer.dataset.page) {
            pageLink.classList.add('active');
        }
        pageLink.addEventListener('click', (e) => {
            e.preventDefault();
            tracksContainer.dataset.page = i;
            generateCards(i);
            generatePaginator();
        });
        paginator.appendChild(pageLink);
    }
}
generatePaginator();

function generateCards(page) {
    tracksContainer.textContent = '';
    const startIndex = (page - 1) * ITEMS_PER_PAGE;
    const endIndex = page * ITEMS_PER_PAGE;
    const tracksToShow = tracks.slice(startIndex, endIndex);
    for (const track of tracksToShow) {
        const card = createCard(track);
        tracksContainer.appendChild(card);
    }
}
generateCards(1);

function selectTrack({ name, artists, image }) {
    document.querySelector('#playing--cover').src = image;
    document.querySelector('#playing--track').textContent = name;
    document.querySelector('#playing--artists').textContent = artists.join(', ');
}

function filterTracks(value) {
    tracks = data.filter(track => {
        const searchTerm = value.toLowerCase();
        const nameMatch = track.name.toLowerCase().includes(searchTerm);
        const artistMatch = track.artists.some(artist => artist.toLowerCase().includes(searchTerm));
        return nameMatch || artistMatch;
    });
    tracksContainer.dataset.page = 1;
    generateCards(tracksContainer.dataset.page);
    generatePaginator();
}

const resultsText = document.querySelector('#number-of-results');
document.querySelector('#search').addEventListener('input', e => {
    filterTracks(e.currentTarget.value)
    resultsText.textContent = e.currentTarget.value ? `${tracks.length} találat a(z) "${e.currentTarget.value}" kifejezésre:` : "";
})