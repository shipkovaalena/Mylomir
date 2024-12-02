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
    public partial class UsersEdit : Form
    {
        public UsersEdit(string id)
        {
            InitializeComponent();

            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"SELECT 
            * FROM user WHERE UserID = '{id}'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                textBox4.Text = reader[4].ToString();
                textBox5.Text = reader[5].ToString();
            }
            switch (reader[6].ToString())
            {
                case "1":
                    comboBox1.Text = "Администратор";
                    break;
                case "2":
                    comboBox1.Text = "Продавец";
                    break;
            }
            connection.Close();
            idUsr = id;
        }
        string con = Data.con;
        string idUsr;
       
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
            if (!Validation.RusLetter(e.KeyChar))
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = Generation.GenerationLogin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = Generation.GenerationPassword();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UsersTable table = new UsersTable();
            this.Visible = false;
            table.ShowDialog();
            this.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Password(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UsersEdit_Load(object sender, EventArgs e)
        {
            FillRole();
        }

        private void EditData(string id)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string category = comboBox1.SelectedItem.ToString();
                string password = GetHash.GetHashPassword(textBox5.Text);
                int idRole = 0;
                switch (category)
                {
                    case "Администратор":
                        idRole = 1;
                        break;
                    case "Продавец":
                        idRole = 2;
                        break;
                }
                MySqlConnection connection = new MySqlConnection(con);
                connection.Open();

                MySqlCommand command = new MySqlCommand($@"UPDATE user SET
                UserSurname ='{textBox1.Text}', UserName = '{textBox2.Text}', 
                UserPatronymic = '{textBox3.Text}', 
                UserLogin = '{textBox4.Text}', 
                UserPassword = '{textBox5.Text}', 
                UserRole = '{idRole}' WHERE UserID = '{idUsr}';", connection);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Запись отредактирована!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Ошибка редактирования! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillRole()
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM role", connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(1));
            }
            //comboBox1.SelectedIndex = ;
            connection.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1).ToLower();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1).ToLower();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = char.ToUpper(textBox3.Text[0]) + textBox3.Text.Substring(1).ToLower();
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }
    }
}
