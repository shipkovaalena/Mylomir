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
    public partial class OrderAdd : Form
    {
        public OrderAdd()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Data.page == "order")
            {
                OrdersTable tble = new OrdersTable();
                this.Visible = false;
                tble.ShowDialog();
                this.Close();
            }
            else
            {
                Products tble = new Products();
                this.Visible = false;
                tble.ShowDialog();
                this.Close();
            }
        }

        private void OrderAdd_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BasketData.getBasket;
            label4.Text += DateTime.Now;
            SumRows();
        }

        
        private void SumRows()
        {
            int summa = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                summa += Convert.ToInt32(row.Cells["Стоимость"].Value.ToString());
            }
            label6.Text += summa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string dateReceiving;
            //string dateOrder;
            //bool checkCountOnStock = false;
          
            //if (Data.role == "1")
            //{
            //    if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 && comboBox3.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Заполните все поля данными", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    }
            //}
            //DataTable tbProduct = MySqlData.LoadData("select articul, productName, description, (select categoryName from categoryproduct where id_categoryproduct=idCategory) as category, cost, image, (Select manufacturerName from manufacturer where id_manufacturer=idManufacturer) as manuf, discount, (Select unitName from unit where id_unit=unit) as unit, countOnStock  from product");
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    DataRow rowProduct = tbProduct.Select("articul='" + row.Cells["Артикул"].Value.ToString() + "'").FirstOrDefault();
            //    int tableCount = Convert.ToInt32(rowProduct["countOnStock"]);
            //    int viewCount = Convert.ToInt32(row.Cells["Количество"].Value);
            //    if (tableCount - viewCount <= 3)
            //    {
            //        checkCountOnStock = true;
            //        break;
            //    }
            //}


            //dateOrder = DateTime.Now.ToString("yyyy-MM-dd");
            //if (checkCountOnStock)
            //    dateReceiving = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd");
            //else
            //    dateReceiving = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            //string AddOrders = "";
            //Random random = new Random();
            //int codeUniq = random.Next(100, 999);
            //if (Role.getRole() == -1)
            //{

            //    AddOrders = string.Format("insert Into orders (order_date, date_of_receiving, costOrder,codeReceiving,pickuppoint, status, fio) values('{0}','{1}',{2},{3},'{4}','Отправлен', '{5} {6} {7}');SELECT last_insert_id();", dateOrder, dateReceiving, label1.Text.Split(':')[1], codeUniq, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            //}
            //if (Role.getRole() == 1 || Role.getRole() == 3)
            //    AddOrders = string.Format("insert Into orders (order_date, date_of_receiving, costOrder,idusers,codeReceiving,pickuppoint,status,staff) values('{0}','{1}',{2},(select id_users from users where surname='{3}' and name='{4}' and middle_name='{5}'),{6},'{7}', 'Отправлен', {8});SELECT last_insert_id();", dateOrder, dateReceiving, label1.Text.Split(':')[1], comboBox1.Text.Split(' ')[0], comboBox1.Text.Split(' ')[1], comboBox1.Text.Split(' ')[2], random.Next(100, 999), textBox1.Text, Role.getidUsers());
            //if (Role.getRole() == 2)
            //    AddOrders = string.Format("insert Into orders (order_date, date_of_receiving, costOrder,idusers,codeReceiving,pickuppoint,status) values('{0}','{1}',{2},{3},{4},'{5}', 'Отправлен', {6});SELECT last_insert_id();", dateOrder, dateReceiving, label1.Text.Split(':')[1], Role.getidUsers(), random.Next(100, 999), textBox1.Text);
            //string addOrdersProduct = "insert into productorders (id_product,id_orders,countProduct,costProduct) values";
            //string lastInserId = MySqlData.AddTran(AddOrders, 0, true);
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    addOrdersProduct += string.Format("({0}, {1}, {2}, {3}),", row.Cells["Артикул"].Value.ToString(), lastInserId, row.Cells["Количество"].Value.ToString(), row.Cells["Стоимость"].Value.ToString());
            //}
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    DataRow rowProduct = tbProduct.Select("articul='" + row.Cells["Артикул"].Value.ToString() + "'").FirstOrDefault();
            //    int tableCount = Convert.ToInt32(rowProduct["countOnStock"]);
            //    int viewCount = Convert.ToInt32(row.Cells["Количество"].Value);
            //    MySqlData.AddTran(string.Format("update product set countOnStock={0} where articul={1}", tableCount - viewCount, row.Cells["Артикул"].Value.ToString()), 1, false);
            //}
            //addOrdersProduct = addOrdersProduct.Substring(0, addOrdersProduct.Length - 1);
            //MySqlData.AddTran(addOrdersProduct, 2, false);
            //MessageBox.Show("Заказ успешно создан", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //var result = MessageBox.Show("Создать чек", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    Check.CreateWord();
            //    Check.AddText("Магазинчик тудым сюдымс", 1, "center", 30);
            //    Check.AddText("Заказ №" + lastInserId + " от " + DateTime.Now.ToString(), 1, "center", 30);
            //    string text = "";
            //    if (Role.getRole() == -1)
            //    {
            //        text = "Покупатель:" + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text;
            //    }
            //    else
            //    {
            //        text = "Покупатель:" + comboBox1.Text + ", тел." + MySqlData.Reader("select number_phone from users where surname='" + comboBox1.Text.Split(' ')[0] + "' and name = '" + comboBox1.Text.Split(' ')[1] + "' and middle_name='" + comboBox1.Text.Split(' ')[2] + "'")[0];
            //    }
            //    Check.AddText(text, 0, "left", 15);
            //    Check.AddText("Код для получения заказа:" + codeUniq, 1, "left", 20);
            //    Check.CreateTable(dataGridView1, label1.Text.Split(':')[1], new string[] { "Артикул", "Наименование", "Количество", "Стоимость" });
            //    Check.AddText("Адрес доставки:" + textBox1.Text, 0, "left", 15);
            //    Check.AddText("Телефон для связи: тудымс сюдымс", 0, "left", 15);
            //    Check.AddText("Подпись того ", 0, "left", 15);
            //    Check.AddText("Подпись сего ", 0, "left", 15);
            //    Check.SaveWord("Заказ №" + lastInserId + "");
            //}
            //BasketData.getBasket = new DataTable();
            //this.Hide();
            //Product product = new Product();
            //product.Show();
        }
    }
}
