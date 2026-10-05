// customers.js - клієнти та картки лояльності: пошук, підсвічування збігів,
// перевірка форми та реєстрація нового клієнта.

const customers = [
    { id: 1, name: 'Олександр Коваленко', phone: '+380 (50) 111-22-33', email: 'oleksandr.k@example.com' },
    { id: 2, name: 'Марія Шевченко', phone: '+380 (67) 222-33-44', email: 'maria.sh@example.com' },
    { id: 3, name: 'Дмитро Савченко', phone: '+380 (63) 333-44-55', email: 'd.savchenko@example.com' },
    { id: 4, name: 'Анастасія Литвиненко', phone: '+380 (99) 444-55-66', email: 'nastia.lyt@example.com' },
    { id: 5, name: 'Ігор Мороз', phone: '+380 (93) 555-66-77', email: 'igor.moroz@example.com' }
];

const cards = [
    { id: 1, customerId: 1, bonuses: 45, date: '01.09.2025' },
    { id: 2, customerId: 2, bonuses: 120, date: '15.09.2025' },
    { id: 3, customerId: 4, bonuses: 80, date: '20.10.2025' }
];

const START_BONUSES = 10;

const searchForm = document.getElementById('search-form');
const searchInput = document.getElementById('customer-search');
const customersBody = document.getElementById('customers-tbody');
const cardsBody = document.getElementById('cards-tbody');
const counter = document.getElementById('customers-counter');
const customerForm = document.getElementById('new-customer-form');
const nameInput = document.getElementById('cust-name');
const phoneInput = document.getElementById('cust-phone');
const emailInput = document.getElementById('cust-email');
const cardCheckbox = document.getElementById('cust-create-card');
const message = document.getElementById('customer-message');


// ---------- допоміжні функції ----------

function createCell(text, className) {
    const td = document.createElement('td');
    td.textContent = text;
    if (className) {
        td.className = className;
    }
    return td;
}

// комірка, у якій знайдений фрагмент обгорнуто тегом <mark>.
// Текст вставляється через createTextNode, тому HTML з поля пошуку не виконується
function createHighlightedCell(text, query) {
    const td = document.createElement('td');
    const position = text.toLowerCase().indexOf(query);

    if (query === '' || position === -1) {
        td.textContent = text;
        return td;
    }

    const mark = document.createElement('mark');
    mark.textContent = text.substring(position, position + query.length);

    td.appendChild(document.createTextNode(text.substring(0, position)));
    td.appendChild(mark);
    td.appendChild(document.createTextNode(text.substring(position + query.length)));
    return td;
}

function findCard(customerId) {
    return cards.find(function (card) {
        return card.customerId === customerId;
    });
}

function findCustomer(customerId) {
    return customers.find(function (customer) {
        return customer.id === customerId;
    });
}

function getNextId(list) {
    let maxId = 0;
    list.forEach(function (item) {
        if (item.id > maxId) {
            maxId = item.id;
        }
    });
    return maxId + 1;
}

// +380501234567, 0501234567, +38 (050) 123-45-67 -> "+380 (50) 123-45-67"
function normalizePhone(value) {
    let digits = value.replace(/\D/g, ''); // залишаємо тільки цифри
    if (digits.length === 10 && digits[0] === '0') {
        digits = '38' + digits;
    }
    if (!/^380\d{9}$/.test(digits)) {
        return null;
    }
    return '+380 (' + digits.substring(3, 5) + ') ' + digits.substring(5, 8) + '-' +
        digits.substring(8, 10) + '-' + digits.substring(10);
}

function showMessage(text, isError) {
    message.textContent = text;
    message.className = isError ? 'form-message error' : 'form-message success';
}


// ---------- відображення таблиць ----------

function renderCustomers() {
    const query = searchInput.value.trim().toLowerCase();
    customersBody.innerHTML = '';

    const found = customers.filter(function (customer) {
        const text = (customer.name + ' ' + customer.phone + ' ' + customer.email).toLowerCase();
        return text.includes(query);
    });

    found.forEach(function (customer) {
        const tr = document.createElement('tr');
        tr.appendChild(createCell(customer.id, 'center'));
        tr.appendChild(createHighlightedCell(customer.name, query));
        tr.appendChild(createHighlightedCell(customer.phone, query));
        tr.appendChild(createHighlightedCell(customer.email, query));

        const tdCard = document.createElement('td');
        tdCard.className = 'center';
        const status = document.createElement('span');
        const card = findCard(customer.id);
        if (card) {
            status.className = 'status-ok';
            status.textContent = 'Активна (картка №' + card.id + ')';
        } else {
            status.className = 'status-bad';
            status.textContent = 'Відсутня';
        }
        tdCard.appendChild(status);
        tr.appendChild(tdCard);

        customersBody.appendChild(tr);
    });

    if (found.length === 0) {
        const tr = document.createElement('tr');
        tr.className = 'empty-row';
        const td = createCell('За запитом «' + searchInput.value.trim() + '» нікого не знайдено');
        td.colSpan = 5;
        tr.appendChild(td);
        customersBody.appendChild(tr);
    }

    counter.textContent = 'Показано ' + found.length + ' з ' + customers.length + ' клієнтів';
}

function renderCards() {
    cardsBody.innerHTML = '';

    cards.forEach(function (card) {
        const owner = findCustomer(card.customerId);
        const tr = document.createElement('tr');
        tr.appendChild(createCell(card.id, 'center'));
        tr.appendChild(createCell(card.customerId, 'center'));
        tr.appendChild(createCell(owner ? owner.name : '-'));
        tr.appendChild(createCell(card.bonuses, 'center'));
        tr.appendChild(createCell(card.date, 'center'));
        cardsBody.appendChild(tr);
    });
}


// ---------- обробники подій ----------

// пошук при кожному введеному символі
searchInput.addEventListener('input', renderCustomers);

// Enter у полі пошуку не повинен перезавантажувати сторінку
searchForm.addEventListener('submit', function (event) {
    event.preventDefault();
});

customerForm.addEventListener('submit', function (event) {
    event.preventDefault();

    const name = nameInput.value.trim().replace(/\s+/g, ' ');
    const phone = normalizePhone(phoneInput.value);
    const email = emailInput.value.trim();

    nameInput.classList.remove('invalid');
    phoneInput.classList.remove('invalid');

    // ПІБ: хоча б два слова з літер
    if (!/^[A-Za-zА-Яа-яІіЇїЄєҐґ'-]{2,}( [A-Za-zА-Яа-яІіЇїЄєҐґ'-]{2,})+$/.test(name)) {
        nameInput.classList.add('invalid');
        showMessage('Вкажіть прізвище та ім\'я літерами, наприклад: Петренко Ольга.', true);
        return;
    }

    if (phone === null) {
        phoneInput.classList.add('invalid');
        showMessage('Телефон має бути українським номером: +380XXXXXXXXX.', true);
        return;
    }

    const duplicate = customers.some(function (customer) {
        return customer.phone === phone;
    });
    if (duplicate) {
        phoneInput.classList.add('invalid');
        showMessage('Клієнт з номером ' + phone + ' уже зареєстрований.', true);
        return;
    }

    const newCustomer = {
        id: getNextId(customers),
        name: name,
        phone: phone,
        email: email
    };
    customers.push(newCustomer);

    let text = 'Клієнта ' + name + ' зареєстровано (ID ' + newCustomer.id + ').';

    if (cardCheckbox.checked) {
        const newCard = {
            id: getNextId(cards),
            customerId: newCustomer.id,
            bonuses: START_BONUSES,
            date: new Date().toLocaleDateString('uk-UA')
        };
        cards.push(newCard);
        text += ' Видано картку №' + newCard.id + ' з ' + START_BONUSES + ' бонусами.';
    }

    customerForm.reset();
    searchInput.value = ''; // щоб новий клієнт одразу було видно в таблиці
    renderCustomers();
    renderCards();
    showMessage(text, false);
});


renderCustomers();
renderCards();
