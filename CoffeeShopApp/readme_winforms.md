# Руководство по созданию C# WinForms приложения «Сеть кофеен» (Без использования бажного мастера Visual Studio)

Поскольку в Visual Studio 2022 есть критический баг при работе с визуальным мастером баз данных (ошибка загрузки сборки `Microsoft.Data.SqlClient`), мы реализуем подключение к базе данных **динамически через код (чистый ADO.NET)**. 

Это **на 100% защищает от ошибок среды разработки**, не требует настройки источников данных (Data Sources) и работает намного надежнее.

---

## Шаг 1. Создание проекта в Visual Studio

1. Откройте **Visual Studio**.
2. Выберите **Create a new project (Создать новый проект)**.
3. Введите в поиске **Windows Forms** и выберите шаблон **Windows Forms App (.NET Framework)** на языке C#.
4. Назовите проект **CoffeeShopApp**, выберите версию **.NET Framework 4.7.2** (или 4.8) и нажмите **Create (Создать)**.

---

## Шаг 2. Разметка интерфейса форм (Визуальный конструктор)

Вам нужно просто перетащить элементы управления из панели **Toolbox (Панель элементов)** на формы.

### 1. Главная форма (`MainForm`):
* Добавьте 4 кнопки (`Button`): `btnCustomers` (Клиенты), `btnBaristas` (Бариста), `btnOrders` (Заказы), `btnAnalytics` (Аналитика).

### 2. Форма клиентов (`FormCustomers`):
* Добавьте вкладки (`TabControl`) или просто две таблицы на форму:
  * `dgvCustomers` (DataGridView для клиентов)
  * `dgvCards` (DataGridView для карт лояльности)
* Добавьте кнопки:
  * `btnSaveCustomer` (Сохранить клиентов)
  * `btnSaveCards` (Сохранить карты)
  * `btnCreateCard` (Выдать бонусную карту выбранному клиенту)

### 3. Форма бариста (`FormBaristas`):
* Добавьте `dgvBaristas` (DataGridView для списка сотрудников).
* Добавьте кнопку `btnSave` (Сохранить изменения).

### 4. Форма заказов (`FormOrders`):
* Разместите элементы для шапки заказа:
  * `cbCustomer` (ComboBox — выбор клиента)
  * `cbBarista` (ComboBox — выбор бариста)
  * `cbShop` (ComboBox — выбор филиала кав'ярні)
  * `btnCreateOrder` (Кнопка: Создать чек)
* Разместите элементы для позиций заказа (Master-Detail):
  * `dgvOrderItems` (DataGridView — таблица состава заказа)
  * `btnCalculateTotal` (Кнопка: Посчитать сумму)
  * `lblTotal` (Label — для отображения суммы)
  * `btnSaveOrder` (Кнопка: Оформить покупку)

### 5. Форма аналитики (`FormAnalytics`):
* Добавьте `cbQuerySelect` (ComboBox — выбор аналитического запроса).
* Добавьте кнопку `btnExecute` (Выполнить запрос).
* Добавьте `dgvResults` (DataGridView — сетка результатов).
* Добавьте `lblStatus` (Label — статус выполнения).

---

## Шаг 3. Вставка C# кода в формы

Откройте код каждой формы (нажав на форму правой кнопкой мыши $\rightarrow$ **View Code (Просмотреть код)** или клавишу **F7**) и замените весь текст кода на код из наших файлов:
* [MainForm.cs](file:///c:/Users/User/Desktop/Универ/Бази%20даних/Курсовая/CoffeeShopApp/MainForm.cs)
* [FormCustomers.cs](file:///c:/Users/User/Desktop/Универ/Бази%20даних/Курсовая/CoffeeShopApp/FormCustomers.cs)
* [FormBaristas.cs](file:///c:/Users/User/Desktop/Универ/Бази%20даних/Курсовая/CoffeeShopApp/FormBaristas.cs)
* [FormOrders.cs](file:///c:/Users/User/Desktop/Универ/Бази%20даних/Курсовая/CoffeeShopApp/FormOrders.cs)
* [FormAnalytics.cs](file:///c:/Users/User/Desktop/Универ/Бази%20даних/Курсовая/CoffeeShopApp/FormAnalytics.cs)

*После переноса кода запустите проект (клавиша **F5**), и приложение сразу же подключится к вашей базе данных `.\SQLEXPRESS`!*
