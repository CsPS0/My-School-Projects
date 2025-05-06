document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('brainrot-form');
    const result = document.querySelector('.result');
    const resultImg = result.querySelector('img');
    const resultBody = document.querySelector('.result-body');
    
    form.addEventListener('submit', function(e) {
        e.preventDefault();
        
        const name = document.getElementById('name').value;
        const age = document.getElementById('age').value;
        result.classList.remove('hidden');
        
        const brainrots = [
            'Tralalero Tralala', 
            'Bombardiro Crocodilo', 
            'Lirili Larila', 
            'Bomborito Bandito'
        ];
        
        const randomIndex = Math.floor(Math.random() * brainrots.length);
        const randomBrainrot = brainrots[randomIndex];
        resultImg.src = `img/brainrots/${randomBrainrot}.png`;
        resultImg.alt = randomBrainrot;
        resultBody.innerHTML = '';
        const heading = document.createElement('h3');
        heading.textContent = `Szia ${name}!`;
        const paragraph = document.createElement('p');
        paragraph.style.textAlign = 'center';

        switch(randomBrainrot) {
            case 'Tralalero Tralala':
                paragraph.textContent = `A te brainrotod ${age} éves korodban a Tralalero Tralala. Vigyázz a cipőidre!`;
                break;
            case 'Bombardiro Crocodilo':
                paragraph.textContent = `A te brainrotod ${age} éves korodban a Bombardiro Crocodilo. Vigyázz az égen!`;
                break;
            case 'Lirili Larila':
                paragraph.textContent = `A te brainrotod ${age} éves korodban a Lirili Larila. Vigyázz a sivatagban!`;
                break;
            case 'Bomborito Bandito':
                paragraph.textContent = `A te brainrotod ${age} éves korodban a Bomborito Bandito. Vigyázz a pénztárcádra!`;
                break;
        }
        
        resultBody.appendChild(heading);
        resultBody.appendChild(paragraph);
        result.style.margin = '1.5rem auto';
        result.style.padding = '0.3rem';
        result.style.backgroundColor = 'var(--green)';
        result.style.color = 'white';
    });
});