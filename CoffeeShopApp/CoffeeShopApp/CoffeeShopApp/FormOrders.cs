using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormOrders : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=coffeeshopdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        private DataTable dtDrinks;
        private decimal orderTotal = 0;

        public FormOrders()
        {
            InitializeComponent();
            this.Text = "Оформлення та облік замовлень";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            LoadComboboxes();
            ConfigureOrderItemsGrid();
        }

        // Загрузка списков клиентов, бариста и кав'ярень в ComboBox
        private void LoadComboboxes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Клиенты (включая вариант "Анонимный гость")
                    SqlDataAdapter custAdapter = new SqlDataAdapter("select customer_id, name from customer", conn);
                    DataTable dtCustomers = new DataTable();
                    custAdapter.Fill(dtCustomers);

                    DataRow guestRow = dtCustomers.NewRow();
                    guestRow["customer_id"] = DBNull.Value;
                    guestRow["name"] = "-- Гість (без картки) --";
                    dtCustomers.Rows.InsertAt(guestRow, 0);

                    cbCustomer.DataSource = dtCustomers;
                    cbCustomer.DisplayMember = "name";
                    cbCustomer.ValueMember = "customer_id";

                    // 2. Бариста
                    SqlDataAdapter baristaAdapter = new SqlDataAdapter("select barista_id, name from barista", conn);
                    DataTable dtBaristas = new DataTable();
                    baristaAdapter.Fill(dtBaristas);
                    cbBarista.DataSource = dtBaristas;
                    cbBarista.DisplayMember = "name";
                    cbBarista.ValueMember = "barista_id";

                    // 3. Филиал кав'ярні
                    SqlDataAdapter shopAdapter = new SqlDataAdapter("select shop_id, name from coffeeshop", conn);
                    DataTable dtShops = new DataTable();
                    shopAdapter.Fill(dtShops);
                    cbShop.DataSource = dtShops;
                    cbShop.DisplayMember = "name";
                    cbShop.ValueMember = "shop_id";

                    // 4. Меню напитков (сохраняем локально для сетки GridView)
                    SqlDataAdapter drinkAdapter = new SqlDataAdapter("select drink_id, name, price from coffeedrink", conn);
                    dtDrinks = new DataTable();
                    drinkAdapter.Fill(dtDrinks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка ініціалізації списків: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Первоначальная настройка колонок DataGridView состава заказа
        private void ConfigureOrderItemsGrid()
        {
            dgvOrderItems.Columns.Clear();
            dgvOrderItems.AutoGenerateColumns = false;

            // 1. Колонка выбора напитка (ComboBoxColumn)
            DataGridViewComboBoxColumn colDrink = new DataGridViewComboBoxColumn();
            colDrink.Name = "colDrink";
            colDrink.HeaderText = "Напій / Продукт";
            colDrink.DataSource = dtDrinks;
            colDrink.DisplayMember = "name";
            colDrink.ValueMember = "drink_id";
            colDrink.Width = 220;
            colDrink.FlatStyle = FlatStyle.Flat;
            dgvOrderItems.Columns.Add(colDrink);

            // 2. Колонка количества (TextBox)
            DataGridViewTextBoxColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.Name = "colQuantity";
            colQuantity.HeaderText = "Кількість";
            colQuantity.Width = 80;
            dgvOrderItems.Columns.Add(colQuantity);

            // 3. Колонка цены (TextBox)
            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.Name = "colPrice";
            colPrice.HeaderText = "Ціна продажу";
            colPrice.ReadOnly = true; // Заполняется автоматически из меню
            colPrice.Width = 110;
            dgvOrderItems.Columns.Add(colPrice);
        }

        // Автозаполнение цены при выборе напитка
        private void dgvOrderItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvOrderItems.Columns[e.ColumnIndex].Name == "colDrink")
            {
                object drinkIdVal = dgvOrderItems.Rows[e.RowIndex].Cells["colDrink"].Value;
                if (drinkIdVal != null && drinkIdVal != DBNull.Value)
                {
                    int drinkId = Convert.ToInt32(drinkIdVal);
                    
                    // Ищем напиток в локальной таблице и берем его базовую цену
                    DataRow[] foundRows = dtDrinks.Select($"drink_id = {drinkId}");
                    if (foundRows.Length > 0)
                    {
                        decimal basePrice = Convert.ToDecimal(foundRows[0]["price"]);
                        dgvOrderItems.Rows[e.RowIndex].Cells["colPrice"].Value = basePrice;

                        // Если количество не заполнено, ставим 1 порцию по умолчанию
                        if (dgvOrderItems.Rows[e.RowIndex].Cells["colQuantity"].Value == null)
                        {
                            dgvOrderItems.Rows[e.RowIndex].Cells["colQuantity"].Value = 1;
                        }
                    }
                }
            }
        }

        // Расчет общей суммы заказа
        private void btnCalculateTotal_Click(object sender, EventArgs e)
        {
            orderTotal = 0;

            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.IsNewRow) continue;

                int qty = 0;
                decimal price = 0;

                if (row.Cells["colQuantity"].Value != null)
                    int.TryParse(row.Cells["colQuantity"].Value.ToString(), out qty);

                if (row.Cells["colPrice"].Value != null)
                    decimal.TryParse(row.Cells["colPrice"].Value.ToString(), out price);

                orderTotal += qty * price;
            }

            lblTotalDisplay.Text = orderTotal.ToString("F2") + " грн";
        }

        // Сохранение заказа в БД (Транзакция Master-Detail на ADO.NET)
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // Сначала пересчитаем сумму
            btnCalculateTotal_Click(null, null);

            if (orderTotal <= 0)
            {
                MessageBox.Show("Додайте хоча б один напій у замовлення!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbBarista.SelectedValue == null || cbShop.SelectedValue == null)
            {
                MessageBox.Show("Виберіть бариста та філіал кав'ярні!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object customerIdVal = cbCustomer.SelectedValue;
            int baristaId = Convert.ToInt32(cbBarista.SelectedValue);
            int shopId = Convert.ToInt32(cbShop.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Вставляем запись в таблицу [order]
                    string orderQuery = @"
                        insert into [order] (order_date, total_amount, customer_id, barista_id, shop_id) 
                        values (getdate(), @total, @cust_id, @barista_id, @shop_id);
                        select scope_identity();";

                    int newOrderId = 0;
                    using (SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction))
                    {
                        orderCmd.Parameters.AddWithValue("@total", orderTotal);
                        orderCmd.Parameters.AddWithValue("@cust_id", customerIdVal ?? DBNull.Value);
                        orderCmd.Parameters.AddWithValue("@barista_id", baristaId);
                        orderCmd.Parameters.AddWithValue("@shop_id", shopId);

                        newOrderId = Convert.ToInt32(orderCmd.ExecuteScalar());
                    }

                    // 2. Вставляем позиции заказа в orderitem
                    string itemQuery = @"
                        insert into orderitem (order_id, drink_id, quantity, price_at_sale) 
                        values (@order_id, @drink_id, @qty, @price);";

                    foreach (DataGridViewRow row in dgvOrderItems.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int drinkId = Convert.ToInt32(row.Cells["colDrink"].Value);
                        int qty = Convert.ToInt32(row.Cells["colQuantity"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["colPrice"].Value);

                        using (SqlCommand itemCmd = new SqlCommand(itemQuery, conn, transaction))
                        {
                            itemCmd.Parameters.AddWithValue("@order_id", newOrderId);
                            itemCmd.Parameters.AddWithValue("@drink_id", drinkId);
                            itemCmd.Parameters.AddWithValue("@qty", qty);
                            itemCmd.Parameters.AddWithValue("@price", price);

                            itemCmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Если клиент постоянный (выбран в списке), начисляем ему 10% бонусов на карту лояльности
                    if (customerIdVal != null && customerIdVal != DBNull.Value)
                    {
                        int customerId = Convert.ToInt32(customerIdVal);
                        int earnedBonuses = (int)Math.Round(orderTotal * 0.10m);

                        string loyaltyQuery = @"
                            update loyaltycard 
                            set bonus_points = bonus_points + @bonuses 
                            where customer_id = @cust_id;";

                        using (SqlCommand loyaltyCmd = new SqlCommand(loyaltyQuery, conn, transaction))
                        {
                            loyaltyCmd.Parameters.AddWithValue("@bonuses", earnedBonuses);
                            loyaltyCmd.Parameters.AddWithValue("@cust_id", customerId);
                            loyaltyCmd.ExecuteNonQuery();
                        }
                    }

                    // Подтверждаем транзакцию
                    transaction.Commit();
                    MessageBox.Show($"Замовлення №{newOrderId} успішно збережено та проведено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Очищаем форму для нового заказа
                    dgvOrderItems.Rows.Clear();
                    lblTotalDisplay.Text = "0.00 грн";
                    orderTotal = 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Помилка оформлення замовлення: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Кнопка очистки / сброса
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            dgvOrderItems.Rows.Clear();
            lblTotalDisplay.Text = "0.00 грн";
            orderTotal = 0;
            if (cbCustomer.Items.Count > 0) cbCustomer.SelectedIndex = 0;
            if (cbBarista.Items.Count > 0) cbBarista.SelectedIndex = 0;
            if (cbShop.Items.Count > 0) cbShop.SelectedIndex = 0;
        }

        // Предотвращение ошибок рендеринга ComboBox в GridView
        private void dgvOrderItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
    }
}
