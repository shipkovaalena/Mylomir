﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;


namespace Mylomir
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadImages();
            GenerateCaptcha();
        }
        string conn = Data.con;
        public string userLogin;
        //int count = 1;
        List<Image> captchaImages = new List<Image>();
        Random random = new Random();

        private void Autorization()
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

                string loginAdmin = Properties.Settings.Default["loginAdmin"].ToString();
                string passwordAdmin = Properties.Settings.Default["passwordAdmin"].ToString();

                if (login == loginAdmin && password == passwordAdmin)
                {
                    ImportAndCopy admin = new ImportAndCopy();
                    this.Visible = false;
                    admin.ShowDialog();
                    this.Close();
                }

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
                    this.Width = 885;
                    pictureBox1.Enabled = false;
                    pictureBox3.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    checkBox1.Enabled = false;
                    button1.Enabled = false;
                    
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка авторизации!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Width = 885;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                pictureBox1.Enabled = false;
                pictureBox3.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                checkBox1.Enabled = false;
                button1.Enabled = false;                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Autorization();
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
            this.Width = 485;
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

        private void LoadImages()
        {
            string pathFile = Directory.GetCurrentDirectory();

            captchaImages.Add(Image.FromFile($@"{pathFile}" + $@"\photos\4GC5.png"));
            captchaImages.Add(Image.FromFile($@"{pathFile}" + $@"\photos\GFY6.png"));
            captchaImages.Add(Image.FromFile($@"{pathFile}" + $@"\photos\QW12.png"));
            captchaImages.Add(Image.FromFile($@"{pathFile}" + $@"\photos\TH58.png"));            
        }
        private void GenerateCaptcha()
        {
            if (captchaImages.Count > 0)
            {
                int index = random.Next(captchaImages.Count);
                pictureBox2.Image = captchaImages[index];
            }
        }
         private bool CheckCaptcha(string answer)
        {
            if (answer == "4GC5" || answer == "GFY6" || answer == "QW12" || answer == "TH58")
            {
                return true;
            }
            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            bool answer = CheckCaptcha(textBox3.Text);
            if (answer)
            {
                this.Width = 485;
                pictureBox1.Enabled = true;
                pictureBox3.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                checkBox1.Enabled = true;
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                GenerateCaptcha();
                Thread.Sleep(10000);
            }          
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
    
    
