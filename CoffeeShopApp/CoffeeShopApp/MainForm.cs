using System;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Управління мережею кав'ярень «Coffee Shop»";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            FormCustomers frm = new FormCustomers();
            frm.ShowDialog();
        }

        private void btnBaristas_Click(object sender, EventArgs e)
        {
            FormBaristas frm = new FormBaristas();
            frm.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            FormOrders frm = new FormOrders();
            frm.ShowDialog();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            FormAnalytics frm = new FormAnalytics();
            frm.ShowDialog();
        }
    }
}
