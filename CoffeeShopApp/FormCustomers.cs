using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormCustomers : Form
    {
        // Строка подключения к вашему SQL Server
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=coffeeshopdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // Объекты для работы с БД
        private SqlDataAdapter customerAdapter;
        private SqlDataAdapter cardAdapter;
        private DataTable dtCustomers;
        private DataTable dtCards;

        public FormCustomers()
        {
            InitializeComponent();
            this.Text = "Керування клієнтами та картками лояльності";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormCustomers_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // 1. Загрузка клиентов
                    string customerQuery = "select customer_id, name, phone, email from customer";
                    customerAdapter = new SqlDataAdapter(customerQuery, conn);
                    SqlCommandBuilder customerBuilder = new SqlCommandBuilder(customerAdapter);

                    dtCustomers = new DataTable();
                    customerAdapter.Fill(dtCustomers);
                    dgvCustomers.DataSource = dtCustomers;
                    dgvCustomers.Columns["customer_id"].ReadOnly = true;
                    dgvCustomers.Columns["customer_id"].HeaderText = "ID Клієнта";
                    dgvCustomers.Columns["name"].HeaderText = "ПІБ Клієнта";
                    dgvCustomers.Columns["phone"].HeaderText = "Телефон";
                    dgvCustomers.Columns["email"].HeaderText = "Електронна пошта";

                    // 2. Загрузка карт лояльности
                    string cardQuery = "select card_id, customer_id, bonus_points, issue_date from loyaltycard";
                    cardAdapter = new SqlDataAdapter(cardQuery, conn);
                    SqlCommandBuilder cardBuilder = new SqlCommandBuilder(cardAdapter);

                    dtCards = new DataTable();
                    cardAdapter.Fill(dtCards);
                    dgvCards.DataSource = dtCards;
                    dgvCards.Columns["card_id"].ReadOnly = true;
                    dgvCards.Columns["card_id"].HeaderText = "Номер картки";
                    dgvCards.Columns["customer_id"].HeaderText = "ID Власника";
                    dgvCards.Columns["bonus_points"].HeaderText = "Бонусні бали";
                    dgvCards.Columns["issue_date"].HeaderText = "Дата видачі";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Сохранение изменений в таблице клиентов
        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                customerAdapter.Update(dtCustomers);
                MessageBox.Show("Дані клієнтів успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Перезагружаем данные для отображения автоинкрементных ID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження клієнтів: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Сохранение изменений в таблице карт
        private void btnSaveCards_Click(object sender, EventArgs e)
        {
            try
            {
                cardAdapter.Update(dtCards);
                MessageBox.Show("Дані бонусних карток збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження карток: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка быстрой выдачи карты выбранному в таблице клиенту
        private void btnCreateCard_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("Виберіть клієнта в таблиці зверху!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем ID выделенного клиента
            int customerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells["customer_id"].Value);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Проверяем, нет ли уже карты у этого клиента
                    string checkQuery = "select count(*) from loyaltycard where customer_id = @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", customerId);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("У цього клієнта вже є бонусна картка!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Добавляем новую карту
                    string insertQuery = "insert into loyaltycard (customer_id, bonus_points, issue_date) values (@cust_id, 0, cast(getdate() as date))";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@cust_id", customerId);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Бонусну картку успішно активовано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Обновляем таблицы
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка створення картки: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
