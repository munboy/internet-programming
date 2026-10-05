// main.js - головна сторінка: годинник у шапці та стан вечірньої акції.
// Скрипт підключено з атрибутом defer, тому DOM уже побудований.

const clock = document.getElementById('clock');
const promoCard = document.getElementById('evening-promo');
const promoState = document.getElementById('promo-state');

const PROMO_START_HOUR = 18; // "друга чашка -50%" діє з 18:00

function updateClock() {
    const now = new Date();

    const date = now.toLocaleDateString('uk-UA', {
        weekday: 'long',
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    });
    const time = now.toLocaleTimeString('uk-UA');

    clock.textContent = date + ', ' + time;
    clock.setAttribute('datetime', now.toISOString());

    // вмикаємо або вимикаємо підсвічування акції
    if (now.getHours() >= PROMO_START_HOUR) {
        promoCard.classList.add('promo-active');
        promoState.textContent = 'Акція діє зараз!';
    } else {
        promoCard.classList.remove('promo-active');
        const hoursLeft = PROMO_START_HOUR - now.getHours();
        promoState.textContent = 'Почнеться о 18:00 (приблизно через ' + hoursLeft + ' год.)';
    }
}

updateClock();
setInterval(updateClock, 1000);
