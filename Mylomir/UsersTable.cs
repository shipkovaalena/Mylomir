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
    public partial class UsersTable : Form
    {
        public UsersTable()
        {
            InitializeComponent();
        }
        string con = Data.con;
        string role = Data.role;
        private void UsersTable_Load(object sender, EventArgs e)
        {
            FillTableData();
        }
        private void FillTableData()
        {
            string cmd = @"SELECT user.*, role.RoleName AS Роль FROM user
                INNER JOIN role ON user.UserRole = role.RoleID";
            dataGridView1.Columns.Clear();
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand(cmd, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            command.ExecuteNonQuery();
            adapter.Fill(table);

            table.Columns[1].ColumnName = "Фамилия";
            table.Columns[2].ColumnName = "Имя";
            table.Columns[3].ColumnName = "Отчество";
            table.Columns[4].ColumnName = "Логин";
            
            dataGridView1.DataSource = table;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;

            dataGridView1.Rows[0].Cells[0].Selected = false;

            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn1);
            buttonColumn1.UseColumnTextForButtonValue = true;
            buttonColumn1.Text = "Изменить";

            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn2);
            buttonColumn2.UseColumnTextForButtonValue = true;
            buttonColumn2.Text = "Удалить";

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            connection.Close();
        }

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
                    case 8:
                        UsersEdit edit = new UsersEdit(id);
                        this.Visible = false;
                        edit.ShowDialog();
                        this.Close();
                        break;
                    case 9:
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка выделения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsersAdd usersAdd = new UsersAdd();
            this.Visible = false;
            usersAdd.ShowDialog();
            this.Close();
        }
        private void DeleteData(string id)
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"DELETE FROM user WHERE UserID = '{id}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
       
    }
}
