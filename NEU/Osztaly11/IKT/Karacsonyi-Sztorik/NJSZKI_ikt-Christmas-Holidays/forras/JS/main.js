document.addEventListener('DOMContentLoaded', () => {
    // Menü toggle
    const menuToggle = document.querySelector('.menu-toggle');
    const mainNav = document.querySelector('.main-nav');
    menuToggle?.addEventListener('click', () => {
        mainNav.classList.toggle('active');
        const spans = menuToggle.querySelectorAll('span');
        spans.forEach(span => span.classList.toggle('active'));
    });

    // Smooth scroll
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });

    // Newsletter form
    const newsletterForm = document.getElementById('newsletter-form');
    newsletterForm?.addEventListener('submit', (e) => {
        e.preventDefault();
        const email = e.target.querySelector('input[type="email"]').value;
        alert('Köszönjük a feliratkozást! (' + email + ')');
        e.target.reset();
    });

    // Lazy loading images
    const images = document.querySelectorAll('img[data-src]');
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.removeAttribute('data-src');
                observer.unobserve(img);
            }
        });
    });

    images.forEach(img => imageObserver.observe(img));

    // Snowflakes animation
    let snowflakesCount = 0;
    function createSnowflake() {
        if (snowflakesCount < 100) { // Csak 100 hópehely lehet egyszerre
            const snowflake = document.createElement('div');
            snowflake.classList.add('snowflake');
            snowflake.style.left = Math.random() * 100 + 'vw';
            snowflake.style.animationDuration = Math.random() * 3 + 2 + 's';
            snowflake.style.opacity = Math.random();
            snowflake.innerHTML = '❆';

            document.querySelector('.snow-animation')?.appendChild(snowflake);

            snowflakesCount++;
            setTimeout(() => {
                snowflake.remove();
                snowflakesCount--;
            }, 5000);
        }
    }

    function startSnowAnimation() {
        if (document.querySelector('.snow-animation')) {
            setInterval(createSnowflake, 200); // Kisebb intervallum a hópehely létrehozásához
        }
    }

    startSnowAnimation();

    // Karácsonyi visszaszámlálás (Még X nap karácsonyig)
    function updateChristmasCountdown() {
        const now = new Date();
        const christmas = new Date(now.getFullYear(), 11, 24);
        if (now > christmas) {
            christmas.setFullYear(christmas.getFullYear() + 1);
        }

        const diff = christmas - now;
        const days = Math.floor(diff / (1000 * 60 * 60 * 24));

        const countdownElement = document.querySelector('.christmas-countdown');
        if (countdownElement) {
            countdownElement.textContent = `Még ${days} nap karácsonyig!`;
        }
    }

    updateChristmasCountdown();

    // Karácsonyi visszaszámlálás (óra, perc, másodperc)
    function updateCountdown() {
        const christmasDate = new Date('2024-12-25T00:00:00');
        const now = new Date();
        const timeLeft = christmasDate - now;

        if (timeLeft > 0) {
            const days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
            const hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);

            document.getElementById("countdown").innerHTML =
                `${days}d ${hours}h ${minutes}m ${seconds}s`;
        } else {
            document.getElementById("countdown").innerHTML = "Boldog Karácsonyt!";
        }
    }

    setInterval(updateCountdown, 1000);

    // Navigációs linkek kiemelése az aktuális oldal alapján
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';
    const navLinks = document.querySelectorAll('.main-nav a');
    navLinks.forEach(link => {
        if (link.getAttribute('href') === currentPage) {
            link.classList.add('active');
        }
    });

    // Szűrő gombok
    const filterBtns = document.querySelectorAll('.filter-btn');
    const cards = document.querySelectorAll('.card');

    filterBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            filterBtns.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');

            const filter = btn.dataset.filter;

            cards.forEach(card => {
                if (filter === 'all' || card.dataset.category === filter) {
                    card.classList.remove('hidden');
                } else {
                    card.classList.add('hidden');
                }
            });
        });
    });
});