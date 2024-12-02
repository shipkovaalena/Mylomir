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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        string conn = Data.con;
        public string userLogin;
        int count = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text;
                string password = textBox2.Text;

                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                mySqlConnection.Open();

                MySqlCommand mySqlCommand = new MySqlCommand($"SELECT * FROM user WHERE UserLogin = '{login}'", mySqlConnection);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                string paswBd = dataTable.Rows[0].ItemArray.GetValue(5).ToString();
                Data.role = dataTable.Rows[0].ItemArray.GetValue(6).ToString();
                //string name = $"{dataTable.Rows[0].ItemArray.GetValue(1)} {dataTable.Rows[0].ItemArray.GetValue(2)} {dataTable.Rows[0].ItemArray.GetValue(3)}";
                //Data.UserFIO = name;
                if (password == paswBd)
                {
                    MessageBox.Show("Успешная авторизация!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    switch (Data.role)
                    {
                        case "1":
                            this.Visible = false;
                            Admin userForm = new Admin();
                            userForm.ShowDialog();
                            this.Close();
                            break;
                        case "2":
                            this.Visible = false;
                            Salesman salesman = new Salesman();
                            salesman.ShowDialog();
                            this.Close();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации! Неправильный логин или пароль.", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                }
                mySqlConnection.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }  

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char letter = e.KeyChar;
            if (!((letter >= 'A' && letter <= 'z') || letter == 15 || Char.IsDigit(letter)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char letter = e.KeyChar;
            if (!((letter >= 'A' && letter <= 'z') || letter == 10 || Char.IsDigit(letter)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {         
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
    
    
