using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Mylomir
{
    public partial class ProductAdd : Form
    {
        public ProductAdd()
        {
            InitializeComponent();
        }
        public string photoPath;
        public string fullPath;
        string con = Data.con;
        private void ProductAdd_Load(object sender, EventArgs e)
        {
            MySqlConnection connection1 = new MySqlConnection(con);
            connection1.Open();
            MySqlCommand command1 = new MySqlCommand("SELECT * FROM category", connection1);
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox1.Items.Add(reader1.GetValue(1).ToString());
            }
            connection1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    if ((fileInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase)) &&
                        fileInfo.Length <= 2 * 1024 * 1024)
                    {
                        pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                        textBox6.Text = fileInfo.Name;
                        fullPath = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Выберите файл JPG или PNG размером не более 2 Мб.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox7.Text == "" || textBox4.Text == "" || textBox5.Text == "" ||  textBox5.Text == " " || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Ошибка! Заполните все поля!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string art = textBox1.Text;
                string name = textBox7.Text;
                string desc = textBox4.Text;
                string stock = textBox5.Text;
                //string category = comboBox1.Text.ToString();
                string cost = textBox3.Text;
                string weight = textBox2.Text;
                string photo = textBox6.Text;
                string category = comboBox1.SelectedItem.ToString();
                int idCategory = 0;
                switch (category)
                {
                    case "Увлажняющее":
                        idCategory = 1;
                        break;
                    case "Против акне":
                        idCategory = 2;
                        break;
                    case "Успокаивающее":
                        idCategory = 3;
                        break;
                    case "Заживляющее":
                        idCategory = 4;
                        break;
                    case "Скрабирующее":
                        idCategory = 5;
                        break;
                }


                MySqlConnection connection1 = new MySqlConnection(con);

                connection1.Open();

                MySqlCommand command1 = new MySqlCommand($@"INSERT INTO product 
                                                        (ProductArticleNumber, ProductName, ProductDescription, ProductPhoto, ProductCost, ProductQuantityInStock, ProductCategory, ProductWight) 
                                                         VALUES ('{art}', '{name}', '{desc}', '{photo}', '{cost}', '{stock}', '{idCategory}', '{weight}')");

                command1.Connection = connection1;
                command1.ExecuteNonQuery();

                connection1.Close();

                MessageBox.Show("Запись добавлена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox7.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox3.Clear();
                textBox2.Clear();
                textBox6.Clear();
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = Generation.GenerationArticul();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                textBox6.Text = "";
                pictureBox1.ImageLocation = $@"./photos/picture.png";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            this.Visible = false;
            pro.ShowDialog();
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = char.ToUpper(textBox7.Text[0]) + textBox7.Text.Substring(1).ToLower();
                textBox7.SelectionStart = textBox7.Text.Length;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = char.ToUpper(textBox4.Text[0]) + textBox4.Text.Substring(1).ToLower();
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Article(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Name(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Name(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Digit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Digit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Digit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
