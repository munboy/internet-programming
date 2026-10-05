// analytics.js - перемикання аналітичних звітів без перезавантаження сторінки.
// Показується лише вибраний звіт, інші ховаються класом .hidden.

const reportSelect = document.getElementById('query-select');
const runButton = document.getElementById('btn-run-query');
const statusLine = document.getElementById('report-status');
const reports = document.querySelectorAll('.report');

function showReport() {
    const selected = 'section-' + reportSelect.value; // наприклад, section-rank
    let rowsCount = 0;

    reports.forEach(function (section) {
        const isSelected = section.id === selected;
        // toggle з другим аргументом: true - додати клас, false - прибрати
        section.classList.toggle('hidden', !isSelected);
        if (isSelected) {
            rowsCount = section.querySelectorAll('tbody tr').length;
        }
    });

    // назва звіту без номера на початку ("2. Рейтинг..." -> "Рейтинг...")
    const title = reportSelect.options[reportSelect.selectedIndex].text.replace(/^\d+\.\s*/, '');
    const time = new Date().toLocaleTimeString('uk-UA');
    statusLine.textContent = 'Сформовано звіт «' + title + '». Рядків: ' + rowsCount + '. Час: ' + time + '.';
}

reportSelect.addEventListener('change', showReport);
runButton.addEventListener('click', showReport);

showReport();
