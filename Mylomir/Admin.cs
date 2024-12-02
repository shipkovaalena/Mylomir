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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            //this.label1.Text = name;
        }
        
        private void Admin_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsersTable usr = new UsersTable();
            this.Visible = false;
            usr.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientsTable clients = new ClientsTable();
            this.Visible = false;
            clients.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            this.Visible = false;
            product.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ImportAndCopy importAndCopy = new ImportAndCopy();
            this.Visible = false;
            importAndCopy.ShowDialog();
            this.Close();
        }
    }
}
