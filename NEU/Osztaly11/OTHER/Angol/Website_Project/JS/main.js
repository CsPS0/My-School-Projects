"use strict"

document.addEventListener('DOMContentLoaded', function() {
    const hamburgerMenu = document.getElementById('hamburger-menu');
    const navLinks = document.getElementById('nav-links');
    
    if (hamburgerMenu) {
        hamburgerMenu.addEventListener('click', function() {
            navLinks.classList.toggle('active');
        });
    }

    document.addEventListener('click', function(event) {
        if (!event.target.closest('nav') && navLinks.classList.contains('active')) {
            navLinks.classList.remove('active');
        }
    });

    const faqQuestions = document.querySelectorAll('.faq-question');
    
    faqQuestions.forEach(question => {
        question.addEventListener('click', function() {
            const answer = this.nextElementSibling;
            const icon = this.querySelector('i');
            
            answer.classList.toggle('active');
            
            if (answer.classList.contains('active')) {
                icon.classList.remove('fa-plus-circle');
                icon.classList.add('fa-minus-circle');
            } else {
                icon.classList.remove('fa-minus-circle');
                icon.classList.add('fa-plus-circle');
            }
        });
    });

    let currentSlide = 0;
    const slides = document.querySelectorAll('.carousel-slide');
    
    if (slides.length > 0) {
        function showSlide(index) {
            slides.forEach(slide => slide.classList.remove('active'));
            
            slides[index].classList.add('active');
        }
        
        function nextSlide() {
            currentSlide = (currentSlide + 1) % slides.length;
            showSlide(currentSlide);
        }
        
        setInterval(nextSlide, 5000);
        
        showSlide(currentSlide);
    }

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
                    
                    if (navLinks.classList.contains('active')) {
                        navLinks.classList.remove('active');
                    }
                }
            }
        });
    });
});