using System;
using System.Data;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormOrders : Form
    {
        public FormOrders()
        {
            InitializeComponent();
            this.Text = "Оформлення та облік замовлень";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            try
            {
                // Завантаження допоміжних списків для ComboBox-ів
                this.customerTableAdapter.Fill(this.coffeeshopdbDataSet.customer);
                this.baristaTableAdapter.Fill(this.coffeeshopdbDataSet.barista);
                this.coffeeshopTableAdapter.Fill(this.coffeeshopdbDataSet.coffeeshop);
                this.coffeedrinkTableAdapter.Fill(this.coffeeshopdbDataSet.coffeedrink);

                // Завантаження замовлень та позицій
                this.orderTableAdapter.Fill(this.coffeeshopdbDataSet._order);
                this.orderitemTableAdapter.Fill(this.coffeeshopdbDataSet.orderitem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Додавання нового замовлення (очищення полів та підготовка нового рядка)
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                this.orderBindingSource.AddNew();
                
                // Встановлюємо значення за замовчуванням для нового замовлення
                DataRowView currentOrder = (DataRowView)orderBindingSource.Current;
                currentOrder["order_date"] = DateTime.Now;
                currentOrder["total_amount"] = 0.00;
                
                // За замовчуванням обираємо перший елемент у списках
                if (cbBarista.Items.Count > 0) cbBarista.SelectedIndex = 0;
                if (cbShop.Items.Count > 0) cbShop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка створення замовлення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обчислення загальної суми замовлення на основі доданих напоїв
        private void btnCalculateTotal_Click(object sender, EventArgs e)
        {
            if (orderBindingSource.Current == null) return;

            decimal total = 0;
            
            // Проходимо по рядкам GridView позицій поточного замовлення
            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.IsNewRow) continue;

                // Отримуємо кількість та ціну продажу
                int quantity = 0;
                decimal price = 0;

                if (row.Cells["colQuantity"].Value != null)
                    int.TryParse(row.Cells["colQuantity"].Value.ToString(), out quantity);

                if (row.Cells["colPrice"].Value != null)
                    decimal.TryParse(row.Cells["colPrice"].Value.ToString(), out price);

                total += quantity * price;
            }

            // Записуємо суму в поточне замовлення
            DataRowView currentOrder = (DataRowView)orderBindingSource.Current;
            currentOrder["total_amount"] = total;
            
            this.Validate();
            orderBindingSource.EndEdit();
            
            lblTotalDisplay.Text = total.ToString("F2") + " грн";
        }

        // Збереження замовлення та позицій у базу даних
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                
                // Зберігаємо поточний стан у DataSet
                this.orderitemBindingSource.EndEdit();
                this.orderBindingSource.EndEdit();

                // Автоматично розраховуємо суму перед збереженням
                btnCalculateTotal_Click(null, null);

                // Оновлюємо спочатку батьківську таблицю (order), потім дочірню (orderitem)
                this.orderTableAdapter.Update(this.coffeeshopdbDataSet._order);
                this.orderitemTableAdapter.Update(this.coffeeshopdbDataSet.orderitem);

                MessageBox.Show("Замовлення успішно збережено в базі даних!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження замовлення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Автоматичне підставлення ціни напою з меню при виборі напою в DataGridView
        private void dgvOrderItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Якщо змінили колонку з напоєм
            if (e.RowIndex >= 0 && dgvOrderItems.Columns[e.ColumnIndex].Name == "colDrink")
            {
                object drinkIdVal = dgvOrderItems.Rows[e.RowIndex].Cells["colDrink"].Value;
                if (drinkIdVal != DBNull.Value && drinkIdVal != null)
                {
                    int drinkId = (int)drinkIdVal;
                    
                    // Шукаємо ціну напою в таблиці coffeedrink DataSet-у
                    DataRow drinkRow = this.coffeeshopdbDataSet.coffeedrink.FindBydrink_id(drinkId);
                    if (drinkRow != null)
                    {
                        decimal price = (decimal)drinkRow["price"];
                        dgvOrderItems.Rows[e.RowIndex].Cells["colPrice"].Value = price;
                        
                        // За замовчуванням кількість встановлюємо = 1
                        if (dgvOrderItems.Rows[e.RowIndex].Cells["colQuantity"].Value == DBNull.Value || dgvOrderItems.Rows[e.RowIndex].Cells["colQuantity"].Value == null)
                        {
                            dgvOrderItems.Rows[e.RowIndex].Cells["colQuantity"].Value = 1;
                        }
                    }
                }
            }
        }
    }
}
