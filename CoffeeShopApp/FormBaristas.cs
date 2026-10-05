using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormBaristas : Form
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=coffeeshopdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        private SqlDataAdapter baristaAdapter;
        private DataTable dtBaristas;
        private DataTable dtShops;

        public FormBaristas()
        {
            InitializeComponent();
            this.Text = "Керування співробітниками (Бариста)";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormBaristas_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // 1. Загружаем список кав'ярень для ComboBox колонки
                    string shopQuery = "select shop_id, name from coffeeshop";
                    SqlDataAdapter shopAdapter = new SqlDataAdapter(shopQuery, conn);
                    dtShops = new DataTable();
                    shopAdapter.Fill(dtShops);

                    // 2. Загружаем список бариста
                    string baristaQuery = "select barista_id, name, phone, shop_id from barista";
                    baristaAdapter = new SqlDataAdapter(baristaQuery, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(baristaAdapter);

                    dtBaristas = new DataTable();
                    baristaAdapter.Fill(dtBaristas);

                    // Очищаем существующие колонки перед настройкой
                    dgvBaristas.Columns.Clear();
                    dgvBaristas.DataSource = dtBaristas;

                    // Настройка отображения полей
                    dgvBaristas.Columns["barista_id"].ReadOnly = true;
                    dgvBaristas.Columns["barista_id"].HeaderText = "ID Співробітника";
                    dgvBaristas.Columns["name"].HeaderText = "ПІБ Бариста";
                    dgvBaristas.Columns["phone"].HeaderText = "Номер телефону";

                    // Скрываем обычную колонку shop_id и заменяем её выпадающим списком
                    dgvBaristas.Columns["shop_id"].Visible = false;

                    DataGridViewComboBoxColumn colShop = new DataGridViewComboBoxColumn();
                    colShop.Name = "colShop";
                    colShop.HeaderText = "Філіал кав'ярні";
                    colShop.DataSource = dtShops;
                    colShop.DisplayMember = "name";       // Что показывать (название кав'ярні)
                    colShop.ValueMember = "shop_id";      // Что сохранять в БД (ID кав'ярні)
                    colShop.DataPropertyName = "shop_id"; // Связываем с колонкой shop_id таблицы barista
                    colShop.FlatStyle = FlatStyle.Flat;

                    dgvBaristas.Columns.Add(colShop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка сохранения изменений в БД
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                baristaAdapter.Update(dtBaristas);
                MessageBox.Show("Дані співробітників успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження даних: " + ex.Message, "Помилка СУБД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Предотвращение вылета приложения при ошибках ввода в DataGridView
        private void dgvBaristas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Помилка введення даних: перевірте правильність заповнення полів.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }
    }
}
