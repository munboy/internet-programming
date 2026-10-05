using System;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class FormBaristas : Form
    {
        public FormBaristas()
        {
            InitializeComponent();
            this.Text = "Керування співробітниками (Бариста)";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormBaristas_Load(object sender, EventArgs e)
        {
            try
            {
                // Завантажуємо список кав'ярень для ComboBox (зовнішній ключ)
                this.coffeeshopTableAdapter.Fill(this.coffeeshopdbDataSet.coffeeshop);
                // Завантажуємо список співробітників
                this.baristaTableAdapter.Fill(this.coffeeshopdbDataSet.barista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Збереження змін у БД
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.baristaBindingSource.EndEdit();
                this.baristaTableAdapter.Update(this.coffeeshopdbDataSet.barista);
                MessageBox.Show("Списки співробітників успішно оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник помилок для DataGridView (наприклад, якщо користувач ввів некоректні дані у зв'язану колонку)
        private void dgvBaristas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Помилка введення даних: перевірте правильність заповнення полів.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }
    }
}
