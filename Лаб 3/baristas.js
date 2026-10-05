// baristas.js - персонал: генерація таблиці з масиву, сортування за кліком
// на заголовку стовпця, фільтр за філіалом і додавання нового працівника.
// Сортування зроблено за прикладом з методички (таблиця книг і quickSort).

// конструктор запису (як BookItem / fillArray у методичці)
function Barista(id, name, phone, shop, shift, status) {
    this.id = id;
    this.name = name;
    this.phone = phone;
    this.shop = shop;
    this.shift = shift;
    this.status = status;
}

let baristas = [
    new Barista(1, 'Андрій Мельник', '+380 (67) 111-00-11', 'Центральний', 'Ранкова (07:30-15:00)', 'На зміні'),
    new Barista(2, 'Олена Бойко', '+380 (50) 222-00-22', "Кав'ярня на Подолі", 'Денна (10:00-18:00)', 'На зміні'),
    new Barista(3, 'Сергій Ткаченко', '+380 (63) 333-00-33', 'Студентський куточок', 'Повна (08:00-20:00)', 'На зміні'),
    new Barista(4, 'Юлія Кравчук', '+380 (97) 444-00-44', 'Центральний', 'Вечірня (15:00-22:00)', 'Вихідний'),
    new Barista(5, 'Богдан Лисенко', '+380 (66) 555-00-55', "Кав'ярня на Подолі", 'Вечірня (15:00-22:00)', 'Вихідний')
];

let sortField = null; // поле, за яким зараз відсортовано
let sortAsc = true;   // true - за зростанням, false - за спаданням

const tableHead = document.querySelector('#baristas-table thead');
const tableBody = document.getElementById('baristas-tbody');
const filterShop = document.getElementById('filter-shop');
const counter = document.getElementById('baristas-counter');
const baristaForm = document.getElementById('new-barista-form');
const nameInput = document.getElementById('barista-name');
const phoneInput = document.getElementById('barista-phone');
const message = document.getElementById('barista-message');


// ---------- сортування ----------

// порівняння двох записів за полем: числа віднімаємо,
// рядки порівнюємо з урахуванням українського алфавіту
function compare(a, b, field) {
    if (typeof a[field] === 'number') {
        return a[field] - b[field];
    }
    return a[field].localeCompare(b[field], 'uk');
}

// швидке сортування: опорний елемент посередині, менші ліворуч, більші праворуч
function quickSort(list, field) {
    if (list.length <= 1) {
        return list;
    }

    const pivot = list[Math.floor(list.length / 2)];
    const less = [];
    const equal = [];
    const greater = [];

    list.forEach(function (item) {
        const result = compare(item, pivot, field);
        if (result < 0) {
            less.push(item);
        } else if (result > 0) {
            greater.push(item);
        } else {
            equal.push(item);
        }
    });

    return quickSort(less, field).concat(equal, quickSort(greater, field));
}

// як allocator() у методичці: повторний клік по тому ж стовпцю
// не сортує заново, а лише перевертає масив
function sortBy(field) {
    if (field === sortField) {
        baristas.reverse();
        sortAsc = !sortAsc;
    } else {
        baristas = quickSort(baristas, field);
        sortField = field;
        sortAsc = true;
    }
    updateHeaderArrows();
    renderTable();
}

// стрілка ▲ або ▼ біля активного заголовка
function updateHeaderArrows() {
    const headers = tableHead.querySelectorAll('th.sortable');
    headers.forEach(function (th) {
        th.classList.remove('sort-asc', 'sort-desc');
        if (th.dataset.field === sortField) {
            th.classList.add(sortAsc ? 'sort-asc' : 'sort-desc');
        }
    });
}


// ---------- відображення ----------

function createCell(text, className) {
    const td = document.createElement('td');
    td.textContent = text;
    if (className) {
        td.className = className;
    }
    return td;
}

function renderTable() {
    const shop = filterShop.value;
    tableBody.innerHTML = '';

    const visible = baristas.filter(function (barista) {
        return shop === 'all' || barista.shop === shop;
    });

    visible.forEach(function (barista) {
        const tr = document.createElement('tr');
        tr.appendChild(createCell(barista.id, 'center'));

        const tdName = document.createElement('td');
        const bold = document.createElement('b');
        bold.textContent = barista.name;
        tdName.appendChild(bold);
        tr.appendChild(tdName);

        tr.appendChild(createCell(barista.phone));
        tr.appendChild(createCell(barista.shop));
        tr.appendChild(createCell(barista.shift));

        const tdStatus = document.createElement('td');
        const status = document.createElement('span');
        status.className = barista.status === 'На зміні' ? 'status-ok' : 'status-off';
        status.textContent = barista.status;
        tdStatus.appendChild(status);
        tr.appendChild(tdStatus);

        tableBody.appendChild(tr);
    });

    counter.textContent = 'Показано ' + visible.length + ' з ' + baristas.length;
}


// ---------- обробники подій ----------

// один обробник на всю шапку таблиці
tableHead.addEventListener('click', function (event) {
    const th = event.target.closest('th');
    if (th && th.dataset.field) {
        sortBy(th.dataset.field);
    }
});

filterShop.addEventListener('change', renderTable);

baristaForm.addEventListener('submit', function (event) {
    event.preventDefault();

    const name = nameInput.value.trim().replace(/\s+/g, ' ');
    const digits = phoneInput.value.replace(/\D/g, '');
    const shop = document.getElementById('barista-shop').value;
    const shift = document.getElementById('barista-shift').value;

    nameInput.classList.remove('invalid');
    phoneInput.classList.remove('invalid');

    if (name.split(' ').length < 2) {
        nameInput.classList.add('invalid');
        message.className = 'form-message error';
        message.textContent = 'Вкажіть прізвище та ім\'я працівника.';
        return;
    }

    if (!/^380\d{9}$/.test(digits)) {
        phoneInput.classList.add('invalid');
        message.className = 'form-message error';
        message.textContent = 'Телефон має бути у форматі +380XXXXXXXXX.';
        return;
    }

    const phone = '+380 (' + digits.substring(3, 5) + ') ' + digits.substring(5, 8) + '-' +
        digits.substring(8, 10) + '-' + digits.substring(10);

    let maxId = 0;
    baristas.forEach(function (barista) {
        if (barista.id > maxId) {
            maxId = barista.id;
        }
    });

    baristas.push(new Barista(maxId + 1, name, phone, shop, shift, 'На зміні'));

    // якщо таблицю вже відсортовано, новий запис стає на своє місце
    if (sortField !== null) {
        baristas = quickSort(baristas, sortField);
        if (!sortAsc) {
            baristas.reverse();
        }
    }

    baristaForm.reset();
    renderTable();
    message.className = 'form-message success';
    message.textContent = 'До штату філіалу «' + shop + '» додано: ' + name + '.';
});


renderTable();
