using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormAnalytics : Form
    {
        // Рядок підключення до локального SQL Server
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=coffeeshopdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public FormAnalytics()
        {
            InitializeComponent();
            this.Text = "Аналітична звітність та OLAP-запити";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormAnalytics_Load(object sender, EventArgs e)
        {
            // Ініціалізація вибору запитів
            cbQuerySelect.Items.Add("1. Виручка за філіалами та напоями (ROLLUP)");
            cbQuerySelect.Items.Add("2. Рейтинг бариста за обсягом продажів (RANK/DENSE_RANK)");
            cbQuerySelect.Items.Add("3. Аналіз популярності продуктів та локацій (CUBE)");
            cbQuerySelect.SelectedIndex = 0;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string sqlQuery = "";

            // Обираємо запит відповідно до вибору користувача
            switch (cbQuerySelect.SelectedIndex)
            {
                case 0:
                    // ROLLUP: Виручка з ієрархічним підсумком (Філіал -> Напій -> Всього по мережі)
                    sqlQuery = @"
                        select 
                            coalesce(cs.name, N'всього по мережі') as [філіал],
                            coalesce(cd.name, N'всі напої') as [напій],
                            sum(oi.quantity) as [продано порцій],
                            sum(oi.quantity * oi.price_at_sale) as [виручка (грн)]
                        from coffeeshop cs
                        join [order] o on cs.shop_id = o.shop_id
                        join orderitem oi on o.order_id = oi.order_id
                        join coffeedrink cd on oi.drink_id = cd.drink_id
                        group by rollup(cs.name, cd.name);";
                    break;

                case 1:
                    // WINDOW FUNCTIONS: Ранжування бариста за сумою продажів у мережі та всередині філіалу
                    sqlQuery = @"
                        select 
                            b.name as [імя бариста],
                            cs.name as [філіал],
                            sum(o.total_amount) as [загальна сума продажів],
                            rank() over (order by sum(o.total_amount) desc) as [загальний рейтинг],
                            dense_rank() over (partition by cs.shop_id order by sum(o.total_amount) desc) as [рейтинг у філіалі]
                        from barista b
                        join coffeeshop cs on b.shop_id = cs.shop_id
                        join [order] o on b.barista_id = o.barista_id
                        group by b.name, cs.name, cs.shop_id;";
                    break;

                case 2:
                    // CUBE: Багатовимірний аналіз перетину популярності напоїв по всіх комбінаціях філіалів
                    sqlQuery = @"
                        select 
                            coalesce(cs.name, N'усі філіали') as [філіал],
                            coalesce(cd.name, N'усі продукти') as [напій/десерт],
                            sum(oi.quantity) as [кількість проданих одиниць]
                        from coffeeshop cs
                        join [order] o on cs.shop_id = o.shop_id
                        join orderitem oi on o.order_id = oi.order_id
                        join coffeedrink cd on oi.drink_id = cd.drink_id
                        group by cube(cs.name, cd.name);";
                    break;
            }

            // Виконуємо запит у базі даних
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            
                            // Виводимо дані у сітку DataGridView
                            dgvResults.DataSource = dt;
                            lblStatus.Text = $"Успішно завантажено рядків: {dt.Rows.Count}. Час: {DateTime.Now.ToLongTimeString()}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка виконання аналітичного запиту: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
