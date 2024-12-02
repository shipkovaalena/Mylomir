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
    public partial class OrdersTable : Form
    {
        public OrdersTable()
        {
            InitializeComponent();
        }

        string con = Data.con;
        string role = Data.role;        

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (role == "1")
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
        private void FillTableData()
        {
            dataGridView1.Columns.Clear();
            string cmd = @"SELECT * FROM `order`";

            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand(cmd, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["OrderID"].HeaderText = "Номер";
            dataGridView1.Columns["OrderStatus"].HeaderText = "Статус";
            dataGridView1.Columns["OrderDate"].HeaderText = "Дата создания";
            dataGridView1.Columns["OrderCompound"].HeaderText = "Состав";
            dataGridView1.Columns["OrderClient"].Visible = false;
            dataGridView1.Columns["OrderUser"].Visible = false;

            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn1);
            buttonColumn1.UseColumnTextForButtonValue = true;
            buttonColumn1.Text = "Изменить";

            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn2);
            buttonColumn2.UseColumnTextForButtonValue = true;
            buttonColumn2.Text = "Удалить";
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
                    case 6:
                        OrderEdit edit = new OrderEdit(id);
                        this.Visible = false;
                        edit.ShowDialog();
                        this.Close();
                        break;
                    case 7:
                        DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteData(id);
                            MessageBox.Show("Запись успешно удалена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillTableData();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Ошибка выделения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteData(string id)
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"DELETE FROM order WHERE OrderID = '{id}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void OrdersTable_Load(object sender, EventArgs e)
        {
            FillTableData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderAdd add = new OrderAdd();
            this.Visible = false;
            add.ShowDialog();
            this.Close();
        }
    }
}
