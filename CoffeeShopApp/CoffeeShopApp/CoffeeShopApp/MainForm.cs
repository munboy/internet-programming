using System;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Мережа кав'ярень «Coffee Shop» - Панель керування";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            FormCustomers f = new FormCustomers();
            f.ShowDialog();
        }

        private void btnBaristas_Click(object sender, EventArgs e)
        {
            FormBaristas f = new FormBaristas();
            f.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            FormOrders f = new FormOrders();
            f.ShowDialog();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            FormAnalytics f = new FormAnalytics();
            f.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
