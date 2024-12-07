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
    public partial class ClientsTable : Form
    {
        public ClientsTable()
        {
            InitializeComponent();
        }
        string role = Data.role;
        string con = Data.con;
        private void button3_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                int r = e.RowIndex, c = e.ColumnIndex;
                string id = dataGridView1.Rows[r].Cells[0].Value.ToString();

                switch (c)
                {
                    case 5:
                        ClientEdit edit = new ClientEdit(id);
                        this.Visible = false;
                        edit.ShowDialog();
                        this.Close();
                        break;
                    case 6:
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
       
        private void ClientsTable_Load(object sender, EventArgs e)
        {
            FillTableData();
        }
        private void FillTableData()
        {
            dataGridView1.Columns.Clear();
            string cmd = @"SELECT * FROM client";
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand(cmd, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["ClientID"].Visible = false;
            dataGridView1.Columns["ClientSurname"].HeaderText = "Фамилия";
            dataGridView1.Columns["ClientName"].HeaderText = "Имя";
            dataGridView1.Columns["ClientPatronumic"].HeaderText = "Отчество";
            dataGridView1.Columns["ClientNumberPhone"].HeaderText = "Номер телефона";

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
        private void DeleteData(string id)
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"DELETE FROM client WHERE ClientID = '{id}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientAdd add = new ClientAdd();
            this.Visible = false;
            add.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string val = e.Value.ToString();

                switch (dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "ClientSurname":
                    case "ClientName":
                    case "ClientPatronumic":
                        int lenc = val.Length;
                        int f = lenc / 3;
                        e.Value = val.Substring(0, f) + "***" + val.Substring(lenc - 2);
                        break;

                    case "ClientNumberPhone":
                        int len = val.Length;
                        e.Value = val.Substring(0, 3) + "***" + val.Substring(len - 5);
                        break;
                }
            }
        }
    }
}
