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
    public partial class ClientAdd : Form
    {
        public ClientAdd()
        {
            InitializeComponent();
        }

        string con = Data.con;
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

        private void button3_Click(object sender, EventArgs e)
        {
            ClientsTable table = new ClientsTable();
            this.Visible = false;
            table.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || maskedTextBox1.Text == "")
            {
                MessageBox.Show("Ошибка! Заполните все поля!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string surname = textBox1.Text;
                string name = textBox2.Text;
                string patronymic = textBox3.Text;
                string numberPhone = maskedTextBox1.Text;

                MySqlConnection connection1 = new MySqlConnection(con);

                connection1.Open();

                MySqlCommand command1 = new MySqlCommand($@"INSERT INTO client 
                                                        (ClientSurname, ClientName, ClientPatronumic, ClientNumberPhone) 
                                                         VALUES ('{surname}', '{name}', '{patronymic}', '{numberPhone}')");

                command1.Connection = connection1;
                command1.ExecuteNonQuery();

                connection1.Close();

                MessageBox.Show("Запись добавлена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                maskedTextBox1.Clear();
            }
        }

        private void ClientAdd_Load(object sender, EventArgs e)
        {

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
    }
}
