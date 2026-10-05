using System;
using System.Data;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormCustomers : Form
    {
        public FormCustomers()
        {
            InitializeComponent();
            this.Text = "Керування клієнтами та картками лояльності";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            try
            {
                // Завантаження даних із бази даних у DataSet
                this.customerTableAdapter.Fill(this.coffeeshopdbDataSet.customer);
                this.loyaltycardTableAdapter.Fill(this.coffeeshopdbDataSet.loyaltycard);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник збереження змін у таблиці клієнтів
        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.customerBindingSource.EndEdit();
                this.customerTableAdapter.Update(this.coffeeshopdbDataSet.customer);
                MessageBox.Show("Дані клієнтів успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження даних клієнтів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник збереження змін у таблиці карток лояльності
        private void btnSaveCards_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.loyaltycardBindingSource.EndEdit();
                this.loyaltycardTableAdapter.Update(this.coffeeshopdbDataSet.loyaltycard);
                MessageBox.Show("Дані карток лояльності успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження даних карток: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для автоматичного створення картки лояльності для виділеного клієнта
        private void btnCreateCard_Click(object sender, EventArgs e)
        {
            if (customerBindingSource.Current == null) return;

            DataRowView currentCustomer = (DataRowView)customerBindingSource.Current;
            int customerId = (int)currentCustomer["customer_id"];

            // Перевіряємо, чи немає вже картки у цього клієнта
            bool hasCard = false;
            foreach (DataRow row in this.coffeeshopdbDataSet.loyaltycard.Rows)
            {
                if (row.RowState != DataRowState.Deleted && (int)row["customer_id"] == customerId)
                {
                    hasCard = true;
                    break;
                }
            }

            if (hasCard)
            {
                MessageBox.Show("У цього клієнта вже є бонусна картка!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Додаємо новий рядок у таблицю loyaltycard
                DataRow newCardRow = this.coffeeshopdbDataSet.loyaltycard.NewRow();
                newCardRow["customer_id"] = customerId;
                newCardRow["bonus_points"] = 0; // Початкові бонуси
                newCardRow["issue_date"] = DateTime.Today;

                this.coffeeshopdbDataSet.loyaltycard.Rows.Add(newCardRow);
                this.loyaltycardTableAdapter.Update(this.coffeeshopdbDataSet.loyaltycard);
                
                MessageBox.Show("Картку лояльності успішно створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка створення картки: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
