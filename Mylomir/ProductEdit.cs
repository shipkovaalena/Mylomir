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
    public partial class ProductEdit : Form
    {
        public ProductEdit(string id)
        {
            InitializeComponent();

            idArt = id;
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"SELECT 
            * FROM product WHERE ProductArticleNumber = '{id}'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[0].ToString();
                textBox7.Text = reader[1].ToString();                
                textBox4.Text = reader[2].ToString();
                textBox6.Text = reader[3].ToString();
                textBox3.Text = reader[4].ToString();
                textBox5.Text = reader[5].ToString();
                textBox2.Text = reader[7].ToString();

                switch (reader[6].ToString())
                {
                    case "1":
                        comboBox1.Text = "Увлажняющее";
                        break;
                    case "2":
                        comboBox1.Text = "Против акне";
                        break;
                    case "3":
                        comboBox1.Text = "Успокаивающее";
                        break;
                    case "4":
                        comboBox1.Text = "Заживляющее";
                        break;
                    case "5":
                        comboBox1.Text = "Скрабирующее";
                        break;
                }

                photoPath = reader[3].ToString();
                textBox6.Text = photoPath;
                pictureBox1.ImageLocation = $@"./photos/{photoPath}";

                if (photoPath == "")
                {
                    pictureBox1.ImageLocation = $@"./photos/picture.png";
                }
            }
            connection.Close();
        }
        string con = Data.con;
        public string photoPath;
        public string fullPath;
        string idArt;

        private void button3_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            this.Visible = false;
            pro.ShowDialog();
            this.Close();
        }
        private void EditData(string id)
        {
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
            string photo = textBox6.Text;
            try
            {
                if (photo != String.Empty)
                {
                    string dest = @"./photos/" + photo;
                    if (File.Exists(dest) == true)
                    {
                        //MessageBox.Show("Выберете другое фото или поменяйте название текущего.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        File.Copy(fullPath, dest);
                    }
                }
                    

                MySqlConnection connection = new MySqlConnection(con);
                connection.Open();

                MySqlCommand command = new MySqlCommand($@"UPDATE product SET
                ProductName ='{textBox7.Text}', ProductDescription = '{textBox4.Text}', 
                ProductQuantityInStock = '{textBox5.Text}',
                ProductCategory = '{idCategory}', ProductCost = '{textBox3.Text}',
                ProductPhoto = '{textBox6.Text}', ProductWight = '{textBox2.Text}' WHERE ProductArticleNumber = '{id}';", connection);               
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Запись отредактирована!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка редактирования! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EditData(idArt);           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Products pro = new Products();
            this.Visible = false;
            pro.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = Generation.GenerationArticul();
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

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"./photos/picture.png");
            textBox6.Text = null;
        }
        private void FillCategory()
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM category", connection);
            MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(1));
            }
            connection.Close();
        }

        private void textBox7_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Digit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void ProductEdit_Load(object sender, EventArgs e)
        {
            FillCategory();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1).ToLower();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1).ToLower();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }
    }
}
