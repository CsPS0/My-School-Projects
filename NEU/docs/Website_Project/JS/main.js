"use strict"

document.addEventListener('DOMContentLoaded', function() {
    // Theme toggle functionality
    const themeToggle = document.getElementById('theme-toggle');
    const body = document.body;
    const icon = themeToggle.querySelector('i');
    
    // Check for saved theme preference
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'light') {
        body.classList.add('light-theme');
        icon.classList.remove('fa-moon');
        icon.classList.add('fa-sun');
    }
    
    themeToggle.addEventListener('click', () => {
        body.classList.toggle('light-theme');
        
        // Update icon
        if (body.classList.contains('light-theme')) {
            icon.classList.remove('fa-moon');
            icon.classList.add('fa-sun');
            localStorage.setItem('theme', 'light');
        } else {
            icon.classList.remove('fa-sun');
            icon.classList.add('fa-moon');
            localStorage.setItem('theme', 'dark');
        }
    });

    // Mobile menu functionality
    const hamburger = document.getElementById('hamburger-menu');
    const navContainer = document.querySelector('.nav-links-container');
    
    hamburger.addEventListener('click', () => {
        navContainer.classList.toggle('active');
        const isExpanded = navContainer.classList.contains('active');
        hamburger.setAttribute('aria-expanded', isExpanded);
    });

    // Close mobile menu when clicking outside
    document.addEventListener('click', (e) => {
        if (!hamburger.contains(e.target) && !navContainer.contains(e.target)) {
            navContainer.classList.remove('active');
            hamburger.setAttribute('aria-expanded', false);
        }
    });

    // Carousel functionality
    const slides = document.querySelectorAll('.carousel-slide');
    let currentSlide = 0;
    
    function showSlide(index) {
        slides.forEach(slide => slide.classList.remove('active'));
        slides[index].classList.add('active');
    }
    
    function nextSlide() {
        currentSlide = (currentSlide + 1) % slides.length;
        showSlide(currentSlide);
    }
    
    // Change slide every 5 seconds
    if (slides.length > 0) {
        setInterval(nextSlide, 5000);
    }

    // FAQ functionality
    const faqQuestions = document.querySelectorAll('.faq-question');
    
    function toggleFAQ(question) {
        const answer = question.nextElementSibling;
        const icon = question.querySelector('i');
        
        answer.classList.toggle('active');
        
        if (answer.classList.contains('active')) {
            icon.classList.remove('fa-plus-circle');
            icon.classList.add('fa-minus-circle');
        } else {
            icon.classList.remove('fa-minus-circle');
            icon.classList.add('fa-plus-circle');
        }
    }

    faqQuestions.forEach(question => {
        question.addEventListener('click', () => toggleFAQ(question));
    });

    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            
            const targetId = this.getAttribute('href').substring(1);
            if (targetId) {
                const targetElement = document.getElementById(targetId);
                if (targetElement) {
                    targetElement.scrollIntoView({
                        behavior: 'smooth'
                    });
                    
                    if (navContainer.classList.contains('active')) {
                        navContainer.classList.remove('active');
                    }
                }
            }
        });
    });
});