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
namespace Mylomir
{
    public partial class UsersTable : Form
    {
        public UsersTable()
        {
            InitializeComponent();
            // Инициализация таймера
            timer1 = new Timer();
            timer1.Interval = 30000; // 30 секунд
            timer1.Tick += timer1_Tick;

            // Запуск таймера
            timer1.Start();

            // Подписка на события активности
            this.MouseMove += ResetInactivityTimer;
            this.KeyPress += ResetInactivityTimer;
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string val = e.Value.ToString();

                switch (dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "Имя":
                    case "Отчество":
                        int lenc = val.Length;
                        int f = lenc / 3;
                        e.Value = val.Substring(0, f) + "***" + val.Substring(lenc - 2);
                        break;

                    case "Фамилия":
                        int lenn = val.Length;
                        int n = lenn / 2;
                        e.Value = val.Substring(0, n) + "***" + val.Substring(lenn- 2);
                        break;

                    case "Логин":
                        int len = val.Length;
                        int y = len / 2;
                        e.Value = val.Substring(0, y) + "***";
                        break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Блокировка системы и перенаправление на форму авторизации
            timer1.Stop(); // Остановите таймер
            this.Hide(); // Скрыть основную форму

            using (Admin loginForm = new Admin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Если авторизация успешна, показываем основную форму снова
                    this.Show();
                }
                else
                {
                    // Если авторизация не успешна, закрываем приложение
                    Application.Exit();
                }
            }
        }


        private void ResetInactivityTimer(object sender, EventArgs e)
        {
            // Сброс таймера активности
            timer1.Stop();
            timer1.Start();

        }
    }
}
