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
    public partial class OrderEdit : Form
    {
        public OrderEdit(string id)
        {
            InitializeComponent();
            idOrder = id;
            FillCombobox();
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT * FROM `order` WHERE OrderID = '{id}'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[0].ToString();                
                comboBox1.Text = reader[1].ToString();
                label4.Text += reader[2].ToString();
                textBox4.Text = reader[3].ToString();
                client = reader[4].ToString();
                user = reader[5].ToString();
                date = reader[2].ToString();
            }            
            connection.Close();

            MySqlConnection mySqlConnection1 = new MySqlConnection(con);
            mySqlConnection1.Open();

            MySqlCommand mySqlCommand1 = new MySqlCommand($"SELECT * FROM client WHERE ClientID = '{client}'", mySqlConnection1);

            MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

            while (mySqlDataReader1.Read())
            {
                comboBox2.Text = mySqlDataReader1.GetValue(1).ToString() + " " + mySqlDataReader1.GetValue(2).ToString() + " " + mySqlDataReader1.GetValue(3).ToString();
            }

            mySqlConnection1.Close();

            MySqlConnection mySqlConnection2 = new MySqlConnection(con);
            mySqlConnection2.Open();

            MySqlCommand mySqlCommand2 = new MySqlCommand($"SELECT * FROM user WHERE UserID = '{user}'", mySqlConnection2);

            MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

            while (mySqlDataReader2.Read())
            {
                comboBox3.Text = mySqlDataReader2.GetValue(1).ToString() + " " + mySqlDataReader2.GetValue(2).ToString() + " " + mySqlDataReader2.GetValue(3).ToString();
            }

            mySqlConnection1.Close();
        }
        string con = Data.con;
        string idOrder, client, user, date;

        private void button1_Click(object sender, EventArgs e)
        {
            EditData(idOrder);
        }

        private void OrderEdit_Load(object sender, EventArgs e)
        {
            //FillCombobox();            
            //label4.Text += DateTime.Now;
        }
        private void FillCombobox()
        {
            try
            {
                //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                //comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                //comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

                MySqlConnection mySqlConnection1 = new MySqlConnection(con);
                mySqlConnection1.Open();

                MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT * FROM user", mySqlConnection1);

                MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                while (mySqlDataReader1.Read())
                {
                    comboBox3.Items.Add(mySqlDataReader1.GetValue(1).ToString() + " " + mySqlDataReader1.GetValue(2).ToString() + " " + mySqlDataReader1.GetValue(3).ToString());
                }

                mySqlConnection1.Close();

                MySqlConnection mySqlConnection2 = new MySqlConnection(con);
                mySqlConnection2.Open();

                MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT * FROM client", mySqlConnection2);

                MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                while (mySqlDataReader2.Read())
                {
                    comboBox2.Items.Add(mySqlDataReader2.GetValue(1).ToString() + " " + mySqlDataReader2.GetValue(2).ToString() + " " + mySqlDataReader2.GetValue(3).ToString());
                }

                mySqlConnection1.Close();

                comboBox1.Items.Add("Оформлен");
                comboBox1.Items.Add("Готов к выдаче");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OrdersTable table = new OrdersTable();
            this.Visible = false;
            table.ShowDialog();
            this.Close();
        }

        private void EditData(string id)
        {
            //try
            //{
            //    MySqlConnection connection = new MySqlConnection(con);
            //    connection.Open();

            //    MySqlCommand command = new MySqlCommand($@"UPDATE product SET
            //    OrderID ='{textBox1.Text}', OrderStatus = '{comboBox1.Text}', 
            //    OrderDate = '{date}', OrderCompound = '{textBox4.Text}', OrderClient = '{clientID}',
            //    OrderUser = '{userID}' WHERE ProductArticleNumber = '{id}';", connection);
            //    command.ExecuteNonQuery();

            //    connection.Close();

            //    MessageBox.Show("Запись отредактирована!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Ошибка редактирования! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            try
            {
                if (comboBox1.Text == " " || comboBox2.Text == " " || textBox1.Text == " " || textBox4.Text == " ")
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string status = comboBox1.SelectedItem.ToString();

                    string user = comboBox2.SelectedItem.ToString();
                    string client = comboBox3.SelectedItem.ToString();

                    //string dateOrder = date;
                    string num = textBox1.Text.ToString();
                    string compoud = textBox4.Text.ToString();

                    string[] userOrder = user.Split(' ');
                    string[] clientOrder = client.Split(' ');
                    string[] productsOrder = compoud.Split(',');

                    MySqlConnection sqlConnection1 = new MySqlConnection(con);
                    DataTable table1 = new DataTable();

                    sqlConnection1.Open();

                    MySqlCommand command1 = new MySqlCommand($@"SELECT ClientID FROM client WHERE ClientSurname = {clientOrder[0]}
                                                            AND ClientName = {clientOrder[1]}
                                                            AND ClientPatronumic = {clientOrder[2]}");
                    MySqlDataAdapter adapter1 = new MySqlDataAdapter(command1);

                    command1.Connection = sqlConnection1;
                    command1.ExecuteNonQuery();

                    adapter1.Fill(table1);

                    int idclient = Convert.ToInt32(table1.Rows[0].ItemArray.GetValue(0));

                    sqlConnection1.Close();


                    MySqlConnection sqlConnection2 = new MySqlConnection(con);
                    DataTable table2 = new DataTable();

                    sqlConnection2.Open();

                    MySqlCommand command2 = new MySqlCommand($@"SELECT UserID FROM user WHERE UserSurname = {userOrder[0]} 
                                                             AND UserName = {userOrder[1]}
                                                             AND UserPatronymic = {userOrder[2]}");
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);

                    command2.Connection = sqlConnection2;
                    command2.ExecuteNonQuery();

                    adapter2.Fill(table2);

                    int iduser = Convert.ToInt32(table2.Rows[0].ItemArray.GetValue(0));

                    sqlConnection2.Close();


                    MySqlConnection sqlConnection = new MySqlConnection(con);

                    sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand($@"UPDATE product SET
                                                                OrderID ='{num}', OrderStatus = '{status}', 
                                                                OrderDate = '{date}', OrderCompound = '{productsOrder}', OrderClient = '{idclient}',
                                                                OrderUser = '{iduser}' WHERE ProductArticleNumber = '{id}'");
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    MySqlConnection conOrderProduct = new MySqlConnection();
                    conOrderProduct.Open();
                    MySqlCommand comOrderProduct = new MySqlCommand($@"INSERT INTO orderProduct
                                                                    (OrderID, ProductArticleNumber)
                                                                    VALUES('{num}', '{productsOrder}'");


                    MessageBox.Show("Запись отредактирована!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //comboBox1.SelectedIndex = -1;
                    //comboBox2.SelectedIndex = -1;
                    //textBox1.Clear();
                    //textBox2.Clear();
                    //dateTimePicker1.CustomFormat = " ";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
