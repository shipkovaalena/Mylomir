using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mylomir
{
    public partial class Salesman : Form
    {
        public Salesman()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientsTable client = new ClientsTable();
            this.Visible = false;
            client.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            this.Visible = false;
            products.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrdersTable ord = new OrdersTable();
            this.Visible = false;
            ord.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Visible = false;
            main.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
