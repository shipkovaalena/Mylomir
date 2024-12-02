using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mylomir
{
    public partial class UsersAdd : Form
    {
        public UsersAdd()
        {
            InitializeComponent();
        }

        string con = Data.con;
        private void button3_Click(object sender, EventArgs e)
        {
            UsersTable users = new UsersTable();
            this.Visible = false;
            users.ShowDialog();
            this.Close();
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Login(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Password(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UsersAdd_Load(object sender, EventArgs e)
        { 
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM role", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(1));
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = Generation.GenerationLogin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = Generation.GenerationPassword();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == " " || comboBox1.Text == " ")
            {
                MessageBox.Show("Ошибка! Заполните все поля!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string surname = textBox1.Text;
                string name = textBox2.Text;
                string patronumic = textBox3.Text;
                string login = textBox4.Text;
                string role = comboBox1.SelectedItem.ToString();
                int idRole = 0;
                //string password = GetHash.GetHashPassword(textBox5.Text);
                string password = textBox5.Text;
                switch (role)
                {
                    case "Администратор":
                        idRole = 1;
                        break;
                    case "Продавец":
                        idRole = 2;
                        break;
                }


                MySqlConnection connection1 = new MySqlConnection(con);

                connection1.Open();

                MySqlCommand command1 = new MySqlCommand($@"INSERT INTO user 
                                                        (UserSurname, UserName, UserPatronymic, UserLogin, UserPassword, UserRole) 
                                                         VALUES ('{surname}', '{name}', '{patronumic}', '{login}', '{password}', '{idRole}')");

                command1.Connection = connection1;
                command1.ExecuteNonQuery();

                connection1.Close();

                MessageBox.Show("Запись добавлена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                comboBox1.SelectedIndex = -1;
            }
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
