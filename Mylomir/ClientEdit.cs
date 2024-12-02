using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mylomir
{
    public partial class ClientEdit : Form
    {
        public ClientEdit(string id)
        {
            InitializeComponent();
            idClient = id;
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"SELECT 
            * FROM client WHERE ClientID = '{id}'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                maskedTextBox1.Text = reader[4].ToString();
            }
            connection.Close();
        }

        string con = Data.con;
        string idClient;
       

        private void button3_Click(object sender, EventArgs e)
        {
            ClientsTable table = new ClientsTable();
            this.Visible = false;
            table.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1).ToLower();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1).ToLower();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = char.ToUpper(textBox3.Text[0]) + textBox3.Text.Substring(1).ToLower();
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Name(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Name(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Name(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ClientsTable table = new ClientsTable();
            this.Visible = false;
            table.ShowDialog();
            this.Close();
        }
        private void EditData(string id)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(con);
                connection.Open();

                MySqlCommand command = new MySqlCommand($@"UPDATE client SET
                ClientSurname ='{textBox1.Text}', ClientName = '{textBox2.Text}', 
                ClientPatronumic = '{textBox3.Text}',
                ClientNumberPhone = '{maskedTextBox1.Text}' WHERE ClientID = '{id}';", connection);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Запись отредактирована!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Ошибка редактирования! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditData(idClient);
        }

        private void ClientEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
