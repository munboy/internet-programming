// orders.js - касовий модуль: склад чека, підсумок, бонуси та історія чеків.
// Дані зберігаються в масивах, а таблиці щоразу будуються з них заново.

const BONUS_PERCENT = 0.1; // кешбек 10% на картку лояльності

// позиції поточного чека
let orderItems = [
    { name: 'Капучино', price: 55, qty: 1 }
];

// проведені за сьогодні чеки (новіші на початку масиву)
const orders = [
    { id: 103, time: '10:05', shop: 'Студентський куточок', barista: 'Сергій Ткаченко', customer: 'Марія Шевченко', total: 125 },
    { id: 102, time: '09:40', shop: "Кав'ярня на Подолі", barista: 'Олена Бойко', customer: 'Гість', total: 40 },
    { id: 101, time: '09:15', shop: 'Центральний', barista: 'Андрій Мельник', customer: 'Олександр Коваленко', total: 90 }
];
let nextOrderId = 104;

// елементи сторінки
const orderForm = document.getElementById('order-form');
const customerSelect = document.getElementById('cbCustomer');
const baristaSelect = document.getElementById('cbBarista');
const shopSelect = document.getElementById('cbShop');
const drinkSelect = document.getElementById('select-drink');
const qtyInput = document.getElementById('input-qty');
const addButton = document.getElementById('btn-add-item');
const calcButton = document.getElementById('btn-calculate');
const itemsBody = document.getElementById('order-items-tbody');
const ordersBody = document.getElementById('recent-orders-tbody');
const totalLabel = document.getElementById('lbl-total');
const bonusLabel = document.getElementById('bonus-info-label');
const message = document.getElementById('order-message');


// ---------- допоміжні функції ----------

function formatMoney(value) {
    return value.toFixed(2);
}

// комірка таблиці з текстом (textContent не виконує HTML-теги)
function createCell(text, className) {
    const td = document.createElement('td');
    td.textContent = text;
    if (className) {
        td.className = className;
    }
    return td;
}

// правильне закінчення: 1 бонус, 2 бонуси, 5 бонусів, 12 бонусів
function bonusWord(n) {
    const last = n % 10;
    const lastTwo = n % 100;
    if (last === 1 && lastTwo !== 11) {
        return 'бонус';
    }
    if (last >= 2 && last <= 4 && (lastTwo < 12 || lastTwo > 14)) {
        return 'бонуси';
    }
    return 'бонусів';
}

function showMessage(text, isError) {
    message.textContent = text;
    message.className = isError ? 'form-message error' : 'form-message success';
}

function getSelectedCustomer() {
    return customerSelect.options[customerSelect.selectedIndex];
}

function getTotal() {
    let total = 0;
    orderItems.forEach(function (item) {
        total += item.price * item.qty;
    });
    return total;
}


// ---------- відображення ----------

function renderItems() {
    itemsBody.innerHTML = ''; // очищаємо старі рядки

    if (orderItems.length === 0) {
        const tr = document.createElement('tr');
        tr.className = 'empty-row';
        const td = createCell('Чек порожній. Додайте напій зі списку вище.');
        td.colSpan = 5;
        tr.appendChild(td);
        itemsBody.appendChild(tr);
    }

    orderItems.forEach(function (item, index) {
        const tr = document.createElement('tr');
        tr.appendChild(createCell(item.name));
        tr.appendChild(createCell(item.qty, 'center'));
        tr.appendChild(createCell(formatMoney(item.price), 'num'));
        tr.appendChild(createCell(formatMoney(item.price * item.qty), 'num'));

        // кнопка видалення знає індекс свого рядка в масиві
        const tdButton = document.createElement('td');
        tdButton.className = 'center';
        const button = document.createElement('button');
        button.type = 'button';
        button.className = 'btn-remove';
        button.textContent = 'Видалити';
        button.dataset.index = index;
        tdButton.appendChild(button);
        tr.appendChild(tdButton);

        itemsBody.appendChild(tr);
    });

    updateTotal();
}

function updateTotal() {
    const total = getTotal();
    totalLabel.textContent = formatMoney(total) + ' грн';

    const customer = getSelectedCustomer();
    if (customer.dataset.card === 'true') {
        const bonus = Math.round(total * BONUS_PERCENT);
        bonusLabel.textContent = 'Кешбек на картку (10%): ' + bonus + ' ' + bonusWord(bonus) +
            '. Зараз на картці: ' + customer.dataset.bonuses + '.';
    } else {
        bonusLabel.textContent = 'Гість без картки лояльності, бонуси не нараховуються.';
    }
}

function renderOrders() {
    ordersBody.innerHTML = '';

    orders.forEach(function (order) {
        const tr = document.createElement('tr');
        tr.appendChild(createCell(order.id, 'center'));
        tr.appendChild(createCell(order.time, 'center'));
        tr.appendChild(createCell(order.shop));
        tr.appendChild(createCell(order.barista));
        tr.appendChild(createCell(order.customer));
        tr.appendChild(createCell(formatMoney(order.total), 'num'));

        const tdStatus = document.createElement('td');
        tdStatus.className = 'center';
        const status = document.createElement('span');
        status.className = 'status-ok';
        status.textContent = 'Виконано';
        tdStatus.appendChild(status);
        tr.appendChild(tdStatus);

        ordersBody.appendChild(tr);
    });
}


// ---------- обробники подій ----------

// додавання напою в чек
addButton.addEventListener('click', function () {
    const option = drinkSelect.options[drinkSelect.selectedIndex];
    const qty = parseInt(qtyInput.value, 10);

    if (isNaN(qty) || qty < 1 || qty > 50) {
        showMessage('Кількість має бути цілим числом від 1 до 50.', true);
        qtyInput.classList.add('invalid');
        qtyInput.focus();
        return;
    }
    qtyInput.classList.remove('invalid');

    const name = option.dataset.name;
    const price = Number(option.value);

    // якщо такий напій уже є в чеку, збільшуємо кількість
    const existing = orderItems.find(function (item) {
        return item.name === name;
    });
    if (existing) {
        existing.qty += qty;
    } else {
        orderItems.push({ name: name, price: price, qty: qty });
    }

    qtyInput.value = 1;
    showMessage('', false);
    renderItems();
});

// видалення позиції: один обробник на весь tbody (делегування подій)
itemsBody.addEventListener('click', function (event) {
    if (event.target.classList.contains('btn-remove')) {
        const index = Number(event.target.dataset.index);
        orderItems.splice(index, 1);
        renderItems();
    }
});

calcButton.addEventListener('click', function () {
    updateTotal();
    showMessage('Суму перераховано: ' + formatMoney(getTotal()) + ' грн.', false);
});

// при зміні клієнта змінюється текст про бонуси
customerSelect.addEventListener('change', updateTotal);

// оформлення покупки
orderForm.addEventListener('submit', function (event) {
    event.preventDefault(); // сторінка не перезавантажується

    if (orderItems.length === 0) {
        showMessage('Неможливо оформити порожній чек. Додайте хоча б один напій.', true);
        return;
    }

    const total = getTotal();
    const customer = getSelectedCustomer();
    const now = new Date();

    const order = {
        id: nextOrderId,
        time: now.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' }),
        shop: shopSelect.options[shopSelect.selectedIndex].text,
        barista: baristaSelect.options[baristaSelect.selectedIndex].text,
        customer: customer.dataset.name,
        total: total
    };
    orders.unshift(order); // новий чек на початок списку
    nextOrderId++;

    let text = 'Чек №' + order.id + ' на суму ' + formatMoney(total) + ' грн проведено.';

    // нараховуємо бонуси на картку клієнта
    if (customer.dataset.card === 'true') {
        const bonus = Math.round(total * BONUS_PERCENT);
        const newBalance = Number(customer.dataset.bonuses) + bonus;
        customer.dataset.bonuses = newBalance;
        customer.textContent = customer.dataset.name + ' (картка: ' + newBalance + ' ' + bonusWord(newBalance) + ')';
        text += ' Нараховано ' + bonus + ' ' + bonusWord(bonus) + ', на картці тепер ' + newBalance + '.';
    }

    orderItems = [];
    renderItems();
    renderOrders();
    showMessage(text, false);
});

// подія reset спрацьовує до того, як поля форми повернуться до початкових значень,
// тому перемальовуємо чек із нульовою затримкою
orderForm.addEventListener('reset', function () {
    orderItems = [];
    setTimeout(function () {
        renderItems();
        showMessage('Чек очищено.', false);
    }, 0);
});


// початкове відображення
renderItems();
renderOrders();
