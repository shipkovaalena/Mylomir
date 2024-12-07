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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        string con = Data.con;
        
        private void Products_Load(object sender, EventArgs e)
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;           
            FillTableData(@"SELECT * FROM product");
            FillFiltr();
            FillSort();
            checkCountOnStock();
            Pagination();
        }
        void Pagination()
        {
            try
            {
                // удаляем LinkLabel служащий для пагинации
                // каждый раз будем создавать новую пагинацию
                for (int j = 0, count = this.Controls.Count; j < count; ++j)
                {
                    this.Controls.RemoveByKey("page" + j);
                }

                // узнаём сколько страниц будет
                int size = dataGridView1.Rows.Count / 20; // на каждой странице по 20 зиписей
                if (Convert.ToBoolean(dataGridView1.Rows.Count % 20)) size += 1; // ситуакиця когда при делении получаем не целое число
                LinkLabel[] ll = new LinkLabel[size]; // пагинация на основе элемента ссылка(можно использовать и другой элемент)
                int x = 26, y = 649, step = 15; // место на форме для меню пагинации и расстояние между номерами страниц
                
                for (int i = 0; i < size; ++i)
                {
                    ll[i] = new LinkLabel();
                    ll[i].Text = Convert.ToString(i + 1); // текст(номер старницы) который видет пользователь
                    ll[i].Name = "page" + i;
                    ll[i].AutoSize = true; //!!!
                    ll[i].Location = new Point(x, y);
                    ll[i].Click += new EventHandler(LinkLabel_Click); // один обработчик для всех пунктов пагинации
                    this.Controls.Add(ll[i]); // добавление на форму

                    x += step;
                }

                // чтобы понять на какой странице пользователь убираем подчеркивание для активной странице
                // по умолчанию первая страница активна
                ll[0].LinkBehavior = LinkBehavior.NeverUnderline;
            }
            catch (Exception ex)
            {
                
            }

        }

        // выбор страницы пагинации
        // те строки которые нам не нужны на выбраной странице - скрываем
        private void LinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                // возвращаем всем LinkLabel подчеркивание
                foreach (var ctrl in this.Controls)
                {
                    if (ctrl is LinkLabel)
                    {
                        (ctrl as LinkLabel).LinkBehavior = LinkBehavior.AlwaysUnderline;
                    }
                }

                // узнаём какая страница выбрана и убираем подчеркивание для неё
                LinkLabel l = sender as LinkLabel;
                l.LinkBehavior = LinkBehavior.NeverUnderline;

                // узнаём с какой и по какую строку отображать информацию в таблицу
                // другие строки будем скрывать
                int numPage = Convert.ToInt32(l.Text) - 1;
                int countRows = dataGridView1.Rows.Count;
                int sizePage = 20;
                int start = numPage * sizePage;
                int stop = (countRows - start) >= sizePage ? start + sizePage : countRows;

                for (int j = 0; j < countRows; ++j)
                {
                    if (j < start || j > stop)
                    {
                        dataGridView1.CurrentCell = null;
                        dataGridView1.Rows[j].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Rows[j].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private void checkCountOnStock()
        {
            foreach (DataRow rowTable in BasketData.getBasket.Rows)
            {
                foreach (DataGridViewRow rowGridView in dataGridView1.Rows)
                {
                    if (rowTable["Артикул"].ToString() == rowGridView.Cells["articul"].Value.ToString())
                        rowGridView.Cells["countOnStock"].Value = Convert.ToInt32(rowGridView.Cells["countOnStock"].Value) - Convert.ToInt32(rowTable["Количество"]);
                }
            }
        }
       
        private void FillFiltr()
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM category", connection);
            MySqlDataReader reader = command.ExecuteReader();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Все типы");

            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(1));
            }
            comboBox1.SelectedIndex = 0;
            connection.Close();
        }

        private void FillSort()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Сортировать по");
            comboBox2.Items.Add("По возрастанию");
            comboBox2.Items.Add("По убыванию");
            comboBox2.SelectedIndex = 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Data.role == "1")
            {
                Admin admin = new Admin();
                this.Visible = false;
                admin.ShowDialog();
                this.Close();
            }
            else
            {
                Salesman salesman = new Salesman();
                this.Visible = false;
                salesman.ShowDialog();
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            FillTableData(textBox1.Text);          
        }
        private void FillTableData(string where = "")
        {
            dataGridView1.Columns.Clear();
            string cmd = @"SELECT product.*, category.CategoryName AS Категория FROM product
            INNER JOIN category ON product.ProductCategory =  category.CategoryID";

            if (comboBox1.SelectedIndex != 0 && comboBox1.SelectedIndex != -1)
            {
                cmd += $" WHERE category.CategoryName = '{comboBox1.SelectedItem}'";
            }
            if (where != String.Empty)
            {
                cmd += $" AND(product.ProductName LIKE '%{where}%')";
            }

            if (comboBox2.SelectedIndex != 0 && comboBox2.SelectedIndex != -1)
            {
                cmd += " ORDER BY product.ProductName";                
                cmd += comboBox2.SelectedItem.ToString() == "По возрастанию" ? $" ASC" : $" DESC";
            }
           

            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand(cmd, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;       

            dataGridView1.Columns["ProductName"].HeaderText = "Наименование";
            dataGridView1.Columns["ProductDescription"].HeaderText = "Описание";
            dataGridView1.Columns["ProductCost"].HeaderText = "Цена";             
            dataGridView1.Columns["ProductArticleNumber"].Visible = false;
            dataGridView1.Columns["ProductQuantityInStock"].Visible = false;
            dataGridView1.Columns["ProductCategory"].Visible = false;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Фото";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView1.Columns.Add(imageColumn);
         
            dataGridView1.Columns["ProductPhoto"].Visible = false;
            dataGridView1.Columns["ProductWight"].HeaderText = "Вес";


            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    string name = row.Cells["ProductPhoto"].Value.ToString();

            //    if (name == "")
            //    {
            //        name = "picture.png";
            //    }
            //    row.Cells["Фото"].Value = Image.FromFile(@"./photos/" + name);
            //}

            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn1);
            buttonColumn1.UseColumnTextForButtonValue = true;
            buttonColumn1.Text = "Изменить";
            buttonColumn1.Width = 150;            

            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn2);
            buttonColumn2.UseColumnTextForButtonValue = true;
            buttonColumn2.Text = "Удалить";
            buttonColumn2.Width = 150;

            DataGridViewButtonColumn buttonColumn3 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn3);
            buttonColumn3.UseColumnTextForButtonValue = true;
            buttonColumn3.Text = "В корзину";            
            buttonColumn3.Width = 150;
           
            connection.Close();
            
        }
        private void DeleteData(string id)
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"DELETE FROM product WHERE ProductArticleNumber = '{id}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                int r = e.RowIndex, c = e.ColumnIndex;
                string id = dataGridView1.Rows[r].Cells[0].Value.ToString();

                switch (c)
                {
                    case 10:
                        ProductEdit edit = new ProductEdit(id);
                        this.Visible = false;                        
                        edit.ShowDialog();
                        this.Close();
                        break;
                    case 11:
                        DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteData(id);
                            MessageBox.Show("Запись успешно удалена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillTableData();
                        }                        
                        break;
                    case 12:
                        {
                            if (BasketData.getBasket.Columns.Count == 0)
                            {
                                BasketData.getBasket.Columns.Add("Артикул");
                                BasketData.getBasket.Columns.Add("Наименование");
                                BasketData.getBasket.Columns.Add("Количество");
                                BasketData.getBasket.Columns.Add("Стоимость");
                                BasketData.getBasket.Columns.Add("Изображение");
                            }
                            if (dataGridView1.Rows[r].Cells["ProductQuantityInStock"].Value.ToString() != "0")
                            {
                                label6.Visible = true;
                                button2.Visible = true;
                                DataRow row = BasketData.getBasket.Select("Артикул='" + dataGridView1.Rows[r].Cells["Артикул"].Value.ToString() + "'").FirstOrDefault();
                                if (row == null)
                                {
                                    BasketData.getBasket.Rows.Add(dataGridView1.Rows[r].Cells["Артикул"].Value.ToString(), dataGridView1.Rows[r].Cells["Наименование"].Value.ToString(), 1 + "", dataGridView1.Rows[r].Cells["Фото"].Value.ToString());
                                }
                                else
                                {
                                    row["Количество"] = Convert.ToInt32(row["Количество"]) + 1;
                                    row["Стоимость"] = Convert.ToInt32(row["Стоимость"]) * Convert.ToInt32(row["Количество"]);
                                }
                                dataGridView1.Rows[r].Cells["ProductQuantityInStock"].Value = Convert.ToInt32(dataGridView1.Rows[r].Cells["ProductQuantityInStock"].Value) - 1;
                            }
                            else
                            {
                                MessageBox.Show("Товар закончился на складе", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            dataGridView1.ClearSelection();
                            label6.Text = BasketData.getBasket.Rows.Count + "";
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка выделения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
        private void button1_Click(object sender, EventArgs e)
        {            
            ProductAdd proAdd = new ProductAdd();
            this.Visible = false;
            proAdd.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTableData(textBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTableData(textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.RusLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        int countPageNow = 1;
        int countPage = 0;
        int count = 0;
        
       
        private void button4_Click(object sender, EventArgs e)
        {
            --countPageNow;
            LockButtonPage();
            FillTableData(textBox1.Text);
            checkCountOnStock();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ++countPageNow;
            LockButtonPage();
            FillTableData(textBox1.Text);
            checkCountOnStock();
        }
        public void LockButtonPage()
        {
            countPage = (int)Math.Ceiling(Convert.ToDouble(count) / 20);
            label1.Text = countPageNow + "/" + countPage;
           
        }
    }
}
